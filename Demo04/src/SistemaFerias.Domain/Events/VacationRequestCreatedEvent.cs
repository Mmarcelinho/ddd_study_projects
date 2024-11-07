namespace SistemaFerias.Domain.Events;

public sealed record VacationRequestCreatedEvent(VacationRequest VacationRequest) : IDomainEvent;
   