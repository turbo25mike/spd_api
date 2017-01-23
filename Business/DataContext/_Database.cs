using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Reflection;
using System.Text;
using Microsoft.Practices.Unity;

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
        media
    }

    public class Database : IDatabase
    {
        private IConfiguration _config;
        [Dependency]
        public IConfiguration config {
            get { return _config; }
            set
            {
                _config = value;
                connection = new MySqlConnection(config.DBConnectionString);
            }
        }

        private string[] _Tables = {"media"};

        public List<T> Select<T>(DBTable table, string whereColumns, int? count = null)
        {
            List<T> list = new List<T>();
            T obj = default(T);

            //Open connection
            if (this.OpenConnection() == true)
            {
                var where = (!String.IsNullOrEmpty(whereColumns)) ? " WHERE " + whereColumns : "";
                var limit = (count.HasValue) ? " LIMIT " + count.Value : "";
                //Create Command
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM " + _Tables[table.GetHashCode()] + where + limit, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dr = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dr.Read())
                {
                    obj = Activator.CreateInstance<T>();
                    foreach (PropertyInfo prop in obj.GetType().GetProperties())
                    {
                        if (!object.Equals(dr[prop.Name], DBNull.Value))
                        {
                            prop.SetValue(obj, dr[prop.Name], null);
                        }
                    }
                    list.Add(obj);
                }

                //close Data Reader
                dr.Close();

                //close Connection
                this.CloseConnection();

            }
            return list;
        }

        //Update statement
        public void Update(DBTable table, Dictionary<string, string> setColumns, KeyValuePair<string, string> whereColumn)
        {
            //Open connection
            if (OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = "UPDATE " + table + " SET " + BuildColumns(setColumns) + " WHERE " + whereColumn.Key + "='" + whereColumn.Value + "'";
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                CloseConnection();
            }
        }

        private string BuildColumns(Dictionary<string, string> columns)
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> col in columns)
            {
                sb.Append("" + col.Key + "='" + col.Value + "',");
            }
            
            return sb.ToString().TrimEnd(',');
        }

        public void Delete(DBTable table, KeyValuePair<string, string> whereColumn)
        {
            //Open connection
            if (this.OpenConnection() == true)
            {
                using (MySqlCommand cmd = new MySqlCommand("DELETE from @TableName where @ColumnName = '@ColumnValue'", connection))
                {
                    cmd.Parameters.AddWithValue("@TableName", table);
                    cmd.Parameters.AddWithValue("@ColumnName", whereColumn.Key);
                    cmd.Parameters.AddWithValue("@ColumnValue", whereColumn.Value);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.ExecuteNonQuery();
                }

                //close Connection
                this.CloseConnection();
            }
        }

        //open connection to database
        private bool OpenConnection()
        {
            connection.Open();
            return true;
        }

        //Close connection
        private bool CloseConnection()
        {
            connection.Close();
            return true;
        }


        private MySqlConnection connection;
    }
}
