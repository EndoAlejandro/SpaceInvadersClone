using SpaceInvaders.Core;
using UnityEngine;

namespace SpaceInvaders.Enemies
{
    public class FeedbackManager : MonoBehaviour
    {
        public static FeedbackManager Instance { get; private set; }

        [SerializeField] private int _initialPoolSize = 5;
        [SerializeField] protected PoolableFx _poolableFx;

        private const float InitialPitch = 1f;

        private static Pool _pool;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start() =>
            _pool ??= Pool.CreatePool("PoolFx", _initialPoolSize, _poolableFx);

        public void PlayFx(Vector3 position ,AudioData audioData = null, Sprite sprite = null)
        {
            var pooledFx = _pool.PoolObject<PoolableFx>();
            pooledFx.PlayFx(position,audioData, sprite);
        }
    }
}