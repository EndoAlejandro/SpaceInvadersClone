using SpaceInvaders.Input;
using UnityEngine;

namespace SpaceInvaders
{
    public static class GameManager
    {
        private static InputReader _input;
        public static InputReader Input => _input ??= CreateInputReader();
        public static float ScreenWidth => Screen.width * Constants.WIDTH_SAFE_SPACE;
        public static float ScreenHeight => Screen.height * Constants.HEIGHT_SAFE_SPACE;

        private static InputReader CreateInputReader()
        {
            var inputReader = new InputReader();
            inputReader.EnableMainInput();
            return inputReader;
        }
    }
}