using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceInvaders.Enemies
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpecialEnemy : BaseEnemy
    {
        [SerializeField] private EnemyStatsSo _stats;

        private Rigidbody2D _rigidbody;
        private Vector3 _initialPosition;

        private bool _isGoingLeft;
        private float _speed;
        private float _leftScreenLimit;
        private float _rightScreenLimit;

        /// <summary>
        /// Initialize Enemy. This method is used instead of start.
        /// </summary>
        /// <param name="initialPosition">Spawn position.</param>
        /// <param name="speed">Horizontal speed.</param>
        /// <param name="yOffset">Distance above the spawn point of the standard enemies.</param>
        public void Setup(Vector2 initialPosition, float speed, float yOffset)
        {
            base.Setup(_stats);
            
            // Each spawn the enemy change side.
            _isGoingLeft = !_isGoingLeft;
            _rigidbody ??= GetComponent<Rigidbody2D>();

            // Sets Side limits.
            _leftScreenLimit = Camera.main?.ViewportToWorldPoint(Vector2.zero).x ?? 0f;
            _rightScreenLimit = Camera.main?.ViewportToWorldPoint(Vector2.right).x ?? 0f;

            // Sets the new move direction.
            var x = _isGoingLeft ? _rightScreenLimit : _leftScreenLimit;
            transform.position = initialPosition + new Vector2(x, -yOffset);
            _speed = _isGoingLeft ? -speed : speed;
            
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Deactivates if side reached.
        /// </summary>
        private void Update()
        {
            if (transform.position.x < _leftScreenLimit || transform.position.x > _rightScreenLimit)
                gameObject.SetActive(false);
        }

        private void FixedUpdate() => 
            _rigidbody.MovePosition(transform.position + Vector3.right * (_speed * Time.fixedDeltaTime));

        public override void Kill()
        {
            int randomIndex = Random.Range(0, Constants.SPECIAL_ENEMY_POINTS.Length);
            // Given score comes from custom array instead of ScriptableObject.
            GameManager.AddScore(Constants.SPECIAL_ENEMY_POINTS[randomIndex]);
            gameObject.SetActive(false);
            OnDeath?.Invoke(this);
        }
    }
}