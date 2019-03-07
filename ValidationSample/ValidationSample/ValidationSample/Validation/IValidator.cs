namespace ValidationSample.Validation
{
	public interface IValidator
	{
		string ValidationMessage { get; set; }
		bool IsValid(string value);
	}
}
