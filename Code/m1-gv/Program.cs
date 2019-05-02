namespace M1
{
    using CommandLine;
    using M1.Data;
    using M1.Data.Modeling;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using static CommandLine.Parser;

    internal static class Program
    {
        private static IEnumerable<(string className, string schema, string fullString)> GenerateFiles(MetaModel metaModel, Options o)
        {
            foreach (var view in metaModel.Views)
            {
                var entity = EfGenerator.CreateEntity(view);
                yield return (view.ClassName, view.NamespaceSegment, entity);
                if (o.GenerateFluent)
                {
                    var queryTypeConfiguration = EfGenerator.CreateQueryTypeConfiguration(view);
                    yield return (view.ConfigClassName, view.ConfigNamespaceSegment, queryTypeConfiguration);
                }
            }
            if (o.GenerateContext)
            {
                var context = EfGenerator.CreateDbContext(metaModel);
                yield return (metaModel.Context, "", context);
            }
        }

        private static MetaModel LoadModel(Options o)
        {
            using (var modelLoader = ModelLoader.Create(o.ConnectionString))
                return modelLoader.Load(o.Namespace, o.Context, o.IncludeSystem, o.FilterViews);
        }

        private static void Main(string[] args) =>
                    Default.ParseArguments<Options>(args)
                .WithParsed(RunOptionsAndReturnExitCode);

        private static void RunOptionsAndReturnExitCode(Options o)
        {
            var metaModel = LoadModel(o);
            var target = Path.IsPathRooted(o.Output) ? o.Output : Path.GetFullPath(o.Output);
            if (!Directory.Exists(target))
                Directory.CreateDirectory(target);
            GenerateFiles(metaModel, o).AsParallel().ForAll(a =>
           {
               var schemaPath = Path.Combine(target, a.schema?.Replace('.', '\\'));
               if (!Directory.Exists(schemaPath))
                   Directory.CreateDirectory(schemaPath);
               var codeFileName = Path.Combine(schemaPath, string.Format("{0}.cs", a.className));
               File.WriteAllText(codeFileName, a.fullString);
           });
        }
    }
}