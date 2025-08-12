using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace SpaceInvaders.Enemies
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpecialEnemy : BaseEnemy
    {
        [SerializeField] private EnemyStatsSo _stats;

        private readonly int[] _specialEnemyPoints = { 50, 100, 150, 300 };

        private Rigidbody2D _rigidbody;
        private Vector3 _initialPosition;

        private bool _isGoingLeft;
        private float _speed;
        private float _leftScreenLimit;
        private float _rightScreenLimit;

        public void Setup(Vector2 initialPosition, float speed, float yOffset)
        {
            Setup(_stats);
            _isGoingLeft = !_isGoingLeft;
            _rigidbody ??= GetComponent<Rigidbody2D>();

            _leftScreenLimit = Camera.main?.ViewportToWorldPoint(Vector2.zero).x ?? 0f;
            _rightScreenLimit = Camera.main?.ViewportToWorldPoint(Vector2.right).x ?? 0f;
            var x = _isGoingLeft ? _rightScreenLimit : _leftScreenLimit;
            transform.position = initialPosition + new Vector2(x, -yOffset);
            _speed = _isGoingLeft ? -speed : speed;
            gameObject.SetActive(true);
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(transform.position + Vector3.right * (_speed * Time.fixedDeltaTime));

            if (transform.position.x < _leftScreenLimit || transform.position.x > _rightScreenLimit)
            {
                gameObject.SetActive(false);
            }
        }

        public override void Kill()
        {
            int randomIndex = Random.Range(0, _specialEnemyPoints.Length);
            GameManager.AddScore(_specialEnemyPoints[randomIndex]);
            gameObject.SetActive(false);
        }
    }
}