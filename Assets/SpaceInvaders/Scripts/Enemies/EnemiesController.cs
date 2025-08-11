using SpaceInvaders.Core;
using UnityEngine;

namespace SpaceInvaders.Enemies
{
    public class EnemiesController : MonoBehaviour
    {
        [SerializeField] private Sprite _greenEnemy;
        [SerializeField] private Sprite _redEnemy;
        [SerializeField] private Sprite _yellowEnemy;

        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private Enemy _extraEnemyPrefab;

        [SerializeField] private Vector2Int _enemiesMatrixSize = new Vector2Int(8, 5);
        [SerializeField] private Vector2 _distance = new Vector2(1, 1);

        private Pool _enemiesPool;

        private void Awake()
        {
            _enemiesPool = Pool.CreatePool("Enemies", _enemiesMatrixSize.x + _enemiesMatrixSize.y, _enemyPrefab);
            var xOffset = -(_enemiesMatrixSize.x * _distance.x) / 2f + _distance.x / 2f;

            for (int i = 0; i < _enemiesMatrixSize.x; i++)
            {
                for (int j = 0; j < _enemiesMatrixSize.y; j++)
                {
                    var normalizedJ = (float)j / _enemiesMatrixSize.y;
                    var sprite = normalizedJ switch {
                        > .6f => _yellowEnemy,
                        > .3f => _greenEnemy,
                        _ => _redEnemy,
                    };

                    var position = Vector3.right * i * _distance.x - Vector3.up * j * _distance.y;
                    position.x += xOffset;
                    position += transform.position;

                    var pooledEnemy = _enemiesPool.PoolObject<Enemy>();
                    pooledEnemy.Setup(transform, position, sprite);
                }
            }
        }

        private void Start() { }
    }
}