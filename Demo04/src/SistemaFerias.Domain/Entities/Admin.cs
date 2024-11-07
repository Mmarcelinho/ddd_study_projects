namespace SistemaFerias.Domain.Entities;

public sealed class Admin : User
{
    private readonly List<VacationRequest> _reviewedVacationRequests;
    private Admin(Email email, Name name, Password password, string role, string position, Guid departmentId) : base(email, name, password, role)
    {
        Position = position;
        DepartmentId = departmentId;
        _reviewedVacationRequests = [];
    }

    public string Position { get; private set; }

    public Guid DepartmentId { get; private set; }

    public ICollection<VacationRequest> ReviewedVacationRequests => _reviewedVacationRequests.AsReadOnly();

    public static Admin Create(
        Email email,
        Name name,
        Password password,
        string role,
        string position,
        Guid departmentId)
        => new(
        email,
        name,
        password,
        role,
        position,
        departmentId);
}
