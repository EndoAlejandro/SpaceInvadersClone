using UnityEngine;

namespace SpaceInvaders.Core
{
    public abstract class PoolableMonoBehaviour : MonoBehaviour
    {
        private Pool _pool;

        public void SetupPoolable(Pool pool)
        {
            _pool = pool;
            transform.SetParent(_pool.transform);
        }

        protected void ReturnToPool()
        {
            transform.SetParent(_pool.transform);
            gameObject.SetActive(false);
        }
    }
}