namespace FitTrack.Domain.Contexts.Workout.Errors;

    public static class DomainErrors
    {
        public const string WORKOUT_EXERCISE_NULL = "The exercise cannot be null.";

        public const string WORKOUT_NAME = "The workout name must contain between 3 and 30 characters.";

        public const string WORKOUT_EXERCISE_NAME = "The exercise name must contain between 3 and 30 characters.";
    }
