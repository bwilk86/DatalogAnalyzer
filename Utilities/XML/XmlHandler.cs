using System;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;

namespace Utilities.XML
{
    public class XmlHandler
    {
        public T Deserialize<T>(string filePath)
        {
            var ser = new XmlSerializer(typeof(T));
            using (var reader = XmlReader.Create(filePath))
            {
                return (T) ser.Deserialize(reader);
            }
        }
        public void Serialize<T>(string filePath, T obj)
        {
            var ser = new XmlSerializer(typeof(T));

            var file = File.Create(filePath);

            ser.Serialize(file, obj);
            file.Close();
        }
    }
}
