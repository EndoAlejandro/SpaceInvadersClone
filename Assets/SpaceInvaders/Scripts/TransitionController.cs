using SpaceInvaders.Core;
using SpaceInvaders.UI;
using UnityEngine;

namespace SpaceInvaders
{
    public class TransitionController : MonoBehaviour
    {
        [SerializeField] private float _loadTime = 5f;
        [SerializeField] private TransitionUI _transitionUI;
        
        [SerializeField] private AudioData _audioData;
        [SerializeField] private AudioSource _audioSource;

        private float _timer;

        private bool _ended;
        private float NormalizedTimer => _timer / _loadTime;

        private void Awake()
        {
            _transitionUI.Setup(GameManager.Level, GameManager.Lives);
            _audioSource.pitch = _audioData.GetRandomPitch(1f);
            _audioSource.PlayOneShot(_audioData.GetClip());
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