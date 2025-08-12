using System.Collections;
using UnityEngine;

namespace SpaceInvaders.Core
{
    /// <summary>
    /// Fx prefab to play any Fx.
    /// </summary>
    public class PoolableFx : PoolableMonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private AudioSource _audioSource;

        private float _initialPitch;

        private void Awake()
        {
            _audioSource ??= GetComponent<AudioSource>();
            _initialPitch = _audioSource.pitch;
        }

        /// <summary>
        /// Plays any given Fx.
        /// </summary>
        /// <param name="position">Spawn position of the fx.</param>
        /// <param name="audioData">Audio Information. Can be null.</param>
        /// <param name="visuals">Visual Information. Can be null.</param>
        public void PlayFx(Vector3 position, AudioData audioData = null, Sprite visuals = null)
        {
            transform.position = position;
            PlayAudio(audioData, out AudioClip clip);
            PlayVisuals(visuals);
            StartCoroutine(ReturnToPoolAsync(clip?.length ?? 1f));
        }

        /// <summary>
        /// Independent call for visual Fx.
        /// </summary>
        private void PlayVisuals(Sprite visuals)
        {
            _spriteRenderer ??= GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = visuals;
        }
        
        /// <summary>
        /// Independent call for audio Fx.
        /// </summary>
        private void PlayAudio(AudioData audioData, out AudioClip clip)
        {
            if (audioData == null)
            {
                clip = null;
                return;
            }

            _audioSource ??= GetComponent<AudioSource>();

            clip = audioData.GetClip();
            _audioSource.loop = false;
            _audioSource.pitch = audioData.GetRandomPitch(_initialPitch);
            _audioSource.outputAudioMixerGroup = audioData.AudioMixerGroup ?? _audioSource.outputAudioMixerGroup;
            _audioSource.PlayOneShot(clip);
        }

        /// <summary>
        /// After wait time, sends back the fx to the pool.
        /// </summary>
        /// <param name="duration">Duration before returning to pool.</param>
        private IEnumerator ReturnToPoolAsync(float duration)
        {
            yield return new WaitForSeconds(duration);
            ReturnToPool();
        }
    }
}