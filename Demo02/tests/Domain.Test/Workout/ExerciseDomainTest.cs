using FitTrack.Domain.Contexts.Workout.Aggregates.Enum;
using FluentAssertions;

namespace Domain.Test.Workout;

public class ExerciseDomainTest
{
    [Fact]
    public void Create_ValidParameters_ShouldReturnValidExercise()
    {
        // Arrange
        var workoutId = 1;
        var name = "Push Ups";
        var type = EExerciseType.Strength;
        var duration = TimeSpan.FromMinutes(5);
        var repetitions = 20;
        var sets = 3;

        // Act
        var exercise = FitTrack.Domain.Contexts.Workout.Aggregates.Entities.Exercise.Create(workoutId, name, type, duration, repetitions, sets);

        // Assert
        exercise.Should().NotBeNull();
        exercise.WorkoutId.Should().Be(workoutId);
        exercise.Name.Should().Be(name);
        exercise.Type.Should().Be(type);
        exercise.Duration.Should().Be(duration);
        exercise.Repetitions.Should().Be(repetitions);
        exercise.Sets.Should().Be(sets);
    }
}
