using Google.API.Search;
using VVVV.PluginInterfaces.V2;

namespace GGGGoogle
{
	[PluginInfo(Name = "GetResultUrls", Author = "alg", Category = "Google")]
	public class GetResultUrlsNode : ResultProperty
	{
		[Output("Url")]
		private ISpread<string> FUrlOutput;
		[Output("Visible Url")]
		private ISpread<string> FVisibleUrlOutput;
		[Output("Cache Url")]
		private ISpread<string> FCacheUrlOutput;

		override public void Evaluate(int spreadMax)
		{
			FUrlOutput.SliceCount = FVisibleUrlOutput.SliceCount = FCacheUrlOutput.SliceCount = spreadMax;

			if (FResultInput[0] == null) return;

			for (int i = 0; i < spreadMax; i++)
			{
				FUrlOutput[i] = FResultInput[i].Url;
				FVisibleUrlOutput[i] = FResultInput[i].VisibleUrl;
				FCacheUrlOutput[i] = FResultInput[i].CacheUrl;
			}
		}
	}
}
