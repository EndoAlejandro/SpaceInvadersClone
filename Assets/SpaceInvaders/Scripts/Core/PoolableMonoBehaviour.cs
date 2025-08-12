using UnityEngine;

namespace SpaceInvaders.Core
{
    /// <summary>
    /// Base class for Object pooling.
    /// </summary>
    public abstract class PoolableMonoBehaviour : MonoBehaviour
    {
        private Pool _pool;

        /// <summary>
        /// Always initialize the object when pooled.
        /// </summary>
        /// <param name="pool"></param>
        public void SetupPoolable(Pool pool)
        {
            _pool = pool;
            transform.SetParent(_pool.transform);
        }

        /// <summary>
        /// When disabled, return to the pool and deactivate itself.
        /// </summary>
        protected void ReturnToPool()
        {
            transform.SetParent(_pool.transform);
            gameObject.SetActive(false);
        }
    }
}