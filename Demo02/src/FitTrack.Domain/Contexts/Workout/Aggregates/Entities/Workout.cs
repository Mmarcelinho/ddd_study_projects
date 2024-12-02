using FitTrack.Domain.Contexts.Workout.Abstractions;
using FitTrack.Domain.Contexts.Workout.Errors;
using FitTrack.Domain.Notifications;
using FitTrack.Domain.Validations;
using FitTrack.Domain.Validations.Interfaces;

namespace FitTrack.Domain.Contexts.Workout.Aggregates.Entities;

public class Workout : Entity, IContract
{
    private readonly List<Exercise> _exercises;

    private Workout(string name, DateTime date, TimeSpan duration, Guid userId)
    {
        Name = name;
        Date = date;
        Duration = duration;
        UserId = userId;
        _exercises = [];
    }

    public string Name { get; private set; }

    public DateTime Date { get; private set; }

    public TimeSpan Duration { get; private set; }

    public Guid UserId { get; private set; }

    public IReadOnlyCollection<Exercise> Exercises => _exercises.AsReadOnly();

    public override bool Validate()
    {
        var contracts = new ContractValidations<Workout>()
            .NameIsOk(Name, 3, 30, DomainErrors.WORKOUT_NAME, nameof(Name));

        SetNotificationList(contracts.Notifications as List<Notification>);
        return contracts.IsValid();
    }

    public static Workout Create(string name, DateTime date, TimeSpan duration, Guid userId) => new(name, date, duration, userId);

    public void AddExercise(Exercise exercise) => _exercises.Add(exercise);
}
