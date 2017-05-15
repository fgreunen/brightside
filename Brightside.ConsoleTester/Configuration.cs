using Brightside.Common;
using System;
using System.IO;
using System.Reflection;

namespace Brightside.ConsoleTester
{
    public class Configuration
        : IConfiguration
    {
        private string feedDirectory = Path.Combine(Environment.CurrentDirectory, "FeedDirectory");
        public string FeedDirectory
        {
            get
            {
                return feedDirectory;
            }
        }

        public string StanfordModelsDirectory => @"C:\Users\bbdnet1601\Desktop\Brightside\Brightside.MetadataGatherers\stanford-corenlp-full-2016-10-31\models";

        public Configuration()
        {
            if (!Directory.Exists(FeedDirectory))
            {
                Directory.CreateDirectory(FeedDirectory);
            }
        }
    }
}