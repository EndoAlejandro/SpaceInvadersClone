using System;
using SpaceInvaders.Core;
using SpaceInvaders.Player;
using UnityEngine;

namespace SpaceInvaders.Enemies
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyProjectile : PoolableMonoBehaviour
    {
        public static event Action OnShieldHit;

        private BoxCollider2D _collider;
        private Rigidbody2D _rigidbody;
        private float _speed;

        /// <summary>
        /// Initialize Projectile
        /// </summary>
        /// <param name="position">Projectile initial position.</param>
        /// <param name="speed">Projectile max speed.</param>
        public void Setup(Vector3 position, float speed)
        {
            _collider ??= GetComponent<BoxCollider2D>();
            _rigidbody ??= GetComponent<Rigidbody2D>();

            transform.position = position;
            _speed = speed;
        }

        private void Update()
        {
            // Check if limit reached.
            if (transform.position.y < GameManager.BottomEdge)
            {
                DestroyProjectile();
            }
        }

        private void FixedUpdate()
        {
            // Only moves downwards.
            _rigidbody.MovePosition(transform.position + Vector3.down * (_speed * Time.fixedDeltaTime));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // First check for Player collision.
            if (other.TryGetComponent(out PlayerController player))
            {
                player.Kill();
                DestroyProjectile();
            }
            // Then for Projectile collision.
            else if (other.TryGetComponent(out PlayerProjectile _))
            {
                DestroyProjectile();
            }
            // And then for Shield collision.
            else if (other.TryGetComponent(out Shield shield))
            {
                shield.DestroyShieldTile(_rigidbody.position);
                OnShieldHit?.Invoke();
                DestroyProjectile();
            }
        }

        private void DestroyProjectile()
        {
            ReturnToPool();
        }
    }
}