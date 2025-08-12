using System;
using System.Linq;
using UnityEngine;
using static SpaceInvaders.Enemies.EnemyController;

namespace SpaceInvaders.Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
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
            _minTimeBetweenMovement = Mathf.Max(_minTimeBetweenMovement * (1f - GameManager.NormalizedLevel), Constants.MIN_DIFFICULTY_SCALE);
            _maxTimeBetweenMovement = Mathf.Max(_maxTimeBetweenMovement * (1f - GameManager.NormalizedLevel), Constants.MIN_DIFFICULTY_SCALE);

            _timeBetweenMovement = _minTimeBetweenMovement;
            BaseEnemy.OnDeath += EnemyOnDeath;
        }

        private void Update()
        {
            _movementTimer += Time.deltaTime;
            if (_movementTimer < _timeBetweenMovement) return;

            _movementTimer = 0;
            
            // Check if any enemy is touching the safe area.
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

            Enemy enemy = EnemyController.Enemies.FirstOrDefault(enemy => enemy.IsTouchingBottomLimit());
            if (enemy)
            {
                GameManager.LoseGame();
            }
        }

        private void EdgeCheck()
        {
            if (_touchingBorder) return;
            
            foreach (var enemy in EnemyController.Enemies)
            {
                enemy.NextSprite();

                if (!enemy.WillTouchBorder(_moveDistance)) continue;
                
                _touchingBorder = true;
                _moveDistance.x *= -1f;
            }
        }

        private void EnemyOnDeath(BaseEnemy enemy)
        {
            var t = _difficultyCurve.Evaluate(1 - EnemyController.Enemies.Count / (float)EnemiesAmount);
            _timeBetweenMovement = Mathf.Lerp(_minTimeBetweenMovement, _maxTimeBetweenMovement, t);
        }

        private void OnDestroy()
        {
            BaseEnemy.OnDeath -= EnemyOnDeath;
        }
    }
}