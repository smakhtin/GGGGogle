using System;
using Google.API.Search;
using VVVV.PluginInterfaces.V2;

namespace GGGGoogle
{
	public abstract class ResultProperty : IPluginEvaluate
	{
		[Input("Result")]
		protected ISpread<IWebResult> FResultInput;

		public virtual void Evaluate(int spreadMax)
		{
			throw new NotImplementedException();
		}
	}
}
