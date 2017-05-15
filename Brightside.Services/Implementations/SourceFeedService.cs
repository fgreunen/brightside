using Brightside.Services.Interfaces;
using System.Net;
using System.IO;
using System;
using Brightside.Data;
using System.Threading;
using System.Linq;
using Brightside.Schemas.News24;
using Brightside.Schemas;

namespace Brightside.Services.Implementations
{
    public class SourceFeedService
        : ServiceBase, ISourceFeedService
    {
        private const int WAIT_MILLISECONDS = 250;
        private static Mutex mutex1 = new Mutex(false, "Brightside.Services.Implementations.SourceFeedService.NextSourceFeedToGet");
        private static Mutex mutex2 = new Mutex(false, "Brightside.Services.Implementations.SourceFeedService.ProcessSourceFeedXml");

        public void GetSourceFeedXml()
        {
            var sourceFeed = NextSourceFeedToGet();
            if (sourceFeed != null)
            {
                string xml = string.Empty;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sourceFeed.URL);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (var response = (HttpWebResponse)request.GetResponse())
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                    xml = reader.ReadToEnd();

                if (!string.IsNullOrEmpty(xml))
                {
                    var timestamp = DateTime.Now;
                    var sourceFeedsHistory = new SourceFeedsHistory()
                    {
                        LastRetrievedTimestamp = timestamp,
                        SourceFeedId = sourceFeed.Id
                    };
                    using (var entities = Entities)
                    {
                        entities.SourceFeedsHistories.Add(sourceFeedsHistory);
                        entities.SaveChanges();
                    }

                    var fileName = $"{sourceFeedsHistory.Id}.xml";
                    File.WriteAllText(Path.Combine(Configuration.FeedDirectory, fileName), xml);
                }
            }
        }

        public void ProcessSourceFeedXml()
        {
            if (mutex2.WaitOne(WAIT_MILLISECONDS))
            {
                var file = Directory.EnumerateFiles(Configuration.FeedDirectory, "*.xml")
                .OrderBy(x => x).FirstOrDefault();
                if (file != null)
                {
                    try
                    {
                        string xml = null;
                        using (var reader = File.OpenText(file))
                        {
                            xml = reader.ReadToEnd();
                        }

                        var articles = new SchemaFinder().GetFromXml(xml);
                        if (articles == null)
                        {
                            // No parser!
                            // TODO: move
                            FileInfo fi = new FileInfo(file);
                            fi.CopyTo(fi.FullName + ".err");
                        }
                        else
                        {
                            var fileName = Path.GetFileName(file);
                            var id = int.Parse(fileName.Substring(0, fileName.IndexOf(".")));
                            using (var entities = Entities)
                            {
                                var sourceFeedHistory = entities.SourceFeedsHistories.Single(x => x.Id == id);
                                articles.ForEach(x =>
                                {
                                    x.SourceFeedId = sourceFeedHistory.SourceFeedId;
                                    x.HasBeenScored = false;
                                    if (!entities.Articles.Any(y => y.SourceFeedId == x.SourceFeedId && y.Title == x.Title))
                                    {
                                        sourceFeedHistory.SourceFeed.Articles.Add(x);
                                    }
                                });
                                entities.SaveChanges();
                            }
                        }

                        File.Delete(file);
                    }
                    catch { }
                }
            }
        }

        private SP_NextSourceFeedToGet_Result NextSourceFeedToGet()
        {
            if (mutex1.WaitOne(WAIT_MILLISECONDS))
            {
                using (var entities = new BrightsideEntities())
                {
                    return entities.SP_NextSourceFeedToGet().FirstOrDefault();
                }
            }

            return null;
        }
    }
}