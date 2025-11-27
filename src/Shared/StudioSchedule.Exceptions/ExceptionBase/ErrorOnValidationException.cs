namespace StudioSchedule.Exceptions;

public class ErrorOnValidationException : StudioScheduleException
{
    public IList<string> ErrorMessages { get; set; }

    public ErrorOnValidationException(IList<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
}