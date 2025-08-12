using System;
using SpaceInvaders.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace SpaceInvaders.Player
{
    /// <summary>
    /// Handle Player shooting.
    /// </summary>
    public class PlayerShoot : MonoBehaviour
    {
        public static event Action OnShot;
        
        [SerializeField] private PlayerProjectile _playerProjectile;
        [SerializeField] private float _speed;
        [SerializeField] private float _projectileVerticalOffset = .5f;

        private Pool _pool;
        private bool _canShoot = true;

        /// <summary>
        /// Creates initial projectiles pool.
        /// </summary>
        private void Awake() => 
            _pool = Pool.CreatePool("Projectiles", 3, _playerProjectile);

        private void Update()
        {
            // Check if previous projectile is destroyed and Shoot Input is pressed.
            if (!_canShoot || !GameManager.Input.Shoot) return;

            Shoot();
        }

        /// <summary>
        /// Spawn projectile from pool.
        /// </summary>
        private void Shoot()
        {
            _canShoot = false;
            var projectile = _pool.PoolObject<PlayerProjectile>();
            projectile.Setup(transform.position + Vector3.up * _projectileVerticalOffset, _speed, OnProjectileDestroyed);
            OnShot?.Invoke();
        }

        /// <summary>
        /// Player can only shoot one bullet at a time.
        /// </summary>
        private void OnProjectileDestroyed() => _canShoot = true;
    }
}