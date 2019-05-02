namespace M1.Data
{
    using Humanizer;

    public static class Language
    {
        public static string ColumnToProperty(string column) =>
            column.Transform(To.LowerCase, To.TitleCase).Pascalize();

        public static string NamespaceSegment(string segment) =>
            segment.Transform(To.LowerCase, To.TitleCase).Pascalize();

        public static string ViewToClass(string view) =>
            view.Transform(To.LowerCase, To.TitleCase).Pascalize().Singularize(inputIsKnownToBePlural: false);

        public static string ViewToConfiguration(string view) =>
            string.Format("{0}QueryTypeConfiguration", ViewToClass(view));

        public static string ViewToProperty(string namespaceName, string view) =>
            namespaceName + view.Pluralize();

        internal static string Namespace(string namespaceName, string schema) =>
            namespaceName + "." + NamespaceSegment(schema);
    }
}