using Xamarin.Forms;

namespace ValidationSample.Validation
{
	public static class ValidationHelper
	{
		public static bool IsValid(this Layout<View> layout)
		{
			var result = true;
			foreach (var view in layout.Children)
			{
				if (view is Layout<View>)
					IsValid(view as Layout<View>);

				if (view is ValidatableContainer)
				{
					var validatableContainer = view as ValidatableContainer;
					var isvalid = validatableContainer.IsValid();
					validatableContainer.ChangeState(isvalid);
					if (result)
						result = false;
				}
			}
			return result;
		}
	}
}
