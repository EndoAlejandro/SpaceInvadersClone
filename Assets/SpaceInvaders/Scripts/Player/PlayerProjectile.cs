using System;
using SpaceInvaders.Core;
using SpaceInvaders.Enemies;
using UnityEngine;

namespace SpaceInvaders.Player
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerProjectile : PoolableMonoBehaviour
    {
        public static event Action OnShieldHit;

        private BoxCollider2D _collider;
        private Rigidbody2D _rigidbody;
        private float _speed;
        private Action _onDestroyedCallback;

        /// <summary>
        /// Initialize Projectile
        /// </summary>
        /// <param name="initialPosition">Projectile initial position.</param>
        /// <param name="speed">Projectile max speed.</param>
        /// <param name="onDestroyedCallback">callback called when projectile is destroyed.</param>
        public void Setup(Vector3 initialPosition, float speed, Action onDestroyedCallback)
        {
            _collider ??= GetComponent<BoxCollider2D>();
            _rigidbody ??= GetComponent<Rigidbody2D>();
            _rigidbody.MovePosition(initialPosition);
            transform.position = initialPosition;
            _speed = speed;
            _onDestroyedCallback = onDestroyedCallback;
        }

        private void Update()
        {
            // Destroy projectile if touching top edge.
            if (transform.position.y > GameManager.TopEdge)
            {
                DestroyProjectile();
            }
        }

        // Simulated physics movement.
        private void FixedUpdate() => 
            _rigidbody.MovePosition(_rigidbody.position + Vector2.up * (_speed * Time.fixedDeltaTime));

        private void OnTriggerEnter2D(Collider2D other)
        {
            // First check for Enemy collision.
            if (other.TryGetComponent(out BaseEnemy enemy))
            {
                enemy.Kill();
                DestroyProjectile();
            }
            // Then for Projectile collision.
            else if (other.TryGetComponent(out EnemyProjectile _))
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
            _onDestroyedCallback?.Invoke();
            _speed = 0f;
            ReturnToPool();
        }
    }
}