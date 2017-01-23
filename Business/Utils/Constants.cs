using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business
{
    public class Constants
    {
        public enum Response { Success, Failed};

        public static string DBError = "Error occured while connecting to the database.";
        public static string ServerError = "Error occured while to our service.";
        public static string UserNotFoundError = "User can not be found.";
    }
}
