using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace WOSApi.Helpers
{
    class XMLHelper
    {
        public string SerializeToXML<T>(T MyObject)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(T));
            using (StringWriter sww = new StringWriter())
            using (XmlWriter writer = XmlWriter.Create(sww))
            {
                xsSubmit.Serialize(writer, MyObject);
                var xml = sww.ToString(); // Your XML
                return xml;
            }
        }

        public T DesrializeXML<T>(string xml)
        {
            T result;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (TextReader reader = new StringReader(xml))
            {
                result = (T)serializer.Deserialize(reader);
            }
            return result;
        }


    }
}
