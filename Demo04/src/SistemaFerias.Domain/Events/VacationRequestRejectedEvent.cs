namespace SistemaFerias.Domain.Events;

public sealed record VacationRequestRejectedEvent(VacationRequest VacationRequest) : IDomainEvent;
   