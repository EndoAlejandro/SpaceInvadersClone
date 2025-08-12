using SpaceInvaders.Core;
using SpaceInvaders.Enemies;
using UnityEngine;

namespace SpaceInvaders.Player
{
    public class PlayerFeedback : MonoBehaviour
    {
        [SerializeField] private AudioData _shootAudio;
        [SerializeField] private AudioData _shieldHitAudio;
        [SerializeField] private AudioData _deathAudio;

        private void Start()
        {
            PlayerController.OnDeath += PlayerControllerOnDeath;
            PlayerProjectile.OnShieldHit += PlayerProjectileOnShieldHit;
            PlayerShoot.OnShot += PlayerShootOnShot;
        }

        private void PlayerControllerOnDeath() => FeedbackManager.Instance.PlayFx(transform.position, _deathAudio);
        private void PlayerProjectileOnShieldHit() => FeedbackManager.Instance.PlayFx(transform.position, _shieldHitAudio);
        private void PlayerShootOnShot() => FeedbackManager.Instance.PlayFx(transform.position, _shootAudio);

        private void OnDestroy()
        {
            PlayerController.OnDeath -= PlayerControllerOnDeath;
            PlayerProjectile.OnShieldHit -= PlayerProjectileOnShieldHit;
            PlayerShoot.OnShot -= PlayerShootOnShot;
        }
    }
}