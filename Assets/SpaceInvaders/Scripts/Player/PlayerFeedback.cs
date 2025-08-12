using SpaceInvaders.Core;
using SpaceInvaders.Enemies;
using UnityEngine;

namespace SpaceInvaders.Player
{
    /// <summary>
    /// Play visual and sound feedback when needed.
    /// </summary>
    public class PlayerFeedback : MonoBehaviour
    {
        [SerializeField] private Sprite _deathSprite;
        
        [SerializeField] private AudioData _shootAudio;
        [SerializeField] private AudioData _shieldHitAudio;
        [SerializeField] private AudioData _deathAudio;

        private void Start()
        {
            PlayerController.OnDeath += PlayerControllerOnDeath;
            PlayerProjectile.OnShieldHit += PlayerProjectileOnShieldHit;
            PlayerShoot.OnShot += PlayerShootOnShot;
        }

        private void PlayerControllerOnDeath() => FeedbackManager.Instance.PlayFx(transform.position, _deathAudio, _deathSprite);
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