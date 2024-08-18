using FitTrack.Domain.Contexts.Activity.Errors;
using FitTrack.Domain.Contexts.User.Exceptions;

namespace FitTrack.Domain.Contexts.User.Services;

public class FollowActivityService
{
    public void FollowActivity(
        User.Aggregates.Entities.User user,
        Activity.Aggregates.Entities.Activity activity)
    {
        if (activity is null)
            throw new ValidationException(DomainErrors.ACTIVITY_NULL);

        user.FollowActivity(activity);
        activity.AddFollower(user);
    }
}
