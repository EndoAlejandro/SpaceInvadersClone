using System;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace SpaceInvaders.Core
{
    /// <summary>
    /// Clase used to store different audio clips and variable pitch.
    /// It helps to play a difference clip each time.
    /// </summary>
    [Serializable]
    public class AudioData
    {
        [field: SerializeField] public AudioMixerGroup AudioMixerGroup { get; private set; }

        [Range(0f, 1f)] [SerializeField] private float _maxPitchVariation;

        [Space]
        [SerializeField] private AudioClip[] _audioClip;

        private int _previousIndex;

        /// <summary>
        /// Get random clip in the audio clip collection.
        /// </summary>
        /// <returns>Audio clip or null.</returns>
        public AudioClip GetClip()
        {
            if (_audioClip.Length == 0) return null;

            // Avoid calculating random if there is only one clip.
            if (_audioClip.Length == 1) return _audioClip[0];

            var randomIndex = Random.Range(0, _audioClip.Length);

            // If the randomly selected index is repeated, select the next one.
            if (_previousIndex == randomIndex)
            {
                randomIndex = (randomIndex + 1) % _audioClip.Length;
            }

            // Store previous index.
            _previousIndex = randomIndex;
            return _audioClip[randomIndex];
        }

        /// <summary>
        /// Get randomized pitch to help the clips to sound different each play.
        /// </summary>
        /// <param name="initialPitch">The result will be calculated around this value.</param>
        /// <returns>Randomized pitch inside range.</returns>
        public float GetRandomPitch(float initialPitch = 1f)
            => initialPitch + Random.Range(-_maxPitchVariation, _maxPitchVariation);
    }
}