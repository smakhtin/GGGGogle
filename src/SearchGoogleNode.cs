using System;
using System.Collections.Generic;
using System.Threading;
using VVVV.PluginInterfaces.V2;
using Google.API.Search;

namespace GGGGoogle
{
	[PluginInfo(Name = "SearchGoogle", Author = "alg", Category = "Google")]
	public class SearchGoogleNode : IPluginEvaluate
	{
		[Input("Keyword")] private IDiffSpread<string> FKeywordInput;
		[Input("Refresh", IsSingle = true, IsBang = true)] private ISpread<bool> FRefreshInput;
		[Input("Result Count", DefaultValue = 5)] private ISpread<int> FResultCountInput;

		[Output("Result")]
		private ISpread<ISpread<IWebResult>> FResultOutput;

		readonly GwebSearchClient FSearchClient = new GwebSearchClient("http://mathrioshka.ru");

		private readonly List<List<IWebResult>> FResults = new List<List<IWebResult>>();
		private int FSpreadMax;
		private Thread FThread;

		public void Evaluate(int spreadMax)
		{
			FSpreadMax = spreadMax;
			FResultOutput.SliceCount = FResults.Count;

			if(FKeywordInput.IsChanged || FRefreshInput[0])
			{
				if(FThread != null) return;

				Thread thread = new Thread(Search);
				thread.Start();
			}

			for (int i = 0; i < FResults.Count; i++)
			{
				FResultOutput[i].AssignFrom(FResults[i]);
			}
		}

		public void Search()
		{
			FResults.Clear();

			for (int i = 0; i < FSpreadMax; i++)
			{
				try
				{
					List<IWebResult> results = (List<IWebResult>)FSearchClient.Search(FKeywordInput[i], FResultCountInput[i]);
					FResults.Add(results);
				}
				catch
				{
					
				}
			}

			FThread = null;
		}
	}
}
