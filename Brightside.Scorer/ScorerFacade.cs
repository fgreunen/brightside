using Brightside.Common;
using System.Collections.Generic;
using System.Linq;
using Brightside.Data;

namespace Brightside.Scorer
{
    public class ScorerFacade
        : DisposableBase, IScorer
    {
        public float GetScore(Article article)
        {
            return new List<IScorer>()
            {
                new StanfordNLP.Scorer()
            }.Select(x => x.GetScore(article)).Average();
        }
    }
}