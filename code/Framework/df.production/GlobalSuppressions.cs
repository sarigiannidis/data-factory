// --------------------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("General", "RCS1079:Throwing of new NotImplementedException.", Justification = "They don't need to be implemented as they are never used.", Scope = "type", Target = "~T:Df.Production.RecordGenerator")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0067:Dispose objects before losing scope", Justification = "Is too.", Scope = "member", Target = "~M:Df.Production.DatasetGenerator.ExecuteSqlBulkCopy(System.Data.SqlClient.SqlTransaction,System.Data.SqlClient.SqlConnection,System.Data.SqlClient.SqlConnection,System.Data.SqlClient.SqlBulkCopyOptions,System.String,System.Collections.Generic.IEnumerable{System.String})")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0067:Dispose objects before losing scope", Justification = "Is too.", Scope = "member", Target = "~M:Df.Production.DatasetGenerator.InternalGenerator.FillTable(Df.Data.ISql,Df.Io.Prescriptive.TablePrescription)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Is too.", Scope = "member", Target = "~M:Df.Production.DatasetGenerator.ExecuteSqlBulkCopy(System.Data.SqlClient.SqlTransaction,System.Data.SqlClient.SqlConnection,System.Data.SqlClient.SqlConnection,System.Data.SqlClient.SqlBulkCopyOptions,System.String,System.Collections.Generic.IEnumerable{System.String})")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Is too.", Scope = "member", Target = "~M:Df.Production.DatasetGenerator.InternalGenerator.FillTable(Df.Data.ISql,Df.Io.Prescriptive.TablePrescription)")]