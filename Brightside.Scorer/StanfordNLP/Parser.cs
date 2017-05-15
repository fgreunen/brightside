using System.IO;

namespace Brightside.Scorer.StanfordNLP
{
    internal class Parser
    {
        private string _input;

        public Parser(
            string input)
        {
            _input = input;
        }

        public bool? GetSentiment()
        {
            using (var stringReader = new StringReader(_input))
            {
                var firstLine = stringReader.ReadLine();
                var substring = firstLine.Substring(firstLine.IndexOf("sentiment: ")).TrimEnd(')', ':').ToLower();
                bool? retValue = null;

                switch (substring)
                {
                    case "sentiment: positive":
                        retValue = true;
                        break;
                    case "sentiment: negative":
                        retValue = false;
                        break;
                    default: break;
                }

                return retValue;
            }
        }
    }
}