using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpaceInvaders.Core
{
    /// <summary>
    /// Simple object pooling system.
    /// </summary>
    public class Pool : MonoBehaviour
    {
        /// <summary>
        /// Global collection of all instanced pools.
        /// </summary>
        private static Dictionary<Type, Pool> _pools;

        /// <summary>
        /// Local list of instanced poolable objects.
        /// </summary>
        private List<PoolableMonoBehaviour> _poolables;

        /// <summary>
        /// Prefab required to instantiate new poolables.
        /// </summary>
        private PoolableMonoBehaviour _prefab;

        private int _index = 0;

        /// <summary>
        /// Get an object from the pool. Creates a new one if all objects are currently active.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Returns an activated poolable object.</returns>
        public T PoolObject<T>() where T : PoolableMonoBehaviour
        {
            T pooled = null;

            if (_poolables[_index].gameObject.activeInHierarchy)
            {
                // If the next object is already active whe search for the next available.
                for (int i = 0; i < _poolables.Count; i++)
                {
                    if (_poolables[i].gameObject.activeInHierarchy) continue;
                    pooled = _poolables[i] as T;
                    _index = i;
                    break;
                }

                // Is no object was found, we create a new object and add it to the pool.
                if (pooled == null)
                {
                    pooled = CreatePoolable(_prefab as T, transform);
                    _poolables.Add(pooled);
                }
            }
            else
            {
                pooled = _poolables[_index] as T;
            }

            // Increment the index with auto reset.
            _index = (_index + 1) % _poolables.Count;
            // Activate the object before returning it.
            pooled?.gameObject.SetActive(true);
            return pooled;
        }

        /// <summary>
        /// Create pool if needed and return and instance of the required pool. 
        /// </summary>
        /// <param name="name">Pool container name.</param>
        /// <param name="size">Initial pool size.</param>
        /// <param name="prefab">Poolable Prefab.</param>
        /// <typeparam name="T">Prefab Type.</typeparam>
        /// <returns></returns>
        public static Pool CreatePool<T>(string name, int size, T prefab) where T : PoolableMonoBehaviour
        {
            _pools ??= new Dictionary<Type, Pool>();

            if (_pools.ContainsKey(typeof(T)))
            {
                return _pools[typeof(T)];
            }

            var poolObject = new GameObject($"{name} Pool");
            var pool = poolObject.AddComponent<Pool>();
            pool._prefab = prefab;
            pool._poolables = new List<PoolableMonoBehaviour>();

            for (int i = 0; i < size; i++)
            {
                var instance = CreatePoolable(prefab, poolObject.transform);
                pool._poolables.Add(instance);
                instance.gameObject.SetActive(false);
            }

            _pools.Add(typeof(T), pool);
            return pool;
        }

        /// <summary>
        /// Creates and initialize a poolable object.
        /// </summary>
        /// <param name="prefab">Poolable prefab.</param>
        /// <param name="parent">Pool Transform.</param>
        /// <typeparam name="T">Prefab Type.</typeparam>
        /// <returns>Instanced Pooled Object.</returns>
        private static T CreatePoolable<T>(T prefab, Transform parent) where T : PoolableMonoBehaviour
        {
            var instance = Instantiate(prefab, parent);
            instance.SetupRoot(parent);
            return instance;
        }
    }
}