using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvaders.UI
{
    /// <summary>
    /// Pause Controller.
    /// </summary>
    public class PauseUI : MonoBehaviour
    {
        [SerializeField] private GameObject _container;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _mainMenuButton;

        private void Awake()
        {
            _container.SetActive(false);
        }

        private void Start()
        {
            GameManager.OnGamePaused += GameManagerOnGamePaused;
            _resumeButton.onClick.AddListener(GameManager.ResumeGame);
            _mainMenuButton.onClick.AddListener(GameManager.GoToMainMenu);
        }

        private void GameManagerOnGamePaused(bool isPaused) => _container.SetActive(isPaused);

        private void OnDestroy()
        {
            GameManager.OnGamePaused -= GameManagerOnGamePaused;
            _resumeButton.onClick.RemoveAllListeners();
            _mainMenuButton.onClick.RemoveAllListeners();
        }
    }
}