using VVVV.PluginInterfaces.V2;

namespace GGGGoogle
{
	[PluginInfo(Name = "GetResultTitle", Help = "Get result title", Author = "alg", Category = "Google")]
	public class GetResultTitleNode : ResultProperty
	{
		[Output("Title")] private ISpread<string> FTitleOutput;
		
		override public void Evaluate(int spreadMax)
		{
			FTitleOutput.SliceCount = spreadMax;

			if (FResultInput[0] == null) return;

			for (int i = 0; i < spreadMax; i++)
			{
				FTitleOutput[i] = FResultInput[i].Title;
			}
		}
	}
}
