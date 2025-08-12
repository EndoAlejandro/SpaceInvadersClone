using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvaders
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _maxScoreText;
        [SerializeField] private Button _mainMenuButton;

        private void Start()
        {
            _levelText.SetText($"Level reached: <b>{GameManager.Level:00}</b>");
            _scoreText.SetText($"Score: <b>{GameManager.Score:0000}</b>");
            _maxScoreText.SetText($"Max Score: <b>{GameManager.MaxScore:0000}</b>");
            _mainMenuButton.onClick.AddListener(MainButtonOnClick);
        }

        private void MainButtonOnClick() => GameManager.GoToMainMenu();
    }
}