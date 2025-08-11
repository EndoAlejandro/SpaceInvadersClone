using SpaceInvaders.Core;
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

        public void Setup(Vector3 initialPosition,float speed, float lifeTime)
        {
            _rigidbody ??= GetComponent<Rigidbody2D>();
            _rigidbody.MovePosition(initialPosition);
            transform.position = initialPosition;
            _speed = speed;
            _lifeTime = lifeTime;
        }

        private void Update()
        {
            _lifeTime -= Time.deltaTime;
            if(_lifeTime <= 0) ReturnToPool();
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(transform.position + Vector3.up * (_speed * Time.fixedDeltaTime));
        }
    }
}