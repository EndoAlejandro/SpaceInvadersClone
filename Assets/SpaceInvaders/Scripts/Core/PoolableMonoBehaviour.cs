using UnityEngine;

namespace SpaceInvaders.Core
{
    public abstract class PoolableMonoBehaviour : MonoBehaviour
    {
        private Transform _root;

        public void SetupRoot(Transform root)
        {
            _root = root;
            transform.SetParent(_root);
        }

        public void ReturnToPool()
        {
            transform.SetParent(_root);
            gameObject.SetActive(false);
        }
    }
}