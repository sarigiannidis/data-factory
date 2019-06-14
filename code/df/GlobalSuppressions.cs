// --------------------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Dependency Injection.", Scope = "type", Target = "~T:Df.OptionHandlers.ListHandler")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Dependency Injection.", Scope = "type", Target = "~T:Df.OptionHandlers.AddHandler")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Dependency Injection.", Scope = "type", Target = "~T:Df.OptionHandlers.GenerateHandler")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Dependency Injection.", Scope = "type", Target = "~T:Df.OptionHandlers.NewHandler")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Dependency Injection.", Scope = "type", Target = "~T:Df.OptionHandlers.TestHandler")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Redundancy", "RCS1163:Unused parameter.", Justification = "It is required.", Scope = "member", Target = "~M:Df.Program.Main(System.String[])")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Misfire.", Scope = "member", Target = "~M:Df.Io.Descriptive.ColumnDescriptionExtensions.ResolveUserType(Df.Io.Descriptive.ColumnDescription)~System.Type")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Misfire.", Scope = "member", Target = "~M:Df.Extensibility.IValueFactoryInfoExtensions.ConfigureForColumn(Df.Extensibility.IValueFactoryInfo,Df.Io.Descriptive.ColumnDescription)~Df.Extensibility.IValueFactoryConfiguration")]