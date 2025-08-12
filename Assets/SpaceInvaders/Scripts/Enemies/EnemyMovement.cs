using System;
using System.Linq;
using UnityEngine;
using static SpaceInvaders.Enemies.EnemyManager;

namespace SpaceInvaders.Enemies
{
    /// <summary>
    /// Move all spawned enemies at once.
    /// </summary>
    public class EnemyMovement : MonoBehaviour
    {
        /// <summary>
        /// Called each movement.
        /// </summary>
        public static event Action OnMove;

        [SerializeField] private Vector2 _moveDistance = new Vector2(.5f, .5f);

        [Space]
        [SerializeField] private float _minTimeBetweenMovement = 1f;

        [SerializeField] private float _maxTimeBetweenMovement = .25f;

        [Space]
        [SerializeField] private AnimationCurve _difficultyCurve;

        private float _timeBetweenMovement;
        private float _movementTimer;
        private bool _touchingBorder;

        private void Awake()
        {
            // Movement limits scaling with current level.
            _minTimeBetweenMovement = Mathf.Max(_minTimeBetweenMovement * (1f - GameManager.NormalizedLevel), Constants.MIN_DIFFICULTY_SCALE);
            _maxTimeBetweenMovement = Mathf.Max(_maxTimeBetweenMovement * (1f - GameManager.NormalizedLevel), Constants.MIN_DIFFICULTY_SCALE);

            // Always starts with minTime.
            _timeBetweenMovement = _minTimeBetweenMovement;
            BaseEnemy.OnDeath += EnemyOnDeath;
        }

        private void Update() => Movement();

        /// <summary>
        /// Movement logic.
        /// </summary>
        private void Movement()
        {
            _movementTimer += Time.deltaTime;
            if (_movementTimer < _timeBetweenMovement) return;
            _movementTimer = 0;
            
            EdgeCheck();

            // Vertical Movement.
            if (_touchingBorder)
            {
                _touchingBorder = false;
                transform.position += Vector3.down * _moveDistance.y;
            }
            // Normal movement.
            else
            {
                transform.position += Vector3.right * _moveDistance.x;
            }
            OnMove?.Invoke();
            BottomReachedCheck();
        }

        /// <summary>
        /// Checks if any enemy reached the bottom of the screen.
        /// </summary>
        private static void BottomReachedCheck()
        {
            StandardEnemy standardEnemy = EnemyManager.Enemies.FirstOrDefault(enemy => enemy.IsTouchingBottomLimit());
            if (standardEnemy)
            {
                GameManager.LoseGame();
            }
        }

        /// <summary>
        /// Check if any enemy is touching the safe area.
        /// </summary>
        private void EdgeCheck()
        {
            if (_touchingBorder) return;

            foreach (var enemy in EnemyManager.Enemies)
            {
                enemy.NextSprite();

                if (!enemy.WillTouchBorder(_moveDistance)) continue;

                _touchingBorder = true;
                _moveDistance.x *= -1f;
            }
        }

        private void EnemyOnDeath(BaseEnemy enemy)
        {
            var t = _difficultyCurve.Evaluate(1 - EnemyManager.Enemies.Count / (float)EnemiesAmount);
            _timeBetweenMovement = Mathf.Lerp(_minTimeBetweenMovement, _maxTimeBetweenMovement, t);
        }

        private void OnDestroy()
        {
            BaseEnemy.OnDeath -= EnemyOnDeath;
        }
    }
}