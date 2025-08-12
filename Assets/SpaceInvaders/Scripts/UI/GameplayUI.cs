using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvaders.UI
{
    public class GameplayUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _livesText;
        [SerializeField] private TMP_Text _levelText;

        [SerializeField] private Button _pauseButton;

        private void Start()
        {
            GameManager.OnScoreUpdated += GameManagerOnOnScoreUpdated;
            _pauseButton.onClick.AddListener(PauseButtonOnClick);
            
            GameManagerOnOnScoreUpdated(GameManager.Score);
            _livesText.SetText($"<sprite name=player> X {GameManager.Lives:00}");
            _levelText.SetText($"Level: {GameManager.Level:00}");
        }

        private void PauseButtonOnClick() => GameManager.PauseGame();

        private void GameManagerOnOnScoreUpdated(int score)
            => _scoreText.SetText($"Score: {score:00000}");

        private void OnDestroy()
        {
            GameManager.OnScoreUpdated -= GameManagerOnOnScoreUpdated;
        }
    }
}