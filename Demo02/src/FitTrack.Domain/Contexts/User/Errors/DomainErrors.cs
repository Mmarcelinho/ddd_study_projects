namespace FitTrack.Domain.Contexts.User.Errors;

    public static class DomainErrors
    {
        public const string USER_FIRSTNAME = "The first name must contain between 5 and 20 characters";

        public const string USER_LASTNAME = "The first name must contain between 5 and 20 characters";

        public const string USER_EMAIL = "The email address must be valid.";

        public const string USER_NULL = "User cannot be null.";

        public const string USER_FOLLOW_YOURSELF = "Cannot follow yourself.";

        public const string USER_ALREADY_FOLLOWED = "User is already being followed.";

        public const string USER_ALREADY_FOLLOWER = "User is already a follower.";

        public const string USER_ALREADY_CONTAIN_ACTIVITY = "The user is already contain this activity.";

        public const string USER_ALREADY_FOLLOW_ACTIVITY = "The user is already follow this activity.";

        public const string USER_ALREADY_CONTAIN_WORKOUT = "The user is already contain this workout.";
    }
