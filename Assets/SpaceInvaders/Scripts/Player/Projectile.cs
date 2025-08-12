using System;
using SpaceInvaders.Core;
using SpaceInvaders.Enemies;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace SpaceInvaders.Player
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : PoolableMonoBehaviour
    {
        private BoxCollider2D _collider;
        private Rigidbody2D _rigidbody;
        private float _speed;
        private Action _onDestroyedCallback;

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
            if (transform.position.y > GameManager.TopEdge)
            {
                DestroyProjectile();
            }
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(transform.position + Vector3.up * (_speed * Time.fixedDeltaTime));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out BaseEnemy enemy))
            {
                enemy.Kill();
                DestroyProjectile();
            }
            else if (other.TryGetComponent(out EnemyProjectile _))
            {
                DestroyProjectile();
            }
            else if (other.TryGetComponent(out Shield shield))
            {
                shield.DestroyShieldTile(_collider);
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