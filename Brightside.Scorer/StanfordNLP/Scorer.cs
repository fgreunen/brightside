using System.Linq;
using Brightside.Data;

namespace Brightside.Scorer.StanfordNLP
{
	internal class Scorer
			: IScorer
	{
		public float GetScore(Article article)
		{
			var metadata = article.ArticleMetadatas.FirstOrDefault(x => x.Success && x.Type == MetadataGatherers.MetadataType.StanfordNLPOutput.ToString());

			var score = int.Parse(metadata.Data);

			return score;

			//var parser = new Parser(metadata.Data);
			//var sentiment = parser.GetSentiment();

			//switch (sentiment)
			//{
			//	case true:
			//		return Constants.MAX_SCORE;

			// case false: return Constants.MIN_SCORE;

			//	case null:
			//	default:
			//		return 0;
			//}
		}
	}
}