using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SpaceInvaders.Core
{
    /// <summary>
    /// Handles all feedback calls.
    /// </summary>
    public class FeedbackManager : MonoBehaviour
    {
        // Singleton for simplicity.
        public static FeedbackManager Instance { get; private set; }

        [SerializeField] private int _initialPoolSize = 5;
        [SerializeField] protected PoolableFx _poolableFx;
        [SerializeField] private AudioData _buttonClickAudio;

        private const float InitialPitch = 1f;
        private Selectable _currentSelected;

        private Pool _pool;

        /// <summary>
        /// Singleton Check.
        /// </summary>
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        /// <summary>
        /// Creates the pool.
        /// </summary>
        private void Start() => 
            _pool ??= Pool.CreatePool("PoolFx", _initialPoolSize, _poolableFx);


        /// <summary>
        /// Plays sfx each time any button is clicked.
        /// </summary>
        private void Update()
        {
            // Checks if any object is selected.
            GameObject selectedObject = EventSystem.current.currentSelectedGameObject;

            if (selectedObject == null) return;
            
            // Check if object is Selectable and is selected.
            var selected = selectedObject.GetComponent<Selectable>();
            if (selected == null || selected == _currentSelected) return;
            
            // Store previous selected to avoid double call.
            _currentSelected = selected;

            // Play the Sfx.
            if (_buttonClickAudio != null)
                PlayFx(Vector3.zero, _buttonClickAudio);
        }

        /// <summary>
        /// Plays any given Fx.
        /// </summary>
        /// <param name="position">Spawn position of the fx.</param>
        /// <param name="audioData">Audio Information. Can be null.</param>
        /// <param name="sprite">Visual Information. Can be null.</param>
        public void PlayFx(Vector3 position, AudioData audioData = null, Sprite sprite = null)
        {
            var pooledFx = _pool.PoolObject<PoolableFx>();
            pooledFx.PlayFx(position, audioData, sprite);
        }
    }
}