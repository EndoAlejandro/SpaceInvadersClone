using SpaceInvaders.Core;
using UnityEngine;

namespace SpaceInvaders.Enemies
{
    public class EnemyFeedback : BaseFeedback
    {
        [SerializeField] private AudioData _shootAudio;
        [SerializeField] private AudioData _deathAudio;
        [SerializeField] private AudioData _shieldHitAudio;

        protected override void Awake()
        {
            base.Awake();
            EnemyProjectile.OnShieldHit += EnemyProjectileOnShieldHit;
            EnemyShooting.OnShoot += EnemyShootingOnShoot;
            BaseEnemy.OnDeath += BaseEnemyOnDeath;
        }

        private void EnemyProjectileOnShieldHit() => PlaySound(_shieldHitAudio);
        private void EnemyShootingOnShoot() => PlaySound(_shootAudio);
        private void BaseEnemyOnDeath(BaseEnemy enemy) => PlaySound(_deathAudio);

        private void OnDestroy()
        {
            EnemyProjectile.OnShieldHit -= EnemyProjectileOnShieldHit;
            EnemyShooting.OnShoot -= EnemyShootingOnShoot;
            BaseEnemy.OnDeath -= BaseEnemyOnDeath;
        }
    }
}
