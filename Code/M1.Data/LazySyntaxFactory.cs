namespace M1.Data
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CSharp;
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

    internal static class LazySyntaxFactory
    {
        public static class Accessor
        {
            public static AccessorDeclarationSyntax Get => SF.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration);
            public static AccessorDeclarationSyntax Set => SF.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration);
        }

        public static class Token
        {
            public static SyntaxToken Override => SF.Token(SyntaxKind.OverrideKeyword);
            public static SyntaxToken Partial => SF.Token(SyntaxKind.PartialKeyword);
            public static SyntaxToken Protected => SF.Token(SyntaxKind.ProtectedKeyword);
            public static SyntaxToken Public => SF.Token(SyntaxKind.PublicKeyword);
            public static SyntaxToken Semicolon => SF.Token(SyntaxKind.SemicolonToken);
            public static SyntaxToken Void => SF.Token(SyntaxKind.VoidKeyword);
        }

        public static TypeSyntax CreateTypeSyntax(Type type)
        {
            using (var provider = new CSharpCodeProvider())
            {
                var typeReference = new CodeTypeReference(type);
                return SF.ParseTypeName(provider.GetTypeOutput(typeReference));
            }
        }

        public static ExpressionSyntax InvocationExpression(ExpressionSyntax expressionSyntax, string method, params ExpressionSyntax[] arguments) =>
            SF.InvocationExpression(MemberAccessExpression(expressionSyntax, SF.IdentifierName(method)))
                .AddArgumentListArguments(arguments.Select(SF.Argument).ToArray());

        public static MemberAccessExpressionSyntax MemberAccessExpression(ExpressionSyntax left, SimpleNameSyntax right) =>
            SF.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, left, right);

        public static NamespaceDeclarationSyntax Namespace(string name, ClassDeclarationSyntax classDeclaration, params string[] usings) =>
            SF.NamespaceDeclaration(SF.ParseName(name))
                .AddUsings(usings.Select(Using).ToArray())
                .AddMembers(classDeclaration)
                .NormalizeWhitespace();

        public static ParameterSyntax Parameter(string text) =>
            SF.Parameter(SF.Identifier(text));

        public static MethodDeclarationSyntax VoidMethodDeclaration(string name) =>
            SF.MethodDeclaration(SF.PredefinedType(Token.Void), SF.Identifier(name));

        private static UsingDirectiveSyntax Using(string text)
        {
            NameSyntax nameSyntax;
            var parts = new Queue<IdentifierNameSyntax>(text.Split('.').Select(SF.IdentifierName));
            if (parts.Count == 1)
                nameSyntax = parts.Dequeue();
            nameSyntax = SF.QualifiedName(parts.Dequeue(), parts.Dequeue());
            while (parts.Count > 0)
                nameSyntax = SF.QualifiedName(nameSyntax, parts.Dequeue());
            return SF.UsingDirective(nameSyntax);
        }
    }
}