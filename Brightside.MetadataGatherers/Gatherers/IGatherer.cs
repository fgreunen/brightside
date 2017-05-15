using Brightside.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brightside.MetadataGatherers.Gatherers
{
    public interface IGatherer
    {
        string GetMetadataType();
        ArticleMetadata Get(Article article);
    }
}