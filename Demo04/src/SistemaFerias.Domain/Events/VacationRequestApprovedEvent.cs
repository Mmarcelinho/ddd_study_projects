namespace SistemaFerias.Domain.Events;

public sealed record VacationRequestApprovedEvent(VacationRequest VacationRequest) : IDomainEvent;