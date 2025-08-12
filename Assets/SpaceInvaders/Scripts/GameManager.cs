using SpaceInvaders.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceInvaders
{
    public static class GameManager
    {
        private static InputReader _input;

        public static InputReader Input => _input ??= CreateInputReader();

        public static float LeftEdge => Camera.main?.ViewportToWorldPoint(Vector2.right * Constants.WIDTH_SAFE_SPACE).x ?? 0f;

        public static float RightEdge => Camera.main?.ViewportToWorldPoint(Vector2.right * (1f - Constants.WIDTH_SAFE_SPACE)).x ?? 0f;

        public static float TopEdge => Camera.main?.ViewportToWorldPoint(Vector2.up * (1f - Constants.HEIGHT_SAFE_SPACE)).y ?? 0f;

        public static float BottomEdge => Camera.main?.ViewportToWorldPoint(Vector2.up * Constants.HEIGHT_SAFE_SPACE).y ?? 0f;

        public static float ScreenHeight => Screen.height * Constants.HEIGHT_SAFE_SPACE;

        public static int Level { get; private set; }

        public static int Lives { get; private set; }

        public static int Score { get; private set; }


        private static InputReader CreateInputReader()
        {
            var inputReader = new InputReader();
            inputReader.EnableMainInput();
            return inputReader;
        }

        public static void IncreaseLevel() => Level++;

        private static void ResetGame()
        {
            Level = 1;
            Score = 0;
            Lives = 3;
        }

        public static void StartGame()
        {
            ResetGame();
            SceneManager.LoadScene("Transition");
        }

        public static void LoadGameScene()
        {
            SceneManager.LoadScene("Main");
        }

        public static void WinGame()
        {
            Level++;
            Lives++;
            SceneManager.LoadScene("Transition");
        }

        public static void LoseGame()
        {
            Lives--;
            SceneManager.LoadScene("Transition");
        }
    }
}