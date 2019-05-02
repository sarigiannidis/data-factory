namespace M1.Data.Modeling
{
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    internal static class SqlUtil
    {
        public static DataSet LoadDataSet(string connectionString, params (string sql, string table)[] selects)
        {
            var dataSet = new DataSet();
            using (var connection = new SqlConnection(connectionString))
            using (var sqlCommand = connection.CreateCommand())
            {
                sqlCommand.CommandText = string.Join(' ', selects.Select(_ => _.sql));
                connection.Open();
                var reader = sqlCommand.ExecuteReader();
                dataSet.Load(reader, LoadOption.OverwriteChanges, selects.Select(_ => _.table).ToArray());
                reader.Close();
            }
            return dataSet;
        }
    }
}