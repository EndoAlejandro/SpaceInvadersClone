using System.Collections.Generic;
using SpaceInvaders.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace SpaceInvaders.Enemies
{
    /// <summary>
    /// Spawns all enemies around his own position.
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Vector2Int _enemiesMatrixSize = new Vector2Int(8, 5);
        [SerializeField] private Vector2 _distance = new Vector2(.55f, .5f);

        [FormerlySerializedAs("_enemyPrefab")]
        [Space]
        [SerializeField] private StandardEnemy _standardEnemyPrefab;

        [SerializeField] private SpecialEnemy _specialEnemyPrefab;
        [SerializeField] private float _specialEnemySpeed = .5f;
        [SerializeField] private float _specialEnemySpawnOffset;

        [Space]
        [SerializeField] private EnemyStatsSo _yellowEnemy;

        [SerializeField] private EnemyStatsSo _greenEnemy;
        [SerializeField] private EnemyStatsSo _redEnemy;

        private SpecialEnemy _spawnedSpecialEnemy;
        private Vector3 _initialPosition;

        private void Awake() => _initialPosition = transform.position;

        public void SpawnSpecialEnemy()
        {
            // Instantiated if not spawned yet. This enemy does not use the pool.
            if (_spawnedSpecialEnemy == null)
            {
                _spawnedSpecialEnemy = Instantiate(_specialEnemyPrefab);
            }

            _spawnedSpecialEnemy.gameObject.SetActive(true);
            _spawnedSpecialEnemy.Setup(_initialPosition, _specialEnemySpeed, _specialEnemySpawnOffset);
        }

        /// <summary>
        /// Spawn all needed enemies around Transform
        /// </summary>
        /// <returns>Spawned standard enemies.</returns>
        public List<StandardEnemy> SpawnBaseEnemies()
        {
            var enemies = new List<StandardEnemy>();
            // Total Enemies to instantiate.
            var enemiesAmount = _enemiesMatrixSize.x + _enemiesMatrixSize.y;

            // Enemies are instantiated using pooling but this could be a simple Instantiate.
            var pool = Pool.CreatePool("Enemies", enemiesAmount, _standardEnemyPrefab);

            // Horizontal offset so each one is centered.
            var xOffset = -(_enemiesMatrixSize.x * _distance.x) / 2f + _distance.x / 2f;

            for (int i = 0; i < _enemiesMatrixSize.x; i++)
            {
                for (int j = 0; j < _enemiesMatrixSize.y; j++)
                {
                    // The enemy type is decided by his spawn position.
                    var normalizedJ = (float)j / _enemiesMatrixSize.y;
                    var stats = normalizedJ switch {
                        > .6f => _greenEnemy,
                        > .3f => _yellowEnemy,
                        _ => _redEnemy,
                    };

                    // Calculate the position.
                    var position = Vector3.right * i * _distance.x - Vector3.up * j * _distance.y;
                    position.x += xOffset;
                    position += transform.position;

                    // Get the enemy from pool and initialize.
                    var pooledEnemy = pool.PoolObject<StandardEnemy>();
                    pooledEnemy.Setup(transform, position, stats);
                    enemies.Add(pooledEnemy);
                }
            }

            return enemies;
        }
    }
}