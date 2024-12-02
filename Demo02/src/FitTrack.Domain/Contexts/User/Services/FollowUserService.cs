using System.ComponentModel.DataAnnotations;
using FitTrack.Domain.Contexts.User.Errors;

namespace FitTrack.Domain.Contexts.User.Services;

public class FollowUserService
{
    public void FollowUser(
        Aggregates.Entities.User follower,
        Aggregates.Entities.User userToFollow)
    {
        if (userToFollow == null)
            throw new ValidationException(DomainErrors.USER_NULL);

        if (follower.Id == userToFollow.Id)
            throw new ValidationException(DomainErrors.USER_FOLLOW_YOURSELF);

        if (follower.Following.Contains(userToFollow))
            throw new ValidationException(DomainErrors.USER_ALREADY_FOLLOWED);

        follower.AddFollowing(userToFollow);
        userToFollow.AddFollower(follower);
    }
}
