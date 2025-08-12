using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceInvaders.Core
{
    [Serializable]
    public class AudioData
    {
        [SerializeField] private AudioClip[] _audioClip;
        [Range(0f, 1f)] [SerializeField] private float _maxPitchVariation;

        private int _previousIndex;

        public AudioClip GetSound()
        {
            if (_audioClip.Length == 0) return null;
            if (_audioClip.Length == 1) return _audioClip[0];

            var randomIndex = Random.Range(0, _audioClip.Length);

            if (_previousIndex == randomIndex)
            {
                randomIndex = (randomIndex + 1) % _audioClip.Length;
            }

            _previousIndex = randomIndex;
            return _audioClip[randomIndex];
        }

        public float GetRandomPitch(float initialPitch)
            => initialPitch + Random.Range(-_maxPitchVariation, _maxPitchVariation);
    }
}