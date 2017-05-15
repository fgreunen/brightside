using Brightside.Common;
using Brightside.Scorer;
using Brightside.Services.Implementations;
using Brightside.Services.Interfaces;
using Ninject.Modules;

namespace Brightside.ConsoleTester
{
    public class Modules
        : NinjectModule
    {
        public override void Load()
        {
            Bind<ISourceFeedService>().To<SourceFeedService>();
            Bind<IScoringService>().To<ScoringService>();
            Bind<IScorer>().To<ScorerFacade>();
            Bind<IMetadataService>().To<MetadataService>();
            Bind<IConfiguration>().ToConstant(new Configuration());
        }
    }
}