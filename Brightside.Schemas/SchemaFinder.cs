using Brightside.Data;
using Brightside.Schemas.News24;
using System.Collections.Generic;

namespace Brightside.Schemas
{
    public class SchemaFinder
    {
        public List<Article> GetFromXml(string xml)
        {
            var list = new List<ConverterBase>
            {
                new News24Converter()
            };

            List<Article> toReturn = null;
            for (int i = 0; i < list.Count && toReturn == null; i++)
            {
                var obj = list[i].GetFromXml(xml);
                if (obj != null)
                {
                    toReturn = obj;
                }
            }

            list.ForEach(x => x.Dispose());
            return toReturn;
        }
    }
}