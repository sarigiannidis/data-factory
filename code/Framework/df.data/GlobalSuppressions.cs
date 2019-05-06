// --------------------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "This is injected.", Scope = "type", Target = "~T:Df.Data.Meta.MetaDbContextFactory")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "This is injected.", Scope = "type", Target = "~T:Df.Data.SqlFactory")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Redundancy", "RCS1163:Unused parameter.", Justification = "Tell that to EF.", Scope = "type", Target = "~T:Df.Data.Meta.MetaDbContext")]