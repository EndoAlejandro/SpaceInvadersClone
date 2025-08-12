using System;
using SpaceInvaders.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceInvaders.Enemies
{
    /// <summary>
    /// Handle enemies shooting.
    /// </summary>
    public class EnemyShooting : MonoBehaviour
    {
        public static event Action OnShoot;
        
        [SerializeField] private EnemyProjectile _projectilePrefab;
        [SerializeField] private float _projectileSpeed = 1f;
        [SerializeField] private float _minTimeBetweenShooting;
        [SerializeField] private float _maxTimeBetweenShooting;
        [SerializeField] private AnimationCurve _difficultyCurve;
        
        private float _shootingTimer;
        private float _timeBetweenShooting;
        private Pool _bulletsPool;

        private void Awake()
        {
            _minTimeBetweenShooting = Mathf.Max(_minTimeBetweenShooting * (1f - GameManager.NormalizedLevel), Constants.MIN_DIFFICULTY_SCALE);
            _maxTimeBetweenShooting = Mathf.Max(_maxTimeBetweenShooting * (1f - GameManager.NormalizedLevel), Constants.MIN_DIFFICULTY_SCALE);
        }

        /// <summary>
        /// Creates initial projectiles pool.
        /// </summary>
        private void Start() => 
            _bulletsPool = Pool.CreatePool("EnemyProjectiles", 3, _projectilePrefab);

        private void Update()
        {
            // Checks for shooting timer.
            _shootingTimer += Time.deltaTime;
            if (_shootingTimer < _timeBetweenShooting) return;

            Shoot();
        }

        /// <summary>
        /// Spawn projectile from pool.
        /// </summary>
        private void Shoot()
        {
            _shootingTimer = 0f;
            var t = _difficultyCurve.Evaluate(1 - EnemyManager.Enemies.Count / (float)EnemyManager.Enemies.Count);
            _timeBetweenShooting = Mathf.Lerp(_minTimeBetweenShooting, _maxTimeBetweenShooting, t);
            int randomIndex = Random.Range(0, EnemyManager.Enemies.Count);
            var pooledProjectile = _bulletsPool.PoolObject<EnemyProjectile>();
            pooledProjectile.Setup(EnemyManager.Enemies[randomIndex].transform.position, _projectileSpeed);
            OnShoot?.Invoke();
        }
    }
}