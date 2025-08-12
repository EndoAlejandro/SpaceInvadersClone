using System;
using UnityEngine;

namespace SpaceInvaders.Enemies
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpecialEnemy : BaseEnemy
    {
        private Rigidbody2D _rigidbody;

        private bool _isGoingLeft;
        private float _speed;
        private float _leftScreenLimit;
        private float _rightScreenLimit;

        public void Setup(Transform parent, float speed, float yOffset)
        {
            _isGoingLeft = !_isGoingLeft;
            _rigidbody ??= GetComponent<Rigidbody2D>();
            transform.SetParent(parent);

            _leftScreenLimit = Camera.main?.ViewportToWorldPoint(Vector2.right).x ?? 0f;
            _rightScreenLimit = Camera.main?.ViewportToWorldPoint(Vector2.zero).x ?? 0f;
            var x = _isGoingLeft ? _leftScreenLimit : 0f;
            transform.localPosition = new Vector2(x, -yOffset);
            _speed = _isGoingLeft ? -speed : speed;
            gameObject.SetActive(true);
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(transform.position + Vector3.right * (_speed * Time.fixedDeltaTime));

            if (transform.position.x > _leftScreenLimit || transform.position.x < _rightScreenLimit)
            {
                gameObject.SetActive(false);
            }
        }
    }
}