using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Taizhou.PLE.Common
{
    public class Serializer
    {
        public static string Serialize(object obj)
        {
            if (obj == null)
                return null;

            StringBuilder builder = new StringBuilder();
            StringWriter writer = new StringWriter(builder);

            try
            {
                XmlSerializer ser = new XmlSerializer(obj.GetType());

                ser.Serialize(writer, obj);

                return builder.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static object Deserialize(string assemblyName, string typeName, string objectXML)
        {
            if (string.IsNullOrEmpty(assemblyName) || string.IsNullOrEmpty(typeName) ||
                string.IsNullOrEmpty(objectXML))
                return null;

            Assembly assembly = Assembly.Load(assemblyName);
            Type type = assembly.GetType(typeName);

            StringReader reader = new StringReader(objectXML);
            XmlSerializer ser = new XmlSerializer(type);

            try
            {
                return ser.Deserialize(reader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
