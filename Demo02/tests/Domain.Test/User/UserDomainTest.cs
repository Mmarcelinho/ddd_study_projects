using System.ComponentModel.DataAnnotations;
using FitTrack.Domain.Contexts.User.Aggregates.ValueObjects;
using FitTrack.Domain.Contexts.User.Errors;
using FitTrack.Domain.Contexts.User.Services;
using FluentAssertions;

namespace Domain.Test.User;

public class UserDomainTest
{
    [Fact]
    public void Create_ValidParameters_ShouldReturnValidUser()
    {
        // Arrange
        var name = Name.Create("user", "test");
        var email = Email.Create("usertest@example.com");

        // Act
        var user = FitTrack.Domain.Contexts.User.Aggregates.Entities.User.Create(name, email);

        // Assert
        user.Should().NotBeNull();
        user.Name.Should().Be(name);
        user.Email.Should().Be(email);
        user.Follower.Should().BeEmpty();
        user.Following.Should().BeEmpty();
        user.ActivitiesFollowed.Should().BeEmpty();
        user.Activities.Should().BeEmpty();
    }

    [Fact]
    public void FollowUser_ValidUser_ShouldAddToFollowingList()
    {
        // Arrange
        var followUserService = new FollowUserService();
        var name = Name.Create("user", "test1");
        var email = Email.Create("usertest1@example.com");
        var firstUser = FitTrack.Domain.Contexts.User.Aggregates.Entities.User.Create(name, email);
        var secondUser = FitTrack.Domain.Contexts.User.Aggregates.Entities.User.Create(Name.Create("user", "test2"), Email.Create("usertest2@example.com"));

        // Act
        followUserService.FollowUser(firstUser, secondUser);

        // Assert
        firstUser.Following.Should().Contain(secondUser);
        secondUser.Follower.Should().Contain(firstUser);
    }

    [Fact]
    public void FollowUser_SameUser_ShouldThrowValidationException()
    {
        // Arrange
        var followUserService = new FollowUserService();
        var name = Name.Create("user", "test");
        var email = Email.Create("usertest@example.com");
        var user = FitTrack.Domain.Contexts.User.Aggregates.Entities.User.Create(name, email);

        // Act
        Action action = () => followUserService.FollowUser(user, user);

        // Assert
        action.Should().Throw<ValidationException>()
            .WithMessage(DomainErrors.USER_FOLLOW_YOURSELF);
    }
}
