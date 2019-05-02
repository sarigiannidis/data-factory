namespace M1
{
    using CommandLine;

    internal class Options
    {
        [Option('o', "output", HelpText = "The directory where the files are saved. It defaults to the current directory.")]
        public string Output { get; set; }

        [Option('n', "namespace", HelpText = "The namespace where to place the entities.")]
        public string Namespace { get; set; }

        [Option("context", Default = "ViewContext", HelpText = "The name of the context class.")]
        public string Context { get; set; }

        [Option('c', "connection-string", HelpText = "The connection string to the database.")]
        public string ConnectionString { get; set; }

        [Option('s', "include-system", Default = false, HelpText = "Includes system views.")]
        public bool IncludeSystem { get; set; }

        [Option("filter-views", Default = "%", HelpText = "Name filter for the schemas.")]
        public string FilterViews { get; set; }

        [Option('f', "generate-fluent", Default = false, HelpText = "Generate configurations.")]
        public bool GenerateFluent { get; set; }

        [Option('x', "generate-context", Default = true, HelpText = "Generate context.")]
        public bool GenerateContext { get; set; }
    }
}