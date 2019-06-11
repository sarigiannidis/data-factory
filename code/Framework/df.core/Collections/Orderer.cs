// --------------------------------------------------------------------------------
// <copyright file="Orderer.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// Orders items so that they are always downstream to their dependencies.
    /// </summary>
    /// <typeparam name="TNode">Any type of item that has dependencies.</typeparam>
    public sealed class Orderer<TNode>
    {
        private sealed class InternalOrderer
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private readonly IList<TNode> _Nodes;

            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private readonly Orderer<TNode> _Parent;

            public InternalOrderer(Orderer<TNode> parent, IEnumerable<TNode> nodes)
            {
                _Parent = Check.NotNull(nameof(parent), parent);
                _Nodes = Check.NotNull(nameof(nodes), nodes).ToList();
            }

            internal IOrderedEnumerable<(int Index, TNode Node)> Order()
            {
                var indices = new int[_Nodes.Count];
                for (var i = 0; i < indices.Length; i++)
                {
                    indices[i] = Index(_Nodes[i], 0, _Nodes[i]);
                }

                return Enumerable
                    .Range(0, _Nodes.Count)
                    .Select(_ => (indices[_], _Nodes[_]))
                    .OrderBy(_ => _.Item1);
            }

            private int Index(TNode node, int depth, TNode origin)
            {
                if (depth > 0 && _Parent._Comparer.Equals(node, origin))
                {
                    return int.MaxValue - depth;
                }

                var i = _Nodes.IndexOf(node);
                foreach (var dependency in _Parent._Dependencies(node))
                {
                    i = Math.Max(i, Index(dependency, depth + 1, origin) + 1);
                }

                return i;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly EqualityComparer<TNode> _Comparer;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Func<TNode, IEnumerable<TNode>> _Dependencies;

        public Orderer(Func<TNode, IEnumerable<TNode>> dependencies)
        {
            _Dependencies = Check.NotNull(nameof(dependencies), dependencies);
            _Comparer = EqualityComparer<TNode>.Default;
        }

        public IOrderedEnumerable<(int Index, TNode Node)> Order(IEnumerable<TNode> nodes) => new InternalOrderer(this, nodes).Order();
    }
}