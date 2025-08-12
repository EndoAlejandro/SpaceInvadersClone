using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvaders.UI
{
    /// <summary>
    /// Initial game screen.
    /// </summary>
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _playButton;

        private void Start() => _playButton.onClick.AddListener(PlayButtonOnClick);

        private static void PlayButtonOnClick() => GameManager.StartGame();
    }
}