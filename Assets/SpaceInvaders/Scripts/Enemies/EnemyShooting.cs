using System;
using SpaceInvaders.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceInvaders.Enemies
{
    public class EnemyShooting : MonoBehaviour
    {
        [SerializeField] private EnemyProjectile _projectilePrefab;
        [SerializeField] private float _projectileSpeed = 1f;
        [SerializeField] private float _minTimeBetweenShooting;
        [SerializeField] private float _maxTimeBetweenShooting;
        [SerializeField] private AnimationCurve _difficultyCurve;
        
        private float _shootingTimer;
        private float _timeBetweenShooting;
        private Pool _bulletsPool;

        private void Start()
        {
            _bulletsPool = Pool.CreatePool("EnemyProjectiles", 3, _projectilePrefab);
        }

        private void Update()
        {
            _shootingTimer += Time.deltaTime;
            if (_shootingTimer < _timeBetweenShooting) return;

            _shootingTimer = 0f;
            var t = _difficultyCurve.Evaluate(1 - EnemiesController.Enemies.Count / (float)EnemiesController.Enemies.Count);
            _timeBetweenShooting = Mathf.Lerp(_minTimeBetweenShooting, _maxTimeBetweenShooting, t);
            int randomIndex = Random.Range(0, EnemiesController.Enemies.Count);
            var pooledBullet = _bulletsPool.PoolObject<EnemyProjectile>();
            pooledBullet.Setup(EnemiesController.Enemies[randomIndex].transform.position, _projectileSpeed);
        }
    }
}