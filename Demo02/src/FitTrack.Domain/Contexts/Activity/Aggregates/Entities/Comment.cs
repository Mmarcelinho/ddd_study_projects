using FitTrack.Domain.Contexts.Activity.Abstractions;
using FitTrack.Domain.Contexts.Activity.Errors;
using FitTrack.Domain.Notifications;
using FitTrack.Domain.Validations;
using FitTrack.Domain.Validations.Interfaces;

namespace FitTrack.Domain.Contexts.Activity.Aggregates.Entities;

public sealed class Comment : Entity, IContract
{
    private Comment(Guid activityId, Guid userId, string content)
    {
        ActivityId = activityId;
        UserId = userId;
        Content = content;
    }

    public Guid ActivityId { get; private set; }

    public Guid UserId { get; private set; }

    public string Content { get; private set; }

    public DateTime ModifiedAtUtc { get; private set; }

    public override bool Validate()
    {
        var contracts = new ContractValidations<Comment>()
            .ContentIsOk(Content, 1, 250, DomainErrors.ACTIVITY_CONTENT, nameof(Content));

        SetNotificationList(contracts.Notifications as List<Notification>);

        return contracts.IsValid();
    }

    public static Comment Create(Guid activityId, Guid userId, string content) => new(activityId, userId, content);
}
