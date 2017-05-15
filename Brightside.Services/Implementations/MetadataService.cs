using Brightside.Services.Interfaces;
using System.Linq;

namespace Brightside.Services.Implementations
{
    public class MetadataService
        : ServiceBase, IMetadataService
    {
        public void GatherMetadataNext()
        {
            using (var entities = Entities)
            {
                var next = entities.Articles.OrderBy(x => x.Id).Where(x => !x.MetadataPopulated).FirstOrDefault();
                if (next != null)
                {
                    var gatherer = new MetadataGatherers.GathererFacade();
                    var metadata = gatherer.GetMetadatas(next);
                    if (metadata.Count > 0)
                    {
                        metadata.ForEach(x =>
                        {
                            entities.ArticleMetadatas.Add(x);
                        });

                        next.MetadataPopulated = true;
                        entities.SaveChanges();
                    }
                }
            }
        }
    }
}