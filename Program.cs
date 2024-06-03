using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace C_13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string xmlFilePath = "file.xml";
            //string jsonFilePath = "file.json";

            //XDocument xDoc = XDocument.Load(xmlFilePath);

            //XNamespace androidNs = "http://schemas.android.com/apk/res/android";
            //XNamespace aaptNs = "http://schemas.android.com/aapt";

            //AddItemToGradient(xDoc, androidNs, "#FF0000", "0,5");

            //var element = FindElement(xDoc, androidNs + "fillColor", "#FFFFFF");
            //if(element != null)
            //{
            //    Console.WriteLine(element.ToString());
            //}


            //UpdateGradientAttributes(xDoc, androidNs, "90.0", "95.0", "50.0", "55.0");
            //xDoc.Save(xmlFilePath);

            //ConvertToJson(xDoc, jsonFilePath);

            string xmlFilePath2 = "file2.xml";
            XDocument xDoc2 = XDocument.Load(xmlFilePath2);
            foreach(var projectElement in xDoc2.Descendants("project"))
            {
                projectElement.Add(new XAttribute("newAttribute1", "Value1"));
                projectElement.Add(new XAttribute("newAttribute2", "Value2"));

                //редактирование атрибутов
                EditAttribute(projectElement, "newAttribute1", "asd", "245");
            }
            //Сохранение изменений в xml
            xDoc2.Save(xmlFilePath2);

            //Чтение xml документа и десерриализация в классы
            var projects = xDoc2.Descendants("project").Select(p => new
            { 
                Id = p.Attribute("id")?.Value,
                Name = p.Attribute("name")?.Value,
                Description = p.Attribute("description")?.Value,
                TeamMembers = p.Element("team")?.Elements("member").Select(m => m.Value).ToList(),
                NewAttribute1 = p.Attribute("asd")?.Value,
                newAttribute2 = p.Attribute("newAttribute2")?.Value
            });



            Console.WriteLine(xDoc2.ToString());


        }
        public static void AddItemToGradient(XDocument xDoc, XNamespace androidNs, string color,string offset)
        {
            var gradient = xDoc.Descendants().FirstOrDefault(e => e.Name.LocalName == "gradient");
            if(gradient != null)
            {
                gradient.Add(new XElement("item", 
                    new XAttribute(androidNs + "color", color), 
                    new XAttribute(androidNs + "offset", offset)));
            }
        }
        public static void UpdateGradientAttributes(XDocument xDoc, XNamespace androidNs, string endX, string endY, string startX, string startY)
        {
            var gradient = xDoc.Descendants().FirstOrDefault(e => e.Name.LocalName == "gradient");
            if(gradient != null)
            {
                gradient.SetAttributeValue(androidNs + "endX", endX);
                gradient.SetAttributeValue(androidNs + "endY", endY);
                gradient.SetAttributeValue(androidNs + "startX", startX);
                gradient.SetAttributeValue(androidNs + "startY", startY);
            }
            
        }
        public static XElement FindElement(XDocument xDoc, XName name,string value)
        {
            return xDoc.Descendants().FirstOrDefault(e => e.Attribute(name)?.Value == value);
        }
        public static void ConvertToJson(XDocument xDoc, string filePath)
        {
            var json = JsonConvert.SerializeXNode(xDoc, Newtonsoft.Json.Formatting.Indented,omitRootObject:true);
            File.WriteAllText(filePath, json);
        }
         
        public static void EditAttribute(XElement element,string attributename, string newAttributename, string newValue)
        {
            var attribute = element.Attribute(attributename);
            if(attribute != null)
            {
                {
                    attribute.Remove();
                    element.Add(new XAttribute(newAttributename, newValue));
                }
            }
        }
       
    }
}
