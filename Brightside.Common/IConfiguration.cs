namespace Brightside.Common
{
    public interface IConfiguration
    {
        string FeedDirectory { get; }
        string StanfordModelsDirectory { get; }
    }
}