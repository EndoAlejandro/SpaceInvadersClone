using SpaceInvaders.Core;
using UnityEngine;

namespace SpaceInvaders.Enemies
{
    public abstract class BaseFeedback : MonoBehaviour
    {
        [SerializeField] protected AudioSource _audioSource;

        private float _initialPitch;

        protected virtual void Awake() => _initialPitch = _audioSource.pitch;

        protected void PlaySound(AudioData audioData)
        {
            _audioSource.Stop();
            _audioSource.clip = audioData.GetSound();
            _audioSource.pitch = audioData.GetRandomPitch(_initialPitch);
            _audioSource.Play();
        }
    }
}