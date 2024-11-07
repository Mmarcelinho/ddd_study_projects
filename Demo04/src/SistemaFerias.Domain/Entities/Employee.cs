namespace SistemaFerias.Domain.Entities;

public sealed class Employee : User
{
    private readonly List<VacationRequest> _vacationRequests;

    public Employee(Email email, Name name, Password password, string role, DateTime startDate, Guid departmentId)
        : base(email, name, password, role)
    {
        StartDate = startDate;
        DepartmentId = departmentId;
        _vacationRequests = [];
    }

    public DateTime StartDate { get; private set; }

    public DateTime? LastVacationDate { get; private set; }

    public Guid DepartmentId { get; private set; }

    public ICollection<VacationRequest> VacationRequests => _vacationRequests.AsReadOnly();

    public void UpdateLastVacationDate(DateTime lastVacationDate) => LastVacationDate = lastVacationDate;

    public void AddVacationRequest(VacationRequest vacationRequest) => _vacationRequests.Add(vacationRequest);

    public static Employee Create(
        Email email,
        Name name,
        Password password,
        string role,
        DateTime startDate,
        Guid departmentId)
        => new(
        email,
        name,
        password,
        role,
        startDate,
        departmentId);

}
