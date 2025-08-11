using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpaceInvaders.Player
{
    public class Pool : MonoBehaviour
    {
        private static Dictionary<Type, Pool> _pools;

        private List<PoolableMonoBehaviour> _poolables;
        private static PoolableMonoBehaviour _prefab;

        private int _index = 0;

        public T PoolObject<T>() where T : PoolableMonoBehaviour
        {
            T pooled = null;

            if (_poolables[_index].gameObject.activeInHierarchy)
            {
                pooled = _poolables.Where(p => !p.gameObject.activeInHierarchy).FirstOrDefault(p => p) as T;
                for (int i = 0; i < _poolables.Count; i++)
                {
                    if (_poolables[i].gameObject.activeInHierarchy) continue;
                    _index = i;
                    break;
                }

                if (pooled == null)
                {
                    pooled = CreatePoolable<T>(transform);
                    _poolables.Add(pooled);   
                }
            }
            else
            {
                pooled = _poolables[_index] as T;
            }

            _index = (_index + 1) % _poolables.Count;
            pooled?.gameObject.SetActive(true);
            return pooled;
        }

        private static T CreatePoolable<T>(Transform parent) where T : PoolableMonoBehaviour
        {
            var instance = Instantiate(_prefab, parent);
            instance.SetupRoot(parent);
            return instance as T;
        }

        public static Pool CreatePool<T>(string name, int size, T prefab) where T : PoolableMonoBehaviour
        {
            _pools ??= new Dictionary<Type, Pool>();
            _prefab = prefab;

            if (_pools.ContainsKey(typeof(T)))
            {
                return _pools[typeof(T)];
            }

            var poolObject = new GameObject($"{name} Pool");
            var pool = poolObject.AddComponent<Pool>();
            pool._poolables = new List<PoolableMonoBehaviour>();

            for (int i = 0; i < size; i++)
            {
                var instance = CreatePoolable<T>(poolObject.transform);
                pool._poolables.Add(instance);
                instance.gameObject.SetActive(false);
            }

            _pools.Add(typeof(T), pool);
            return pool;
        }
    }
}