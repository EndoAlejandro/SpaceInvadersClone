using UnityEngine;

namespace SpaceInvaders
{
    public static class GameManager
    {
        public static float ScreenWidth => Screen.width * Constants.WIDTH_SAFE_SPACE;
        public static float ScreenHeight => Screen.height * Constants.HEIGHT_SAFE_SPACE;
    }
}