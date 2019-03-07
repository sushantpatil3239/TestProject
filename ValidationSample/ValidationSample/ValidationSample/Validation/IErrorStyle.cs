using Xamarin.Forms;

namespace ValidationSample.Validation
{
	public interface IErrorStyle
	{
		void ShowError(View view, string errorMessage);
		void RemoveError(View view);
	}
}
