using FitTrack.Domain.Contexts.User.Abstractions;
using FitTrack.Domain.Contexts.User.Aggregates.ValueObjects;
using FitTrack.Domain.Contexts.User.Errors;
using FitTrack.Domain.Contexts.User.Exceptions;
using FitTrack.Domain.Notifications;
using FitTrack.Domain.Validations;
using FitTrack.Domain.Validations.Interfaces;

namespace FitTrack.Domain.Contexts.User.Aggregates.Entities;

public sealed class User : Entity, IContract
{
    private readonly List<User> _follower;
    private readonly List<User> _following;
    private readonly List<Activity.Aggregates.Entities.Activity> _activitiesFollowed;
    private readonly List<Activity.Aggregates.Entities.Activity> _activities;
    private readonly List<Workout.Aggregates.Entities.Workout> _workouts;

    private User(Name name, Email email)
    {
        Name = name;
        Email = email;
        _follower = [];
        _following = [];
        _activities = [];
        _activitiesFollowed = [];
        _workouts = [];
    }

    public Name Name { get; private set; }
    public Email Email { get; private set; }

    public IReadOnlyCollection<User> Follower => _follower.AsReadOnly();
    public IReadOnlyCollection<User> Following => _following.AsReadOnly();
    public IReadOnlyCollection<Activity.Aggregates.Entities.Activity> ActivitiesFollowed => _activitiesFollowed.AsReadOnly();
    public IReadOnlyCollection<Activity.Aggregates.Entities.Activity> Activities => _activities.AsReadOnly();
    public IReadOnlyCollection<Workout.Aggregates.Entities.Workout> Workouts => _workouts.AsReadOnly();

    public static User Create(Name name, Email email) => new(name, email);

    public override bool Validate()
    {
        var contracts = new ContractValidations<User>()
            .FirstNameIsOk(Name, 5, 20, DomainErrors.USER_FIRSTNAME, nameof(Name.FirstName))
            .LastNameIsOk(Name, 5, 20, DomainErrors.USER_LASTNAME, nameof(Name.FirstName))
            .EmailIsValid(Email, DomainErrors.USER_EMAIL, nameof(Email.Value));

        SetNotificationList(contracts.Notifications as List<Notification>);
        return contracts.IsValid();
    }

    public void FollowUser(User user) => AddFollowing(user);

    internal void AddFollowing(User userToFollow) => _following.Add(userToFollow);

    internal void AddFollower(User follower)
    {
        if (_following.Contains(follower))
            throw new ValidationException(DomainErrors.USER_ALREADY_FOLLOWED);

        _follower.Add(follower);
    }

    public void AddActivity(Activity.Aggregates.Entities.Activity activity)
    {
        if (_activities.Contains(activity))
            throw new ValidationException(DomainErrors.USER_ALREADY_CONTAIN_ACTIVITY);

        _activities.Add(activity);
    }

    internal void FollowActivity(Activity.Aggregates.Entities.Activity activity)
    {
        if (_activitiesFollowed.Contains(activity))
            throw new ValidationException(DomainErrors.USER_ALREADY_FOLLOW_ACTIVITY);

        _activitiesFollowed.Add(activity);
    }

    public void AddWorkout(Workout.Aggregates.Entities.Workout workout)
    {
        if (_workouts.Contains(workout))
            throw new ValidationException(DomainErrors.USER_ALREADY_CONTAIN_WORKOUT);

        _workouts.Add(workout);
    }
}
