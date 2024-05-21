using System.Data;
using System.Data.SQLite;
using System.Reflection;
using NewDatabase.Models;
using NewDatabase.Attributes;

public class ReflectionORM
{
    private readonly string connectionString;
    private readonly string dbFilePath;

    public ReflectionORM(string dbName = "MyDatabase.db")
    {
        dbFilePath = dbName;
        connectionString = $"Data Source={dbName};Version=3;";
    }

    public void CreateDatabase()
    {
        DeleteDatabase();

        Console.WriteLine("--- CREATING DATABASE ---");

        Type[] models = { typeof(Address), typeof(Student), typeof(School), typeof(Application), typeof(StudyProgram) };
        foreach (var model in models)
        {
            CreateTable(model);
        }

        Console.WriteLine("-------------------------\n");
    }
    public void DeleteDatabase()
    {
        Console.WriteLine("--- DELETING DATABASE ---");
        try 
        { 
            SQLiteConnection.ClearAllPools();

            if (File.Exists(dbFilePath))
            {
                File.Delete(dbFilePath);
                Console.WriteLine("Database file deleted successfully.");
            }
            else
            {
                Console.WriteLine("Database file does not exist.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error deleting database: " + ex.Message);
        }
    }
    private void CreateTable(Type model)
    {
        var tableName = model.Name;
        Console.WriteLine($"Creating table {tableName}");

        var properties = model.GetProperties();

        var columns = properties.Select(p => $"{p.Name} {GetSqlType(p.PropertyType)}" +
                                             $"{(p.GetCustomAttribute<PrimaryKeyAttribute>() != null ? " PRIMARY KEY" : "")}" +
                                             $"{(p.GetCustomAttribute<AutoIncrementAttribute>() != null ? " AUTOINCREMENT" : "")}" +
                                             (p.GetCustomAttribute<ForeignKeyAttribute>() != null ?
                                             $" REFERENCES {p.GetCustomAttribute<ForeignKeyAttribute>()?.ReferenceTable}" +
                                             $"({p.GetCustomAttribute<ForeignKeyAttribute>()?.ReferenceColumn})" : ""))
                                .ToArray();

        var sql = $"CREATE TABLE IF NOT EXISTS {tableName} ({string.Join(", ", columns)});";

        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }


    public void Insert<T>(T obj) where T : new()
    {
        var type = obj.GetType();
        var tableName = type.Name;
        var properties = type.GetProperties().Where(p => p.GetCustomAttribute<AutoIncrementAttribute>() == null);
        var columnNames = string.Join(", ", properties.Select(p => p.Name));
        var values = string.Join(", ", properties.Select(p => $"'{p.GetValue(obj)}'"));

        var sql = $"INSERT INTO {tableName} ({columnNames}) VALUES ({values}); SELECT last_insert_rowid();";

        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(sql, connection))
            {
                var lastId = Convert.ToInt64(command.ExecuteScalar());
                var autoIncrementProperty = type.GetProperties()
                    .FirstOrDefault(p => p.GetCustomAttribute<AutoIncrementAttribute>() != null);
                if (autoIncrementProperty != null && lastId > 0)
                {
                    autoIncrementProperty.SetValue(obj, Convert.ChangeType(lastId, autoIncrementProperty.PropertyType));
                }
            }
        }
    }
    public List<T> Select<T>() where T : new()
    {
        var type = typeof(T);
        var tableName = type.Name;
        var properties = type.GetProperties();
        var columnNames = string.Join(", ", properties.Select(p => p.Name));

        var sql = $"SELECT {columnNames} FROM {tableName}";

        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(sql, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    var list = new List<T>();
                    while (reader.Read())
                    {
                        var obj = new T();
                        foreach (var prop in properties)
                        {
                            var value = reader[prop.Name];
                            if (value != DBNull.Value)
                            {
                                prop.SetValue(obj, Convert.ChangeType(value, prop.PropertyType));
                            }
                        }
                        list.Add(obj);
                    }
                    return list;
                }
            }
        }
    }
    public void Update<T>(T obj)
    {
        var type = obj.GetType();
        var tableName = type.Name;
        var properties = type.GetProperties();
        var setClause = string.Join(", ", properties.Select(p => $"{p.Name} = '{p.GetValue(obj)}'"));

        var idProp = type.GetProperties().FirstOrDefault(p => p.GetCustomAttribute<PrimaryKeyAttribute>() != null);
        if (idProp == null)
            throw new InvalidOperationException("No 'Id' property defined!");

        var sql = $"UPDATE {tableName} SET {setClause} WHERE {idProp.Name} = {idProp.GetValue(obj)}";

        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
    public void Delete<T>(T obj)
    {
        var type = obj.GetType();
        var tableName = type.Name;

        var idProp = type.GetProperties().FirstOrDefault(p => p.GetCustomAttribute<PrimaryKeyAttribute>() != null);
        if (idProp == null)
            throw new InvalidOperationException("No 'Id' property defined!");

        var sql = $"DELETE FROM {tableName} WHERE {idProp.Name} = {idProp.GetValue(obj)}";

        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }


    private string GetSqlType(Type type)
    {
        if (type == typeof(int)) return "INTEGER";
        if (type == typeof(string)) return "TEXT";
        if (type == typeof(DateTime)) return "TEXT";
        if (type == typeof(bool)) return "BOOLEAN";
        return "TEXT";
    }
}