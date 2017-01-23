using System;
using Microsoft.Practices.Unity;
using MySql.Data.MySqlClient;

namespace Business
{
    public interface IErrorManager {
        string Handle(Exception ex);
    }

    public class ErrorManager : IErrorManager
    {
        public string Handle(Exception ex)
        {
            if (ex is MySqlException)
            {
                return Constants.DBError;
            }

            try
            {
                //TODO find out error table
                //_db.Update("ErrorTable with ex");
            }
            catch (Exception)
            {
                //DB failed to save
            }
            return Constants.ServerError;
        }

        [Dependency]
        public IDatabase _db { get; set; }
    }
}
