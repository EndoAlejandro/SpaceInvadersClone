using SpaceInvaders.Core;
using UnityEngine;

namespace SpaceInvaders.Player
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private Projectile _projectile;
        [SerializeField] private float _speed;
        [SerializeField] private float _bulletLifeTime = 1f;

        private Pool _pool;
        private bool _canShoot = true;

        private void Awake()
        {
            _pool = Pool.CreatePool("Projectiles", 3, _projectile);
        }

        private void Update()
        {
            if (!_canShoot || !GameManager.Input.Shoot) return;

            _canShoot = false;
            var projectile = _pool.PoolObject<Projectile>();
            projectile.Setup(transform.position, _speed, _bulletLifeTime, OnProjectileDestroyed);
        }

        private void OnProjectileDestroyed() => _canShoot = true;
    }
}