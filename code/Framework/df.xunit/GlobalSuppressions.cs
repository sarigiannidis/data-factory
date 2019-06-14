// --------------------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Dependency Injection.", Scope = "type", Target = "~T:Xunit.OrderTraitDiscoverer")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "An aggregate exception is thrown.", Scope = "member", Target = "~M:Xunit.TemporaryFileContext.Dispose(System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0067:Dispose objects before losing scope", Justification = "Is too.", Scope = "member", Target = "~M:Xunit.TemporaryFileContext.AddContext(System.Reflection.MemberInfo,System.String,System.String)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Is too.", Scope = "member", Target = "~M:Xunit.TemporaryFileContext.AddContext(System.Reflection.MemberInfo,System.String,System.String)")]
