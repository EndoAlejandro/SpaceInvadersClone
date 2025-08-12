using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvaders.UI
{
    public class TransitionUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _transitionText;
        [SerializeField] private TMP_Text _livesText;
        [SerializeField] private Image _progressBar;

        public void Setup(int level, int lives)
        {
            _transitionText.SetText($"Space Invaders\n\nLevel - {level:00}");
            _livesText.SetText($"<sprite name=player> x {lives:00}");
        }

        public void UpdateProgressBar(float normalizedProgress) => 
            _progressBar.fillAmount = normalizedProgress;
    }
}