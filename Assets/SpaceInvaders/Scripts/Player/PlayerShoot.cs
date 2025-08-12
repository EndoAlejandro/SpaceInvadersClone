using System;
using SpaceInvaders.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace SpaceInvaders.Player
{
    public class PlayerShoot : MonoBehaviour
    {
        public static event Action OnShot;
        
        [SerializeField] private PlayerProjectile _playerProjectile;
        [SerializeField] private float _speed;

        private Pool _pool;
        private bool _canShoot = true;

        private void Awake()
        {
            _pool = Pool.CreatePool("Projectiles", 3, _playerProjectile);
        }

        private void Update()
        {
            if (!_canShoot || !GameManager.Input.Shoot) return;

            _canShoot = false;
            var projectile = _pool.PoolObject<PlayerProjectile>();
            projectile.Setup(transform.position, _speed, OnProjectileDestroyed);
            OnShot?.Invoke();
        }

        private void OnProjectileDestroyed() => _canShoot = true;
    }
}