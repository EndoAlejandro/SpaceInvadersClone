using UnityEngine;

namespace SpaceInvaders.Enemies
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class StandardEnemy : BaseEnemy
    {
        private BoxCollider2D _collider;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody;

        private bool _usingMainSprite;

        /// <summary>
        /// Initialize Enemy. This method is used instead of start.
        /// </summary>
        /// <param name="parent">Enemy Controller Transform.</param>
        /// <param name="position">Spawn Position.</param>
        /// <param name="enemyStats">Scriptable Object.</param>
        public void Setup(Transform parent, Vector3 position, EnemyStatsSo enemyStats)
        {
            base.Setup(enemyStats);
            
            _collider ??= GetComponent<BoxCollider2D>();
            _rigidbody ??= GetComponent<Rigidbody2D>();

            _spriteRenderer ??= GetComponentInChildren<SpriteRenderer>();
            _spriteRenderer.sprite = enemyStats.MainSprite;
            _usingMainSprite = true;

            transform.SetParent(parent, true);
            transform.position = position;
        }

        /// <summary>
        /// Animate the enemy each time it moves.
        /// </summary>
        public void NextSprite()
        {
            _usingMainSprite = !_usingMainSprite;
            _spriteRenderer.sprite = _usingMainSprite ? stats.MainSprite : stats.SecondarySprite;
        }

        /// <summary>
        /// Check if the enemy will be touching the borders in the next move.
        /// </summary>
        /// <param name="moveDistance">Next move distance.</param>
        /// <returns>True if outside of boundaries.</returns>
        public bool WillTouchBorder(Vector3 moveDistance)
        {
            return transform.position.x + moveDistance.x < GameManager.LeftEdge
                || transform.position.x + moveDistance.x > GameManager.RightEdge;
        }

        /// <summary>
        /// Checks if the enemy already reached the bottom line.
        /// </summary>
        /// <returns></returns>
        public bool IsTouchingBottomLimit() =>
            transform.position.y < GameManager.BottomEnemyLimit;
    }
}