using Brightside.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brightside.Scorer
{
    public interface IScorer
    {
        float GetScore(Article article);
    }
}