namespace M1.Data.Modeling
{
    using System;
    using System.Data;
    using System.Linq;
    using static Constants.Columns;
    using static Constants.SqlStatements;
    using static Constants.Tables;

    public class ModelLoader
        : IDisposable
    {
        private bool _Disposed;
        private DataTable Columns => DataSet.Tables[TABLE_COLS];

        private DataSet DataSet { get; set; }
        private DataRelation ViewColumnsRelation { get; }

        private DataTable Views => DataSet.Tables[TABLE_VIEWS];

        private ModelLoader(DataSet dataSet)
        {
            DataSet = dataSet;
            ViewColumnsRelation = dataSet.Relations.Add(Views.Columns[COLUMN_VIEW_ID], Columns.Columns[COLUMN_VIEW_ID]);
        }

        public static ModelLoader Create(string connectionString)
        {
            var dataSet = SqlUtil.LoadDataSet(connectionString, (SQL_SELECT_VIEWS, TABLE_VIEWS), (SQL_SELECT_COLUMNS, TABLE_COLS));
            return new ModelLoader(dataSet);
        }

        public void Dispose() => Dispose(true);

        public MetaModel Load(string namespaceName, string contextName, bool includeSystemViews, string filterViews)
        {
            if (includeSystemViews)
                Views.DefaultView.RowFilter = filterViews;
            else
                Views.DefaultView.RowFilter = $"{COLUMN_IS_SYSTEM} = 0 AND {filterViews}";

            var metaViews = Views.DefaultView.OfType<DataRowView>().Select(CreateMetaView).ToArray();
            var haveNameConflicts = metaViews.GroupBy(_ => _.ClassName).Any(_ => _.Count() > 1);
            foreach (var metaView in metaViews)
            {
                foreach (var metaColumn in metaView.Columns)
                {
                    if (metaColumn.PropertyName == metaView.ClassName)
                        metaColumn.PropertyName += "_";
                }
                metaView.RootNamespace = namespaceName;
            }
            return new MetaModel(namespaceName, contextName, metaViews);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_Disposed)
                return;
            if (disposing)
            {
                DataSet?.Dispose();
                DataSet = null;
            }
            _Disposed = true;
        }

        private MetaColumn CreateMetaColumn(DataRow colRow)
        {
            var columnName = (string)colRow[COLUMN_COLUMN_NAME];
            var dataTypeName = (string)colRow[COLUMN_DATA_TYPE_NAME];
            var columnSize = (short)colRow[COLUMN_COLUMN_SIZE];
            var allowDbNull = (bool)colRow[COLUMN_ALLOW_DB_NULL];
            var dataType = SqlTypeUtil.GetDataType(dataTypeName, columnSize, allowDbNull);
            var propertyName = Language.ColumnToProperty(columnName);
            return new MetaColumn(
                allowDbNull,
                columnName,
                (int)colRow[COLUMN_COLUMN_ORDINAL],
                columnSize,
                dataType,
                dataTypeName,
                (byte)colRow[COLUMN_NUMERICOLUMN_PRECISION],
                (byte)colRow[COLUMN_NUMERICOLUMN_SCALE],
                propertyName);
        }

        private MetaView CreateMetaView(DataRowView viewRow)
        {
            var schema = (string)viewRow[COLUMN_SCHEMA_NAME];
            var name = (string)viewRow[COLUMN_VIEW_NAME];
            var namespaceSegment = Language.NamespaceSegment(schema);
            var className = Language.ViewToClass(name);
            return new MetaView(
                schema,
                name,
                (bool)viewRow[COLUMN_IS_SYSTEM],
                viewRow.Row.GetChildRows(ViewColumnsRelation).Select(CreateMetaColumn))
            {
                ClassName = className,
                ConfigClassName = Language.ViewToConfiguration(name),
                NamespaceSegment = namespaceSegment,
                ConfigNamespaceSegment = Language.NamespaceSegment(schema) + ".Configurations",
                PropertyName = Language.ViewToProperty(namespaceSegment, className)
            };
        }
    }
}