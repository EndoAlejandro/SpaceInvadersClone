using System;
using SpaceInvaders.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SpaceInvaders.Enemies
{
    public class FeedbackManager : MonoBehaviour
    {
        public static FeedbackManager Instance { get; private set; }

        [SerializeField] private int _initialPoolSize = 5;
        [SerializeField] protected PoolableFx _poolableFx;

        private const float InitialPitch = 1f;

        private Pool _pool;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            _pool ??= Pool.CreatePool("PoolFx", _initialPoolSize, _poolableFx);
        }

        [SerializeField] private AudioData _buttonClickAudio;
        private Selectable _currentSelected;

        private void Update()
        {
            GameObject selectedObject = EventSystem.current.currentSelectedGameObject;

            if (selectedObject == null) return;
            
            var selected = selectedObject.GetComponent<Selectable>();
            if (selected == null || selected == _currentSelected) return;
            
            _currentSelected = selected;

            if (_buttonClickAudio != null)
            {
                PlayFx(Vector3.zero, _buttonClickAudio);
            }
        }

        public void PlayFx(Vector3 position, AudioData audioData = null, Sprite sprite = null)
        {
            var pooledFx = _pool.PoolObject<PoolableFx>();
            pooledFx.PlayFx(position, audioData, sprite);
        }
    }
}