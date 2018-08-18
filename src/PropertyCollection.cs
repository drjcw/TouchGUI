using System.Collections.Generic;

namespace QuickVR
{
    namespace Core
    {
        public class PropertyCollection
        {
            public PropertyCollection()
            {
                properties = new Dictionary<string, string>();
            }

            public string Get(string Property)
            {
                if (!properties.ContainsKey(Property)) return null;
                else return properties[Property];
            }

            public void Set(string Property, string Value)
            {
                if (!properties.ContainsKey(Property)) properties.Add(Property, Value);
                else properties[Property] = Value;
            }

            public void Clear()
            {
                properties.Clear();
            }

            protected Dictionary<string, string> properties;
        }
    } 
}