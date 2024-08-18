using FitTrack.Domain.Contexts.Workout.Abstractions;
using FitTrack.Domain.Contexts.Workout.Aggregates.Enum;
using FitTrack.Domain.Contexts.Workout.Errors;
using FitTrack.Domain.Notifications;
using FitTrack.Domain.Validations;
using FitTrack.Domain.Validations.Interfaces;

namespace FitTrack.Domain.Contexts.Workout.Aggregates.Entities;

public class Exercise : Entity, IContract
{
    private Exercise(int workoutId, string name, EExerciseType type, TimeSpan duration, int repetitions, int sets)
    {
        WorkoutId = workoutId;
        Name = name;
        Type = type;
        Duration = duration;
        Repetitions = repetitions;
        Sets = sets;
    }

    public string Name { get; private set; }

    public EExerciseType Type { get; private set; }

    public TimeSpan Duration { get; private set; }

    public int Repetitions { get; private set; }

    public int Sets { get; private set; }

    public int WorkoutId { get; private set; }

    public override bool Validate()
    {
        var contracts = new ContractValidations<Exercise>()
            .NameIsOk(Name, 3, 30, DomainErrors.WORKOUT_EXERCISE_NAME, nameof(Name));

        SetNotificationList(contracts.Notifications as List<Notification>);
        return contracts.IsValid();
    }

    public static Exercise Create(int workoutId, string name, EExerciseType type, TimeSpan duration, int repetitions, int sets)
        => new(workoutId, name, type, duration, repetitions, sets);
}
