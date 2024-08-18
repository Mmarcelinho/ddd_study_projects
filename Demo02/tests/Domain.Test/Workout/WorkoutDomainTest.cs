using FitTrack.Domain.Contexts.Workout.Aggregates.Entities;
using FitTrack.Domain.Contexts.Workout.Aggregates.Enum;
using FluentAssertions;

namespace Domain.Test.Workout;

public class WorkoutDomainTest
{
    [Fact]
    public void Create_ValidParameters_ShouldReturnValidWorkout()
    {
        // Arrange
        var name = "Leg Day";
        var date = DateTime.UtcNow;
        var duration = TimeSpan.FromHours(1);
        var userId = Guid.NewGuid();

        // Act
        var workout = FitTrack.Domain.Contexts.Workout.Aggregates.Entities.Workout.Create(name, date, duration, userId);

        // Assert
        workout.Should().NotBeNull();
        workout.Name.Should().Be(name);
        workout.Date.Should().Be(date);
        workout.Duration.Should().Be(duration);
        workout.UserId.Should().Be(userId);
        workout.Exercises.Should().BeEmpty();
    }

    [Fact]
    public void AddExercise_ValidExercise_ShouldAddToExerciseList()
    {
        // Arrange
        var workout = FitTrack.Domain.Contexts.Workout.Aggregates.Entities.Workout.Create("Leg Day", DateTime.UtcNow, TimeSpan.FromHours(1), Guid.NewGuid());
        var exercise = Exercise.Create(1, "Squats", EExerciseType.Strength, TimeSpan.FromMinutes(10), 10, 3);

        // Act
        workout.AddExercise(exercise);

        // Assert
        workout.Exercises.Should().Contain(exercise);
    }
}
