namespace M1.Data.Modeling
{
    using System.Collections.Generic;
    using System.Diagnostics;

    [DebuggerDisplay(".{NamespaceSegment,nq}.{ClassName,nq} ([{Schema,nq}].[{ViewName,nq}])")]
    public class MetaView
    {
        public string ClassIdentifier => string.Format("{0}.{1}", NamespaceSegment, ClassName);
        public string ClassName { get; internal set; }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public IReadOnlyList<MetaColumn> Columns { get; }

        public string ConfigClassName { get; internal set; }
        public string ConfigIdentifier => string.Format("{0}.{1}", ConfigNamespaceSegment, ConfigClassName);
        public bool IsSystem { get; }
        public string Namespace => string.Format("{0}.{1}", RootNamespace, NamespaceSegment);
        public string ConfigNamespace => string.Format("{0}.{1}", RootNamespace, ConfigNamespaceSegment);
        public string NamespaceSegment { get; internal set; }
        public string ConfigNamespaceSegment { get; internal set; }
        public string PropertyName { get; internal set; }
        public string RootNamespace { get; internal set; }
        public string Schema { get; }
        public string ViewName { get; }

        internal MetaView(
            string schema,
            string name,
            bool isSystem,
            IEnumerable<MetaColumn> columns)
        {
            Columns = new List<MetaColumn>(columns);
            IsSystem = isSystem;
            Schema = schema;
            ViewName = name;
        }
    }
}