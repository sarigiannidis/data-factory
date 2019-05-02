namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
    using L = M1.Data.LazySyntaxFactory;

    internal static class SyntaxExtensions
    {
        public static MethodDeclarationSyntax AddOverrideModifier(this MethodDeclarationSyntax syntax) =>
            syntax.AddModifiers(L.Token.Override);

        public static ClassDeclarationSyntax AddPartialModifier(this ClassDeclarationSyntax syntax) =>
            syntax.AddModifiers(L.Token.Partial);

        public static MethodDeclarationSyntax AddProtectedModifier(this MethodDeclarationSyntax syntax) =>
            syntax.AddModifiers(L.Token.Protected);

        public static ClassDeclarationSyntax AddPublicModifier(this ClassDeclarationSyntax syntax) =>
            syntax.AddModifiers(L.Token.Public);

        public static MethodDeclarationSyntax AddPublicModifier(this MethodDeclarationSyntax syntax) =>
            syntax.AddModifiers(L.Token.Public);

        public static PropertyDeclarationSyntax AddPublicModifier(this PropertyDeclarationSyntax syntax) =>
            syntax.AddModifiers(L.Token.Public);

        public static ConstructorDeclarationSyntax AddPublicModifier(this ConstructorDeclarationSyntax syntax) =>
            syntax.AddModifiers(L.Token.Public);

        public static ConstructorDeclarationSyntax AddProtectedModifier(this ConstructorDeclarationSyntax syntax) =>
            syntax.AddModifiers(L.Token.Protected);

        public static PropertyDeclarationSyntax AddGetSetModifiers(this PropertyDeclarationSyntax syntax)
        {
            var get = L.Accessor.Get.WithSemicolonToken(L.Token.Semicolon);
            var set = L.Accessor.Set.WithSemicolonToken(L.Token.Semicolon);
            return syntax.AddAccessorListAccessors(get, set);
        }
    }
}