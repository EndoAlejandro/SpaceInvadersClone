using System;
using SpaceInvaders.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceInvaders.Enemies
{
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

        private void Start()
        {
            _bulletsPool = Pool.CreatePool("EnemyProjectiles", 3, _projectilePrefab);
        }

        private void Update()
        {
            _shootingTimer += Time.deltaTime;
            if (_shootingTimer < _timeBetweenShooting) return;

            _shootingTimer = 0f;
            var t = _difficultyCurve.Evaluate(1 - EnemyController.Enemies.Count / (float)EnemyController.Enemies.Count);
            _timeBetweenShooting = Mathf.Lerp(_minTimeBetweenShooting, _maxTimeBetweenShooting, t);
            int randomIndex = Random.Range(0, EnemyController.Enemies.Count);
            var pooledBullet = _bulletsPool.PoolObject<EnemyProjectile>();
            pooledBullet.Setup(EnemyController.Enemies[randomIndex].transform.position, _projectileSpeed);
            OnShoot?.Invoke();
        }
    }
}