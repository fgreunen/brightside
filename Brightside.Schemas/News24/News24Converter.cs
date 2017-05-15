using Brightside.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Brightside.Schemas.News24
{
    internal class News24Converter
        : ConverterBase
    {
        public override List<Article> GetFromXml(string xml)
        {
            try
            {
                var instance = new rss();
                var schemaSet = new XmlSchemaSet();
                using (var schema = XmlReader.Create("News24\\News24.xsd"))
                {
                    schemaSet.Add(null, schema);
                    schemaSet.Compile();

                    XDocument.Parse(xml).Validate(schemaSet, null);

                    var serializer = new XmlSerializer(typeof(rss));
                    rss rss;

                    using (XmlReader reader = XmlReader.Create(new StringReader(xml)))
                    {
                        rss = (rss)serializer.Deserialize(reader);

                        return rss.channel.item.ToList()
                            .Select(x =>
                                new Article
                                {
                                    Title = x.title,
                                    Description = x.description.Text[0],
                                    URL = x.link,
                                    PublicationDate = DateTime.ParseExact(x.pubDate.Replace(":", ""), "ddd, dd MMM yyyy HHmmss K", CultureInfo.InvariantCulture)
                                }
                            ).ToList();
                    }
                }
            }
            catch (Exception) { }
            return null;
        }
    }
}