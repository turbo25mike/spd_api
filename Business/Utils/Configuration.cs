using System;
using System.Configuration;

namespace Business
{
    public interface IConfiguration
    {
        string DBConnectionString { get; }
    }

    public class Configuration : IConfiguration
    {
        public string DBConnectionString => Properties.Settings.Default.DBConnectionString; 
    }
}
