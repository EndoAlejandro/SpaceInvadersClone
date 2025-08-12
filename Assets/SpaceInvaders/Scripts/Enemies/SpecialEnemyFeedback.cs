using SpaceInvaders.Core;
using UnityEngine;

namespace SpaceInvaders.Enemies
{
    /// <summary>
    /// Special enemy sound fx.
    /// </summary>
    public class SpecialEnemyFeedback : MonoBehaviour
    {
        [SerializeField] private AudioData _audioData;
        [SerializeField] private AudioSource _audioSource;

        private void OnEnable()
        {
            _audioSource.clip = _audioData.GetClip();
            _audioSource.pitch = _audioData.GetRandomPitch(1f);
            _audioSource.loop = true;
            _audioSource.Play();
        }

        private void OnDisable() => _audioSource.Stop();
    }
}