namespace M1.Data.Modeling
{
    using System.Collections.Generic;
    using System.Diagnostics;

    [DebuggerDisplay("Count = {Views.Count}")]
    public class MetaModel
    {
        public string Context { get; }
        public string Namespace { get; }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public IReadOnlyList<MetaView> Views { get; }

        internal MetaModel(
            string namespaceName,
            string contextName,
            IEnumerable<MetaView> views)
        {
            Namespace = namespaceName;
            Context = contextName;
            Views = new List<MetaView>(views);
        }
    }
}