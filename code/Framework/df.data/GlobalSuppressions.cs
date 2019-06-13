// --------------------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Dependency Injection.", Scope = "type", Target = "~T:Df.Data.Meta.MetaDbContextFactory")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Dependency Injection.", Scope = "type", Target = "~T:Df.Data.SqlFactory")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Redundancy", "RCS1163:Unused parameter.", Justification = "Tell that to EF.", Scope = "type", Target = "~T:Df.Data.Meta.MetaDbContext")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0069:Disposable fields should be disposed", Justification = "Is too.", Scope = "member", Target = "~F:Df.Data.LocalDbServer._Instance")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0069:Disposable fields should be disposed", Justification = "Is too.", Scope = "member", Target = "~F:Df.Data.LocalDbServer._SqlLocalDbApi")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0067:Dispose objects before losing scope", Justification = "Is too.", Scope = "member", Target = "~M:Df.Data.SqlBase.ExecuteQuery``1(System.String,System.Func{System.Data.IDataRecord,``0})~Df.Data.ISqlQueryResultCollection{``0}")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0067:Dispose objects before losing scope", Justification = "Is too.", Scope = "member", Target = "~M:Df.Data.SqlQueryResultCollection`1.GetEnumerator~System.Collections.Generic.IEnumerator{`0}")]