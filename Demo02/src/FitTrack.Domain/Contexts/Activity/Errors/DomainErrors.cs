namespace FitTrack.Domain.Contexts.Activity.Errors;

    public static class DomainErrors
    {
        public const string ACTIVITY_CONTENT = "The content must contain between 1 and 250 characters.";

        public const string ACTIVITY_NAME = "The name must contain between 3 and 30 characters.";

        public const string ACTIVITY_POSITIVE_VALUES = "The value must be positive.";

        public const string ACTIVITY_COMMENT_INVALID = "comment does not belong to this activity.";

        public const string ACTIVITY_ALREADY_FOLLOWER = "User is already a follower.";

        public const string ACTIVITY_NULL = "The activity cannot be null.";

        public const string ACTIVITY_ALREADY_COMMENT = "The comment is already exist.";
    }
