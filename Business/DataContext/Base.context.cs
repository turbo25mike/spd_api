using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Practices.Unity;

namespace Business
{
    public class BaseContext
    {
		[Dependency]
        public IDatabase DB { get; set; }
        
        public Dictionary<string, string> BuildDictionary()
        {
            Dictionary<string, string> output = new Dictionary<string, string>();
            Type myType = this.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

            foreach (PropertyInfo prop in props)
            {
                object propValue = prop.GetValue(this, null);
                output.Add(prop.Name, propValue?.ToString());
            }
            return output;
        }
    }
}
