using System;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace SpaceInvaders.Core
{
    [Serializable]
    public class AudioData
    {
        [field: SerializeField] public AudioMixerGroup AudioMixerGroup { get; private set; }

        [Range(0f, 1f)] [SerializeField] private float _maxPitchVariation;

        [Space]
        [SerializeField] private AudioClip[] _audioClip;

        private int _previousIndex;

        public AudioClip GetClip()
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