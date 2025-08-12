using TMPro;
using UnityEngine;

namespace SpaceInvaders.UI
{
    public class GameplayUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _livesText;
        [SerializeField] private TMP_Text _levelText;

        private void Start()
        {
            GameManager.OnScoreUpdated += GameManagerOnOnScoreUpdated;
            
            _livesText.SetText($"<sprite name=player> X {GameManager.Lives:00}");
            _levelText.SetText($"Level: {GameManager.Level:00}");
        }

        private void GameManagerOnOnScoreUpdated(int score) 
            => _scoreText.SetText($"Score: {score:00000}");

        private void OnDestroy()
        {
            GameManager.OnScoreUpdated -= GameManagerOnOnScoreUpdated;
        }
    }
}