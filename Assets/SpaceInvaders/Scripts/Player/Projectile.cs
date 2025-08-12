using System;
using SpaceInvaders.Core;
using SpaceInvaders.Enemies;
using UnityEngine;

namespace SpaceInvaders.Player
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : PoolableMonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private float _speed;
        private float _lifeTime;
        private Action _onDestroyedCallback;

        public void Setup(Vector3 initialPosition, float speed, float lifeTime, Action onDestroyedCallback)
        {
            _rigidbody ??= GetComponent<Rigidbody2D>();
            _rigidbody.MovePosition(initialPosition);
            transform.position = initialPosition;
            _speed = speed;
            _lifeTime = lifeTime;
            _onDestroyedCallback = onDestroyedCallback;
        }

        private void Update()
        {
            _lifeTime -= Time.deltaTime;
            if (_lifeTime <= 0) DestroyBullet();
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(transform.position + Vector3.up * (_speed * Time.fixedDeltaTime));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out Enemy enemy)) return;
            enemy.Kill();
            DestroyBullet();
        }
        
        private void DestroyBullet()
        {
            _onDestroyedCallback?.Invoke();
            ReturnToPool();
        }
    }
}