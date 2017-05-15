using Brightside.Common;
using Brightside.Data;
using com.sun.source.tree;
using edu.stanford.nlp.ling;
using edu.stanford.nlp.neural.rnn;
using edu.stanford.nlp.pipeline;
using edu.stanford.nlp.sentiment;
using edu.stanford.nlp.util;
using java.util;
using System;
using System.IO;
using static edu.stanford.nlp.trees.TreeCoreAnnotations;

namespace Brightside.MetadataGatherers.Gatherers
{
	public class StanfordNLPGatherer
			: IGatherer
	{
		private static StanfordNLPGatherer instance;

		private static StanfordNLPGatherer Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new StanfordNLPGatherer();

					var props = new Properties();
					props.setProperty("annotators", "tokenize, ssplit, parse, sentiment");
					props.setProperty("ner.useSUTime", "false");

					var curDir = Environment.CurrentDirectory;
					Directory.SetCurrentDirectory(Kernel.Get<IConfiguration>().StanfordModelsDirectory);
					instance.pipeline = new StanfordCoreNLP(props);

					Directory.SetCurrentDirectory(curDir);
				}
				return instance;
			}
		}

		private StanfordCoreNLP pipeline = null;

		private ArticleMetadata Get(Article article)
		{
			String[] sentimentText = { "Very Negative", "Negative", "Neutral", "Positive", "Very Positive" };
			var annotation = pipeline.process(article.Title + " " + article.Description);

			var sentences = annotation.get(typeof(CoreAnnotations.SentencesAnnotation)) as ArrayList;
			int sentiment = 0;
			foreach (CoreMap sentence in sentences)
			{
				var tree = (edu.stanford.nlp.trees.Tree)sentence.get(typeof(SentimentCoreAnnotations.SentimentAnnotatedTree));

				sentiment = RNNCoreAnnotations.getPredictedClass(tree);
				if (sentiment > 4 || sentiment < 0)
				{
					sentiment = 0;
				}
				else
				{
					sentiment -= 2;
				}
				sentiment = (int)(100 * sentiment / 2.0);
			}

			string output = sentiment.ToString();
			//using (var stream = new java.io.ByteArrayOutputStream())
			//{
			//	pipeline.prettyPrint(annotation, new java.io.PrintWriter(stream));
			//	output = stream.toString();
			//	stream.close();
			//}

			return new ArticleMetadata
			{
				ArticleId = article.Id,
				Article = article,
				Data = output,
				Success = true,
				Type = GetMetadataType()
			};
		}

		ArticleMetadata IGatherer.Get(Article article)
		{
			return Instance.Get(article);
		}

		public string GetMetadataType()
		{
			return MetadataType.StanfordNLPOutput.ToString();
		}
	}
}