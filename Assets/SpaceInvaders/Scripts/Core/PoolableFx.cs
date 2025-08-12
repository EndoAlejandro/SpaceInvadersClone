using System.Collections;
using UnityEngine;

namespace SpaceInvaders.Core
{
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

        public void PlayFx(Vector3 position, AudioData audioData = null, Sprite visuals = null)
        {
            transform.position = position;
            PlayAudio(audioData, out AudioClip clip);
            PlayVisuals(visuals);
            StartCoroutine(ReturnToPoolAsync(clip?.length ?? 1f));
        }

        private void PlayVisuals(Sprite visuals)
        {
            _spriteRenderer ??= GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = visuals;
        }

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

        private IEnumerator ReturnToPoolAsync(float duration)
        {
            yield return new WaitForSeconds(duration);
            ReturnToPool();
        }
    }
}