using System;
using Xamarin.Forms;

namespace ValidationSample.Validation
{
	public class ValidatableContainer : ContentView
	{
		private Label _errorLabel = new Label { TextColor = Color.Red, IsVisible = false };
		private StackLayout _containerStack = new StackLayout();

		public static readonly BindableProperty ValidatorProperty = BindableProperty.Create("Validator"
			, typeof(IValidator)
			, typeof(ValidatableContainer)
			, default(IValidator)
			, propertyChanged: OnValidatorChanged);

		private static void OnValidatorChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var control = bindable as ValidatableContainer;
			var validator = newValue as IValidator;
			control._errorLabel.SetBinding(Label.TextProperty
				, new Binding
				{
					Source = validator
				,
					Path = nameof(validator.ValidationMessage)
				});
		}

		public IValidator Validator
		{
			get { return (IValidator)GetValue(ValidatorProperty); }
			set { SetValue(ValidatorProperty, value); }
		}

		public static readonly BindableProperty ViewProperty
			= BindableProperty.Create("View"
				, typeof(View)
				, typeof(ValidatableContainer)
				, default(View)
				, propertyChanged: OnViewChanged);

		private static void OnViewChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var control = bindable as ValidatableContainer;
			var view = newValue as View;
			if (view == null) return;

			control.Initialize();
		}

		public View View
		{
			get { return (View)GetValue(ViewProperty); }
			set { SetValue(ViewProperty, value); }
		}

		public static readonly BindableProperty ValidationPropertyProperty = BindableProperty.Create("ValidationProperty"
			, typeof(string)
			, typeof(ValidatableContainer)
			, default(string));

		public string ValidationProperty
		{
			get { return (string)GetValue(ValidationPropertyProperty); }
			set { SetValue(ValidationPropertyProperty, value); }
		}

		private void Initialize()
		{
			_containerStack.Children.Add(View);
			_containerStack.Children.Add(_errorLabel);
			this.Content = _containerStack;

			View.Unfocused += (sender, args) =>
			{
				PerformValidation();
			};
			View.PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName == ValidationProperty)
					PerformValidation();
			};
		}

		private void PerformValidation()
		{
			var isValid = IsValid();
			ChangeState(isValid);
		}

		public bool IsValid()
		{
			string value = GetValidatablePropertyValue(View, ValidationProperty);
			return Validator.IsValid(value);
		}

		public void ChangeState(bool isValid)
		{
			_errorLabel.IsVisible = !isValid;
		}

		private string GetValidatablePropertyValue(View view, string validationProperty)
		{
			var propertyValue = view.GetType()
				.GetProperty(validationProperty)
				.GetValue(view)
				?.ToString();

			return propertyValue;
		}
	}
}
