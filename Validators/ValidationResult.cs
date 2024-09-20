namespace WeatherAPI.Validators;

public class ValidationResult
{
    public bool IsValid { get; set; }
    public DateOnly Date { get; set; }
    public string? ErrorMessage { get; set; }
}