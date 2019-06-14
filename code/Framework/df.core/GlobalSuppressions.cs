// --------------------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "RCS1158:Static member in generic type should use a type parameter.", Justification = "Misfire. This is just a constant.", Scope = "member", Target = "~F:Df.Numeric.WeightedValue`1.DEFAULTWEIGHT")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0047:Remove unnecessary parentheses", Justification = "Conflicting analyzers.", Scope = "member", Target = "~M:Df.Stochastic.RandomFloatingPointExtensions.Initialize(Df.Stochastic.IRandom)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Misfire.", Scope = "member", Target = "~M:Df.Numeric.WeightedValue`1.op_Implicit(Newtonsoft.Json.Linq.JToken)~Df.Numeric.WeightedValue`1")]