using FitTrack.Domain.Contexts.Activity.Abstractions;
using FitTrack.Domain.Contexts.Activity.Errors;
using FitTrack.Domain.Contexts.Activity.Exceptions;
using FitTrack.Domain.Notifications;
using FitTrack.Domain.Validations;
using FitTrack.Domain.Validations.Interfaces;

namespace FitTrack.Domain.Contexts.Activity.Aggregates.Entities;

public sealed class Activity : Entity, IContract
{
    private readonly List<Comment> _comments;
    private readonly List<User.Aggregates.Entities.User> _followers;

    private Activity(string name, double distance, TimeSpan duration, double averagePace, double averageHeartRate, Guid userId)
    {
        Name = name;
        Distance = distance;
        Duration = duration;
        AveragePace = averagePace;
        AverageHeartRate = averageHeartRate;
        UserId = userId;
        _comments = [];
        _followers = [];
    }

    public string Name { get; private set; }
    public double Distance { get; private set; }
    public TimeSpan Duration { get; private set; }
    public double AveragePace { get; private set; }
    public double AverageHeartRate { get; private set; }
    public Guid UserId { get; private set; }

    public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();
    public IReadOnlyCollection<User.Aggregates.Entities.User> Followers => _followers.AsReadOnly();

    public override bool Validate()
    {
        var contracts = new ContractValidations<Activity>()
            .NameIsOk(Name, 3, 30, DomainErrors.ACTIVITY_NAME, nameof(Name));

        SetNotificationList(contracts.Notifications as List<Notification>);
        return contracts.IsValid();
    }

    public static Activity Create(string name, double distance, TimeSpan duration, double averagePace, double averageHeartRate, Guid userId)
    {
        if (distance <= 0 || duration <= TimeSpan.Zero || averagePace <= 0 || averageHeartRate <= 0)
            throw new ValidationException(DomainErrors.ACTIVITY_POSITIVE_VALUES);

        return new(name, distance, duration, averagePace, averageHeartRate, userId);
    }

    public void AddComment(Comment comment)
    {
        if (comment.ActivityId != Id)
            throw new ValidationException(DomainErrors.ACTIVITY_COMMENT_INVALID);

        _comments.Add(comment);
    }

    internal void AddFollower(User.Aggregates.Entities.User user)
    {
        if (_followers.Contains(user))
            throw new ValidationException(DomainErrors.ACTIVITY_ALREADY_FOLLOWER);

        _followers.Add(user);
    }
}
