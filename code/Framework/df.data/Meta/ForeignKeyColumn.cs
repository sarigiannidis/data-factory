// --------------------------------------------------------------------------------
// <copyright file="ForeignKeyColumn.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Data.Meta
{
    public sealed class ForeignKeyColumn
    {
        public int ConstraintColumnId { get; set; }

        public int ConstraintObjectId { get; set; }

        public ForeignKey ForeignKey { get; set; }

        public Column Parent { get; set; }

        public int ParentColumnId { get; set; }

        public int ParentObjectId { get; set; }

        public Column Referenced { get; set; }

        public int ReferencedColumnId { get; set; }

        public int ReferencedObjectId { get; set; }
    }
}