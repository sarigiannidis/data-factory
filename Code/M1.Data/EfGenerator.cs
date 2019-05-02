namespace M1.Data
{
    using M1.Data.Modeling;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using System.Collections.Generic;
    using System.Linq;
    using static Constants.Parameters;
    using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
    using L = LazySyntaxFactory;

    public static class EfGenerator
    {
        public static string CreateDbContext(MetaModel metaModel)
        {
            string[] usings =
            {
                "Microsoft.EntityFrameworkCore"
            };
            var cd1 = ConstructorDeclaration(Identifier(metaModel.Context))
                .AddPublicModifier()
                .WithParameterList(ParameterList(SingletonSeparatedList(Parameter(Identifier("options")).WithType(IdentifierName("DbContextOptions")))))
                .WithInitializer(ConstructorInitializer(SyntaxKind.BaseConstructorInitializer, ArgumentList(SingletonSeparatedList(Argument(IdentifierName("options"))))))
                .WithBody(Block());
            var cd2 = ConstructorDeclaration(Identifier(metaModel.Context))
                .AddProtectedModifier()
                .WithBody(Block());
            var classDeclaration = ClassDeclaration(metaModel.Context)
                .AddPublicModifier()
                .AddPartialModifier()
                .AddBaseListTypes(SimpleBaseType(IdentifierName("DbContext")))
                .AddMembers(metaModel.Views.Select(CreateDbContext_DbQueryProperty).ToArray())
                .AddMembers(cd1, cd2)
                .AddMembers((MethodDeclarationSyntax)CreateDbContext_OnModelCreating(metaModel));
            return L.Namespace(metaModel.Namespace, classDeclaration, usings)
                .ToFullString();
        }

        public static string CreateEntity(MetaView metaView)
        {
            var classDeclaration = ClassDeclaration(metaView.ClassName)
                .AddPublicModifier()
                .AddPartialModifier()
                .AddMembers(metaView.Columns.Select(CreateEntity_ColumnProperty).ToArray());
            return L.Namespace(metaView.Namespace, classDeclaration)
                .ToFullString();
        }

        public static string CreateQueryTypeConfiguration(MetaView view)
        {
            string[] usings =
            {
                "Microsoft.EntityFrameworkCore",
                "Microsoft.EntityFrameworkCore.Metadata.Builders",
            };
            var classDeclaration = ClassDeclaration(view.ConfigClassName)
                .AddPublicModifier()
                .AddPartialModifier()
                .AddBaseListTypes((SimpleBaseTypeSyntax)SimpleBaseType(GenericName("IQueryTypeConfiguration").AddTypeArgumentListArguments(IdentifierName(view.ClassName))))
                .AddMembers((MethodDeclarationSyntax)CreateQueryTypeConfiguration_Configure(view));
            return L.Namespace(view.ConfigNamespace, classDeclaration, usings)
                .ToFullString();
        }

        private static PropertyDeclarationSyntax CreateDbContext_DbQueryProperty(MetaView metaView)
        {
            var type = GenericName("DbQuery")
                .AddTypeArgumentListArguments(IdentifierName(metaView.ClassIdentifier));
            return PropertyDeclaration(type, metaView.PropertyName)
            .AddPublicModifier()
            .AddGetSetModifiers();
        }

        private static MethodDeclarationSyntax CreateDbContext_OnModelCreating(MetaModel metaModel)
        {
            var bodyStatements = metaModel.Views.Select(CreateDbContext_QueryTypeConfigurationRegistration).ToArray();
            return L.VoidMethodDeclaration("OnModelCreating")
                .AddProtectedModifier()
                .AddOverrideModifier()
                .AddParameterListParameters(L.Parameter("modelBuilder").WithType(IdentifierName("ModelBuilder")))
                .AddBodyStatements(bodyStatements);
        }

        private static StatementSyntax CreateDbContext_QueryTypeConfigurationRegistration(MetaView view)
        {
            var objectCreation = ObjectCreationExpression(IdentifierName(view.ConfigIdentifier))
                .WithArgumentList(ArgumentList());
            var invocation = L.InvocationExpression(IdentifierName("modelBuilder"), "ApplyConfiguration", objectCreation);
            return ExpressionStatement(invocation);
        }

        private static PropertyDeclarationSyntax CreateEntity_ColumnProperty(MetaColumn metaColumn)
        {
            var type = L.CreateTypeSyntax(metaColumn.DataType);
            return PropertyDeclaration(type, metaColumn.PropertyName)
                .AddPublicModifier()
                .AddGetSetModifiers();
        }

        private static MethodDeclarationSyntax CreateQueryTypeConfiguration_Configure(MetaView view)
        {
            var parameterType = GenericName("QueryTypeBuilder")
                .AddTypeArgumentListArguments(IdentifierName(view.ClassName));
            var bodyStatements = new List<StatementSyntax>(view.Columns.Select(CreateQueryTypeConfiguration_PropertyMapping));
            bodyStatements.Insert(0, CreateQueryTypeConfiguration_ViewMapping(view));
            return L.VoidMethodDeclaration("Configure")
                        .AddPublicModifier()
                        .AddParameterListParameters(L.Parameter(BUILDER_PARAMETER_NAME).WithType(parameterType))
                        .AddBodyStatements(bodyStatements.ToArray());
        }

        private static StatementSyntax CreateQueryTypeConfiguration_PropertyMapping(MetaColumn metaColumn)
        {
            var lambda = SimpleLambdaExpression(
                L.Parameter("_"),
                L.MemberAccessExpression(IdentifierName("_"), IdentifierName(metaColumn.PropertyName)));
            var propertyAccess = L.InvocationExpression(IdentifierName(BUILDER_PARAMETER_NAME), "Property", lambda);
            var invocation = L.InvocationExpression(propertyAccess, "HasColumnName", IdentifierName($"\"{metaColumn.ColumnName}\""));
            return ExpressionStatement(invocation);
        }

        private static ExpressionStatementSyntax CreateQueryTypeConfiguration_ViewMapping(MetaView metaView)
        {
            var expression = L.InvocationExpression(
                IdentifierName(BUILDER_PARAMETER_NAME),
                "ToView",
                IdentifierName($"\"{metaView.ViewName}\""),
                IdentifierName($"\"{metaView.Schema}\""));
            return ExpressionStatement(expression);
        }
    }
}