namespace SistemaFerias.Domain.Errors;

public static class DomainErrors
{
    public const string NAME_EMPTY = "Username cannot be empty or whitespace.";

    public const string EMAIL_EMPTY = "Email cannot be empty or whitespace.";

    public const string EMAIL_INVALID = "Invalid email format.";

    public const string PASSWORD_EMPTY = "Password cannot be null or empty.";

    public const string PASSWORD_TOO_SHORT = "Password must be at least 6 characters long.";

    public const string EMPLOYEE_NOT_ELEGIBLE = "Employee is not elegible for vacation.";

    public const string VACATION_ALREADY_REVIEWED = "Vacation request already reviewed.";
}
