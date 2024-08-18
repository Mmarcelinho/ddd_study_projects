using FitTrack.Domain.Contexts.Activity.Aggregates.Entities;
using FluentAssertions;

namespace Domain.Test.Activity;

public class ActivityDomainTest
{
    [Fact]
    public void Create_ValidParameters_ShouldReturnValidActivity()
    {
        // Arrange
        var name = "Morning Run";
        var distance = 5.0;
        var duration = TimeSpan.FromMinutes(30);
        var averagePace = 6.0;
        var averageHeartRate = 120.0;
        var userId = Guid.NewGuid();

        // Act
        var activity = FitTrack.Domain.Contexts.Activity.Aggregates.Entities.Activity.Create(name, distance, duration, averagePace, averageHeartRate, userId);

        // Assert
        activity.Should().NotBeNull();
        activity.Name.Should().Be(name);
        activity.Distance.Should().Be(distance);
        activity.Duration.Should().Be(duration);
        activity.AveragePace.Should().Be(averagePace);
        activity.AverageHeartRate.Should().Be(averageHeartRate);
        activity.UserId.Should().Be(userId);
        activity.Comments.Should().BeEmpty();
        activity.Followers.Should().BeEmpty();
    }

    [Fact]
    public void AddComment_ValidComment_ShouldAddToCommentsList()
    {
        // Arrange
        var activity = FitTrack.Domain.Contexts.Activity.Aggregates.Entities.Activity.Create("Morning Run", 5.0, TimeSpan.FromMinutes(30), 6.0, 120.0, Guid.NewGuid());
        var comment = Comment.Create(activity.Id, Guid.NewGuid(), "Great run!");

        // Act
        activity.AddComment(comment);

        // Assert
        activity.Comments.Should().Contain(comment);
    }
}
