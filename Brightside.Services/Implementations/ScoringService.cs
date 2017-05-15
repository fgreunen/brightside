using Brightside.Common;
using Brightside.Scorer;
using Brightside.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brightside.Services.Implementations
{
    public class ScoringService
        : ServiceBase, IScoringService
    {
        private IScorer Scorer
        {
            get
            {
                return Kernel.Get<IScorer>();
            }
        }

        public void ScoreNext()
        {
            using (var entities = Entities)
            {
                var next = entities.Articles.OrderBy(x => x.Id).Where(x => !x.HasBeenScored && x.MetadataPopulated).FirstOrDefault();
                if (next != null)
                {
                    next.Score = Scorer.GetScore(next);
                    next.HasBeenScored = true;
                    entities.SaveChanges();
                }
            }
        }
    }
}