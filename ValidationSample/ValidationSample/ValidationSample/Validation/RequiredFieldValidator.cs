using System;

namespace ValidationSample.Validation
{
	public class RequiredFieldValidator : IValidator
	{
		private readonly IValidator _otherValidator;

		public RequiredFieldValidator(string message) : this(message, null) { }

		public RequiredFieldValidator(string message, IValidator otherValiidator)
		{
			ValidationMessage = message;
			_otherValidator = otherValiidator;
		}

		public string ValidationMessage { get; set; }

		public bool IsValid(string value)
		{
			return !String.IsNullOrEmpty(value);
		}
	}
}
