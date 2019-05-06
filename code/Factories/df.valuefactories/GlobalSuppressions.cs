// --------------------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Injected.", Scope = "type", Target = "~T:Df.ValueFactories.Random.DoubleRandomConfigurationFactory")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "This is unnecessary for configuration types.", Scope = "type", Target = "~T:Df.ValueFactories.RandomStringConfiguration")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "This is unnecessary for configuration types.", Scope = "type", Target = "~T:Df.ValueFactories.RandomBoolConfiguration")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "This is unnecessary for configuration types.", Scope = "type", Target = "~T:Df.ValueFactories.BinaryConfiguration")]