

namespace NewDatabase.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DbTableAttribute : Attribute
    {
        public string TableName { get; }

        public DbTableAttribute(string tableName)
        {
            TableName = tableName;
        }
    }


    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKeyAttribute : Attribute { }


    [AttributeUsage(AttributeTargets.Property)]
    public class AutoIncrementAttribute : Attribute { }


    [AttributeUsage(AttributeTargets.Property)]
    public class ForeignKeyAttribute : Attribute
    {
        public string ReferenceTable { get; }
        public string ReferenceColumn { get; }

        public ForeignKeyAttribute(string referenceTable, string referenceColumn)
        {
            ReferenceTable = referenceTable;
            ReferenceColumn = referenceColumn;
        }
    }
}
