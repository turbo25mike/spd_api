using System;

namespace Business
{
    public interface IConfiguration
    {
        string DBConnectionString { get; }
    }

    public class Configuration : IConfiguration
    {
        private const string localConnection = "SERVER=localhost;DATABASE=automap;UID=root;PASSWORD=admin;";
        public string DBConnectionString => Environment.GetEnvironmentVariable("DB_CONNECTION") ?? localConnection; 
    }
}
