namespace WeatherAPI.Validators;

public class DateValidator(DateOnly startDate, DateOnly endDate)
{
    public ValidationResult Validate(string date)
    {
        if (!DateOnly.TryParse(date, out DateOnly parsedDate))
        {
            return new ValidationResult
            {
                IsValid = false,
                ErrorMessage = $"Invalid date format. Please use yyyy-MM-dd."
            };
        }
        
        if (parsedDate < startDate || parsedDate > endDate)
        {
            return new ValidationResult
            {
                IsValid = false,
                ErrorMessage = $"The date {date} is outside the valid range ({startDate} - {endDate})."
            };
        }

        return new ValidationResult { IsValid = true, Date = parsedDate};
    }
    
    
}
