using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Text;

namespace Business
{
    public interface IDatabase
    {
        List<T> Select<T>(DBTable table, string whereColumns = null, int? count = null);
        void Update(DBTable table, Dictionary<string, string> setColumns, KeyValuePair<string, string> whereColumn);
        void Delete(DBTable table, KeyValuePair<string, string> whereColumn);
    }

    public enum DBTable
    {
        Media
    }

    public class Database : IDatabase
    {
        public Database()
        {
            _connection = new MySqlConnection(Environment.GetEnvironmentVariable("APP_DB_CONNECTION") ?? LocalConnection);
        }

        private const string LocalConnection = "SERVER=localhost;DATABASE=automap;UID=root;PASSWORD=admin;";
        private readonly string[] _tables = { "media" };

        public List<T> Select<T>(DBTable table, string whereColumns, int? count = null)
        {
            List<T> list = new List<T>();
            T obj = default(T);

            //Open connection
            if (OpenConnection())
            {
                var where = (!string.IsNullOrEmpty(whereColumns)) ? " WHERE " + whereColumns : "";
                var limit = (count.HasValue) ? " LIMIT " + count.Value : "";
                //Create Command
                var cmd = new MySqlCommand("SELECT * FROM " + _tables[table.GetHashCode()] + where + limit, _connection);
                //Create a data reader and Execute the command
                var dr = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dr.Read())
                {
                    obj = Activator.CreateInstance<T>();
                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        if (!Equals(dr[prop.Name], DBNull.Value))
                        {
                            prop.SetValue(obj, dr[prop.Name], null);
                        }
                    }
                    list.Add(obj);
                }

                //close Data Reader
                dr.Close();

                //close Connection
                CloseConnection();

            }
            return list;
        }

        //Update statement
        public void Update(DBTable table, Dictionary<string, string> setColumns, KeyValuePair<string, string> whereColumn)
        {
            //Open connection
            if (OpenConnection())
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = "UPDATE " + table + " SET " + BuildColumns(setColumns) + " WHERE " + whereColumn.Key + "='" + whereColumn.Value + "'";
                //Assign the connection using Connection
                cmd.Connection = _connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                CloseConnection();
            }
        }

        private string BuildColumns(Dictionary<string, string> columns)
        {
            var sb = new StringBuilder();
            foreach (var col in columns)
                sb.Append("" + col.Key + "='" + col.Value + "',");
            return sb.ToString().TrimEnd(',');
        }

        public void Delete(DBTable table, KeyValuePair<string, string> whereColumn)
        {
            //Open connection
            if (OpenConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand("DELETE from @TableName where @ColumnName = '@ColumnValue'", _connection))
                {
                    cmd.Parameters.AddWithValue("@TableName", table);
                    cmd.Parameters.AddWithValue("@ColumnName", whereColumn.Key);
                    cmd.Parameters.AddWithValue("@ColumnValue", whereColumn.Value);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.ExecuteNonQuery();
                }

                //close Connection
                CloseConnection();
            }
        }

        //open connection to database
        private bool OpenConnection()
        {
            _connection.Open();
            return true;
        }

        //Close connection
        private void CloseConnection()
        {
            _connection.Close();
        }


        private readonly MySqlConnection _connection;
    }
}
