namespace M1.Data.Modeling
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("{PropertyName,nq} ([{ColumnName,nq}] {DataTypeName,nq}[{ColumnSize}]({NumericPrecision}, {NumericScale}), {AllowDBNull?\"NULL\":\"NOT NULL\",nq}")]
    public class MetaColumn
    {
        public bool AllowDBNull { get; }
        public string ColumnName { get; }
        public int ColumnOrdinal { get; }
        public short ColumnSize { get; }
        public Type DataType { get; }
        public string DataTypeName { get; }
        public byte NumericPrecision { get; }
        public byte NumericScale { get; }
        public string PropertyName { get; internal set; }

        public MetaColumn(
            bool allowDBNull,
            string columnName,
            int columnOrdinal,
            short columnSize,
            Type dataType,
            string dataTypeName,
            byte numericPrecision,
            byte numericScale,
            string propertyName)
        {
            AllowDBNull = allowDBNull;
            ColumnName = columnName;
            ColumnOrdinal = columnOrdinal;
            ColumnSize = columnSize;
            DataType = dataType;
            DataTypeName = dataTypeName;
            NumericPrecision = numericPrecision;
            NumericScale = numericScale;
            PropertyName = propertyName;
        }
    }
}