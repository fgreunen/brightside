using Brightside.Common;
using Brightside.Schemas.News24;
using Brightside.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Brightside.ConsoleTester
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Kernel.Initialize();
			Kernel.Load(new Modules());

			Task.Run(() =>
			{
				while (true)
				{
					Kernel.Get<ISourceFeedService>().GetSourceFeedXml();
					Thread.Sleep(1000);
				}
			});

			Task.Run(() =>
			{
				while (true)
				{
					Kernel.Get<ISourceFeedService>().ProcessSourceFeedXml();
					Thread.Sleep(100);
				}
			});

			Task.Run(() =>
			{
				while (true)
				{
					Kernel.Get<IMetadataService>().GatherMetadataNext();
					Thread.Sleep(100);
				}
			});

			Task.Run(() =>
			{
				while (true)
				{
					Kernel.Get<IScoringService>().ScoreNext();
					Thread.Sleep(100);
				}
			});

			Console.ReadKey();
		}
	}
}