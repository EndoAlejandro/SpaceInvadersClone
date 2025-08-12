using SpaceInvaders.Core;
using UnityEngine;

namespace SpaceInvaders.Enemies
{
    /// <summary>
    /// Play visual and sound feedback when needed.
    /// </summary>
    public class EnemyFeedback : MonoBehaviour
    {
        [SerializeField] private Sprite _deathSprite;

        [SerializeField] private AudioData _moveAudio;
        [SerializeField] private AudioData _shootAudio;
        [SerializeField] private AudioData _deathAudio;
        [SerializeField] private AudioData _shieldHitAudio;

        protected void Awake()
        {
            EnemyMovement.OnMove += EnemyMovementOnMove;
            EnemyProjectile.OnShieldHit += EnemyProjectileOnShieldHit;
            EnemyShooting.OnShoot += EnemyShootingOnShoot;
            BaseEnemy.OnDeath += BaseEnemyOnDeath;
        }

        private void EnemyMovementOnMove() =>
            FeedbackManager.Instance.PlayFx(transform.position, audioData: _moveAudio);

        private void EnemyProjectileOnShieldHit() =>
            FeedbackManager.Instance.PlayFx(transform.position, audioData: _shieldHitAudio);

        private void EnemyShootingOnShoot() =>
            FeedbackManager.Instance.PlayFx(transform.position, audioData: _shootAudio);

        private void BaseEnemyOnDeath(BaseEnemy enemy) =>
            FeedbackManager.Instance.PlayFx(enemy.transform.position, audioData: _deathAudio, sprite: _deathSprite);

        private void OnDestroy()
        {
            EnemyMovement.OnMove -= EnemyMovementOnMove;
            EnemyProjectile.OnShieldHit -= EnemyProjectileOnShieldHit;
            EnemyShooting.OnShoot -= EnemyShootingOnShoot;
            BaseEnemy.OnDeath -= BaseEnemyOnDeath;
        }
    }
}