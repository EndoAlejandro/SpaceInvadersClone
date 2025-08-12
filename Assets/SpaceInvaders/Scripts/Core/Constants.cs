namespace SpaceInvaders
{
    /// <summary>
    /// Constants used across the project to avoid "magic" variables.
    /// </summary>
    public static class Constants
    {
        public const float WIDTH_SAFE_SPACE = .05f;
        public const float HEIGHT_SAFE_SPACE = .0f;
        public const float HEIGHT_DEATH_SPACE = .2f;
        public const float MIN_DIFFICULTY_SCALE = .4f;
        public const float TRANSITION_LOAD_TIME = 5f;
        public const float DEATH_TIME_TO_TRANSITION = 2f;

        public const int INITIAL_LEVEL = 1;
        public const int INITIAL_LIVES = 3;
        public const int LIVES_PER_WIN = 1;
        public const int LEVELS_PER_WIN = 1;
        public const int MAX_LEVEL = 10;

        public static readonly int[] SPECIAL_ENEMY_POINTS = { 50, 100, 150, 300 };
    }
}