using System;
using System.Collections.Generic;
using System.Linq;
using SpaceInvaders.Core;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace SpaceInvaders.Enemies
{
    /// <summary>
    /// Spawn and move all enemies.
    /// </summary>
    public class EnemiesController : MonoBehaviour
    {
        [Header("Spawn Settings")]
        [SerializeField] private EnemyStatsSo _greenEnemy;

        [SerializeField] private EnemyStatsSo _redEnemy;
        [SerializeField] private EnemyStatsSo _yellowEnemy;

        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private Enemy _extraEnemyPrefab;

        [SerializeField] private Vector2Int _enemiesMatrixSize = new Vector2Int(8, 5);
        [SerializeField] private Vector2 _distance = new Vector2(1, 1);

        [Header("Movement Settings")]
        [SerializeField] private Vector2 _moveDistance = new Vector2(.5f, .5f);

        [SerializeField] private float _minTimeBetweenMovement = 1f;
        [SerializeField] private float _maxTimeBetweenMovement = .25f;

        [FormerlySerializedAs("_movementCurve")]
        [SerializeField] private AnimationCurve _difficultyCurve;

        [Header("Shooting Settings")]
        [SerializeField] private EnemyProjectile _projectilePrefab;

        [SerializeField] private float _projectileSpeed = 1f;

        [SerializeField] private float _minTimeBetweenShooting = 5f;
        [SerializeField] private float _maxTimeBetweenShooting = 1f;

        private List<Enemy> _enemies;
        private int _enemiesAmount;
        private bool _touchingBorder;

        private float _timeBetweenMovement;
        private float _movementTimer;

        private float _timeBetweenShooting;
        private float _shootingTimer;
        private Pool _bulletsPool;

        private void Awake()
        {
            _enemies = new List<Enemy>();
            _timeBetweenMovement = _minTimeBetweenMovement;
            Enemy.OnDeath += EnemyOnDeath;
        }

        private void OnDestroy()
        {
            Enemy.OnDeath -= EnemyOnDeath;
        }

        // When an enemy dies is removed from the enemy collection.
        private void EnemyOnDeath(Enemy enemy)
        {
            _enemies?.Remove(enemy);
            var t = _difficultyCurve.Evaluate(1 - _enemies.Count / (float)_enemiesAmount);
            _timeBetweenMovement = Mathf.Lerp(_minTimeBetweenMovement, _maxTimeBetweenMovement, t);

            if (_enemies.Count == 0)
            {
                GameManager.WinGame();
            }
        }

        // Create the enemies from a pool.
        private void Start()
        {
            _enemiesAmount = _enemiesMatrixSize.x + _enemiesMatrixSize.y;
            _bulletsPool = Pool.CreatePool("EnemyProjectiles", 3, _projectilePrefab);
            SpawnEnemies();
        }

        private void SpawnEnemies()
        {
            var pool = Pool.CreatePool("Enemies", _enemiesAmount, _enemyPrefab);

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
            Shooting();
            Movement();
        }

        private void Shooting()
        {
            _shootingTimer += Time.deltaTime;
            if (_shootingTimer < _timeBetweenShooting) return;

            _shootingTimer = 0f;
            var t = _difficultyCurve.Evaluate(1 - _enemies.Count / (float)_enemiesAmount);
            _timeBetweenShooting = Mathf.Lerp(_minTimeBetweenShooting, _maxTimeBetweenShooting, t);
            int randomIndex = Random.Range(0, _enemies.Count);
            var pooledBullet = _bulletsPool.PoolObject<EnemyProjectile>();
            pooledBullet.Setup(_enemies[randomIndex].transform.position, _projectileSpeed);
        }

        private void Movement()
        {
            _movementTimer += Time.deltaTime;
            if (_movementTimer < _timeBetweenMovement) return;

            // Check if any enemy is touching the safe area.
            EdgeCheck();

            // Vertical Movement.
            if (_touchingBorder)
            {
                _touchingBorder = false;
                transform.position += Vector3.down * _moveDistance.y;
            }
            // Normal movement.
            else
            {
                transform.position += Vector3.right * _moveDistance.x;
            }
        }

        private void EdgeCheck()
        {
            if (!_touchingBorder)
            {
                var result = _enemies.FirstOrDefault(enemy => enemy.WillTouchBorder(_moveDistance));
                _movementTimer = 0;

                // When an enemy is touching the safe area, chane movement to vertical and flip horizontal direction.
                if (result != null)
                {
                    _touchingBorder = true;
                    _moveDistance.x *= -1f;
                }
            }
        }
    }
}