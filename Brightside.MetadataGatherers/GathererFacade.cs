using Brightside.Common;
using Brightside.Data;
using Brightside.MetadataGatherers.Gatherers;
using System;
using System.Collections.Generic;

namespace Brightside.MetadataGatherers
{
    public class GathererFacade
        : DisposableBase
    {
        private List<IGatherer> GetMetadataGatherers()
        {
            return new List<IGatherer>
            {
                new StanfordNLPGatherer()
            };
        }

        public List<ArticleMetadata> GetMetadatas(
            Article article)
        {
            var meta = new List<ArticleMetadata>();
            GetMetadataGatherers().ForEach(x =>
            {
                ArticleMetadata metadata = null;
                try
                {
                    metadata = x.Get(article);
                }
                catch (Exception ex)
                {
                    if (metadata == null)
                    {
                        metadata = new ArticleMetadata
                        {
                            Data = ex.Message,
                            Success = false
                        };
                    };
                }
                metadata.Type = x.GetMetadataType();
                meta.Add(metadata);
            });

            meta.ForEach(x => x.ArticleId = article.Id);
            return meta;
        }
    }
}