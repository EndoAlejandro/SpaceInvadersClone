using SpaceInvaders.Core;
using UnityEngine;

namespace SpaceInvaders.Enemies
{
    public class SpecialEnemyFeedback : BaseFeedback
    {
        [SerializeField] private AudioData _audioData;

        private void OnEnable()
        {
            _audioSource.loop = true;
            _audioSource.playOnAwake = true;
            PlaySound(_audioData);
        }
    }
}