using SpaceInvaders.Core;
using SpaceInvaders.Enemies;
using UnityEngine;

namespace SpaceInvaders.Player
{
    public class PlayerFeedback : BaseFeedback
    {
        [SerializeField] private AudioData _shootAudio;
        [SerializeField] private AudioData _bulletCollideAudio;
        [SerializeField] private AudioData _shieldHitAudio;

        private void Start()
        {
            PlayerProjectile.OnShieldHit += PlayerProjectileOnShieldHit;
            PlayerShoot.OnShot += PlayerShootOnShot;
        }

        private void PlayerProjectileOnShieldHit() => PlaySound(_shieldHitAudio);
        private void PlayerShootOnShot() => PlaySound(_shootAudio);

        private void OnDestroy()
        {
            PlayerProjectile.OnShieldHit -= PlayerProjectileOnShieldHit;
            PlayerShoot.OnShot -= PlayerShootOnShot;
        }
    }
}