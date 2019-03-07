using System;
using ValidationSample.Validation;
using Xamarin.Forms;

namespace ValidationSample
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
			RequiredFieldValidator = new Validation.RequiredFieldValidator("Feild should not be empty");
			this.BindingContext = this;
		}

		public Validation.RequiredFieldValidator RequiredFieldValidator { get; set; }

		private void Button_Clicked(object sender, EventArgs e)
		{
			var isValid = Container.IsValid();
		}
	}
}
