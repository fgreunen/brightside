using Brightside.Data;

namespace Brightside.Scorer
{
    internal class DefaultScorer
        : IScorer
    {
        private const float MAX_LENGTH = 100f;
        public float GetScore(Article article)
        {
            var ratio = 100 * (float)article.Title.Length / MAX_LENGTH;
            return Clamp((int)ratio, -100, 100);
        }

        private static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }
    }
}