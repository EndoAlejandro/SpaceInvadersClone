using UnityEngine;

namespace SpaceInvaders
{
    public class TransitionController : MonoBehaviour
    {
        [SerializeField] private float _loadTime = 5f;
        [SerializeField] private TransitionUI _transitionUI;

        private float _timer;

        private bool _ended;
        private float NormalizedTimer => _timer / _loadTime;

        private void Awake()
        {
            _transitionUI.Setup(GameManager.Level, GameManager.Lives);
        }

        private void Update()
        {
            if(_ended) return;
            _timer += Time.deltaTime;
            _transitionUI.UpdateProgressBar(NormalizedTimer);

            if (NormalizedTimer >= 1f)
            {
                _ended = true;
                GameManager.LoadGameScene();
            }
        }
    }
}