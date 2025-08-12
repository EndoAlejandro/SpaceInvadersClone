using System;
using System.Collections.Generic;
using System.Linq;
using SpaceInvaders.Core;
using UnityEngine;

namespace SpaceInvaders.Enemies
{
    public class EnemiesController : MonoBehaviour
    {
        [Header("Spawn Settings")]
        [SerializeField] private Sprite _greenEnemy;

        [SerializeField] private Sprite _redEnemy;
        [SerializeField] private Sprite _yellowEnemy;

        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private Enemy _extraEnemyPrefab;

        [SerializeField] private Vector2Int _enemiesMatrixSize = new Vector2Int(8, 5);
        [SerializeField] private Vector2 _distance = new Vector2(1, 1);

        [Header("Movement Settings")]
        [SerializeField] private Vector2 _moveDistance = new Vector2(.5f, .5f);

        [SerializeField] private float _timeBetweenMovement = 1f;

        private List<Enemy> _enemies;
        private float _timer;
        private bool _touchingBorder;

        private void Awake()
        {
            _enemies = new List<Enemy>();
        }

        private void Start()
        {
            var pool = Pool.CreatePool("Enemies", _enemiesMatrixSize.x + _enemiesMatrixSize.y, _enemyPrefab);
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

                    var pooledEnemy = pool.PoolObject<Enemy>();
                    pooledEnemy.Setup(transform, position, sprite);
                    _enemies.Add(pooledEnemy);
                }
            }
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer < _timeBetweenMovement) return;

            if (!_touchingBorder)
            {
                var result = _enemies.FirstOrDefault(enemy => enemy.WillTouchBorder(_moveDistance));
                _timer = 0;

                if (result != null)
                {
                    _touchingBorder = true;
                    _moveDistance.x *= -1f;
                }
            }

            if (_touchingBorder)
            {
                _touchingBorder = false;
                transform.position += Vector3.down * _moveDistance.y;
            }
            else
            {
                transform.position += Vector3.right * _moveDistance.x;
            }
        }
    }
}