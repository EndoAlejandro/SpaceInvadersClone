using System;
using SpaceInvaders.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceInvaders
{
    /// <summary>
    /// Global game context and manager.
    /// </summary>
    public static class GameManager
    {
        // Events.
        public static event Action<int> OnScoreUpdated;

        public static event Action<bool> OnGamePaused;

        // Input.
        private static InputReader _input;

        public static InputReader Input => _input ??= CreateInputReader();

        // Border check.
        public static float LeftEdge => Camera.main?.ViewportToWorldPoint(Vector2.right * Constants.WIDTH_SAFE_SPACE).x ?? 0f;

        public static float RightEdge => Camera.main?.ViewportToWorldPoint(Vector2.right * (1f - Constants.WIDTH_SAFE_SPACE)).x ?? 0f;

        public static float TopEdge => Camera.main?.ViewportToWorldPoint(Vector2.up * (1f - Constants.HEIGHT_SAFE_SPACE)).y ?? 0f;

        public static float BottomEdge => Camera.main?.ViewportToWorldPoint(Vector2.up * Constants.HEIGHT_SAFE_SPACE).y ?? 0f;

        public static float BottomEnemyLimit => Camera.main?.ViewportToWorldPoint(Vector2.up * Constants.HEIGHT_DEATH_SPACE).y ?? 0f;

        // Context.
        public static int Level { get; private set; }

        public static int Lives { get; private set; }

        public static int Score { get; private set; }

        public static float NormalizedLevel => Level / (float)Constants.MAX_LEVEL;

        /// <summary>
        /// Simple in-memory max score.
        /// </summary>
        public static int MaxScore
        {
            get => PlayerPrefs.GetInt("Score", 0);
            private set => PlayerPrefs.SetInt("Score", value);
        }

        /// <summary>
        /// Initialize Input reader.
        /// </summary>
        /// <returns>InputReader instance.</returns>
        private static InputReader CreateInputReader()
        {
            var inputReader = new InputReader();
            inputReader.EnableMainInput();
            return inputReader;
        }

        /// <summary>
        /// Set context to it's default value.
        /// </summary>
        private static void ResetGame()
        {
            Level = Constants.INITIAL_LEVEL;
            Score = 0;
            Lives = Constants.INITIAL_LIVES;
            ResumeGame();
        }

        public static void GoToMainMenu() => SceneManager.LoadScene("Menu");

        /// <summary>
        /// Starts a new game sesion.
        /// </summary>
        public static void StartGame()
        {
            ResetGame();
            SceneManager.LoadScene("Transition");
        }

        public static void LoadGameplayScene() => SceneManager.LoadScene("Main");


        /// <summary>
        /// If all standard enemies are death, go to next level.
        /// </summary>
        public static void WinGame()
        {
            Level += Constants.LEVELS_PER_WIN;
            Lives += Constants.LIVES_PER_WIN;

            // Check if level is greater than the max level, go to GameOver if so.
            SceneManager.LoadScene(Level > Constants.MAX_LEVEL ? "GameOver" : "Transition");
        }

        /// <summary>
        /// If enemies reach the bottom part or a projectile hits the player,
        /// Lose a life and restart the same level.
        /// </summary>
        public static void LoseGame()
        {
            Lives--;
            // Check if the player already lose.
            if (Lives <= 0)
            {
                // Save the score if new record reached.
                if (Score > MaxScore) MaxScore = Score;
                
                SceneManager.LoadScene("GameOver");
            }
            else
            {
                SceneManager.LoadScene("Transition");
            }
        }

        public static void AddScore(int score)
        {
            Score += score;
            OnScoreUpdated?.Invoke(Score);
        }

        public static void PauseGame()
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(true);
        }

        public static void ResumeGame()
        {
            Time.timeScale = 1f;
            OnGamePaused?.Invoke(false);
        }
    }
}