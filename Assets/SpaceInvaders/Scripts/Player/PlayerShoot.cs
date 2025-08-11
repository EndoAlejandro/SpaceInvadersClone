using UnityEngine;

namespace SpaceInvaders.Player
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private Projectile _projectile;
        [SerializeField] private float _speed;
        [SerializeField] private float _cooldown;
        [SerializeField] private float _bulletLifeTime = 1f;

        private Pool _pool;
        private float _timer;

        private void Awake()
        {
            _pool = Pool.CreatePool("Projectiles", 3, _projectile);
        }

        private void Update()
        {
            if (_timer > 0f)
            {
                _timer -= Time.deltaTime;
                return;
            }

            if (!GameManager.Input.Shoot) return;
            
            _timer = _cooldown;
            var projectile = _pool.PoolObject<Projectile>();
            projectile.Setup(transform.position, _speed, _bulletLifeTime);
        }
    }
}