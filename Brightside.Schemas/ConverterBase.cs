using Brightside.Common;
using Brightside.Data;
using System.Collections.Generic;

namespace Brightside.Schemas
{
    internal abstract class ConverterBase
        : DisposableBase
    {
        public abstract List<Article> GetFromXml(string xml);
    }
}