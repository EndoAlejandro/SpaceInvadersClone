using SpaceInvaders.Input;
using UnityEngine;

namespace SpaceInvaders
{
    public static class GameManager
    {
        private static InputReader _input;

        public static InputReader Input => _input ??= CreateInputReader();

        public static float LeftEdge => Camera.main?.ViewportToWorldPoint(Vector2.right * Constants.WIDTH_SAFE_SPACE).x ?? 0f;

        public static float RightEdge => Camera.main?.ViewportToWorldPoint(Vector2.right * (1f - Constants.WIDTH_SAFE_SPACE)).x ?? 0f;

        public static float ScreenHeight => Screen.height * Constants.HEIGHT_SAFE_SPACE;

        private static InputReader CreateInputReader()
        {
            var inputReader = new InputReader();
            inputReader.EnableMainInput();
            return inputReader;
        }
    }
}