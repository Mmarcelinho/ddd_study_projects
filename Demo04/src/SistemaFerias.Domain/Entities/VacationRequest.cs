namespace SistemaFerias.Domain.Entities;

public sealed class VacationRequest : Entity
{
    private VacationRequest(DateTime requestDate, DateTime startDate, DateTime endDate, int days, EStatus status, Employee employee, Admin? admin)
    {
        RequestDate = requestDate;
        StartDate = startDate;
        EndDate = endDate;
        Days = days;
        Status = status;
        Employee = employee;
        Admin = admin;
    }

    public DateTime RequestDate { get; private set; }

    public DateTime StartDate { get; private set; }

    public DateTime EndDate { get; private set; }

    public int Days { get; private set; }

    public EStatus Status { get; private set; }

    public Employee Employee { get; private set; } = default!;

    public Admin? Admin { get; private set; }

    public static VacationRequest Create(DateTime startDate, int days, Employee employee)
    {
        ValidateHoliday(employee);
        return new VacationRequest(
            requestDate: DateTime.Today,
            startDate: startDate,
            endDate: startDate.AddDays(days),
            days: days,
            status: EStatus.Pending,
            employee: employee,
            admin: null);
    }

    public static void ValidateHoliday(Employee employee)
    {
        if (!ElegibleForVacation(employee.StartDate, employee.LastVacationDate))
            throw new DomainException(DomainErrors.EMPLOYEE_NOT_ELEGIBLE);

    }

    private static bool ElegibleForVacation(DateTime startDate, DateTime? lastVacationDate)
    {
        if (lastVacationDate.HasValue)
            return DateTime.Today.Subtract(lastVacationDate.Value).TotalDays >= 365;
        else
            return DateTime.Today.Subtract(startDate).TotalDays >= 365;

    }

    public void ReviewVacationRequest(Admin admin, EStatus status)
    {
        ValidateStatus();

        if (status == EStatus.Approved)
            Approve(admin);
        else
            Reject(admin);
    }

    private void Approve(Admin admin)
    {
        Status = EStatus.Approved;
        Admin = admin;
    }

    private void Reject(Admin admin)
    {
        Status = EStatus.Denied;
        Admin = admin;
    }

    private void ValidateStatus()
    {
        if (Status != EStatus.Pending)
            throw new DomainException(DomainErrors.VACATION_ALREADY_REVIEWED);
    }
}
