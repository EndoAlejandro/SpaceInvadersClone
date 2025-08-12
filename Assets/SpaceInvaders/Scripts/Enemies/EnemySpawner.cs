using System.Collections.Generic;
using SpaceInvaders.Core;
using UnityEngine;

namespace SpaceInvaders.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Vector2Int _enemiesMatrixSize = new Vector2Int(8, 5);
        [SerializeField] private Vector2 _distance = new Vector2(.55f, .5f);
        [Space]
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private Enemy _extraEnemyPrefab;
        [Space]
        [SerializeField] private EnemyStatsSo _yellowEnemy;
        [SerializeField] private EnemyStatsSo _greenEnemy;
        [SerializeField] private EnemyStatsSo _redEnemy;

        public List<Enemy> Spawn()
        {
            var enemies = new List<Enemy>();
            var enemiesAmount = _enemiesMatrixSize.x + _enemiesMatrixSize.y;

            var pool = Pool.CreatePool("Enemies", enemiesAmount, _enemyPrefab);

            var xOffset = -(_enemiesMatrixSize.x * _distance.x) / 2f + _distance.x / 2f;

            for (int i = 0; i < _enemiesMatrixSize.x; i++)
            {
                for (int j = 0; j < _enemiesMatrixSize.y; j++)
                {
                    var normalizedJ = (float)j / _enemiesMatrixSize.y;
                    var sprite = normalizedJ switch {
                        > .6f => _greenEnemy,
                        > .3f => _yellowEnemy,
                        _ => _redEnemy,
                    };

                    var position = Vector3.right * i * _distance.x - Vector3.up * j * _distance.y;
                    position.x += xOffset;
                    position += transform.position;

                    var pooledEnemy = pool.PoolObject<Enemy>();
                    pooledEnemy.Setup(transform, position, sprite);
                    enemies.Add(pooledEnemy);
                }
            }
            
            return enemies;
        }
    }
}