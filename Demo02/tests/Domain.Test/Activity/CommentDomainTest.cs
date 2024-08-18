using FitTrack.Domain.Contexts.Activity.Aggregates.Entities;
using FluentAssertions;

namespace Domain.Test.Activity;

public class CommentDomainTest
{
    [Fact]
    public void Create_ValidParameters_ShouldReturnValidComment()
    {
        // Arrange
        var activityId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var content = "Nice workout!";

        // Act
        var comment = Comment.Create(activityId, userId, content);

        // Assert
        comment.Should().NotBeNull();
        comment.ActivityId.Should().Be(activityId);
        comment.UserId.Should().Be(userId);
        comment.Content.Should().Be(content);
    }
}
