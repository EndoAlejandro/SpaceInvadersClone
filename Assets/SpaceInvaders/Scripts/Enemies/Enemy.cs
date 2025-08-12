using UnityEngine;

namespace SpaceInvaders.Enemies
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : BaseEnemy
    {
        private BoxCollider2D _collider;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody;

        public void Setup(Transform parent, Vector3 position, EnemyStatsSo enemyStats)
        {
            base.Setup(enemyStats);
            _collider ??= GetComponent<BoxCollider2D>();
            _rigidbody ??= GetComponent<Rigidbody2D>();

            _spriteRenderer ??= GetComponentInChildren<SpriteRenderer>();
            _spriteRenderer.sprite = enemyStats.Sprite;

            transform.SetParent(parent, true);
            transform.position = position;
        }

        public bool WillTouchBorder(Vector3 moveDistance)
        {
            return transform.position.x + moveDistance.x < GameManager.LeftEdge
                || transform.position.x + moveDistance.x > GameManager.RightEdge;
        }

        public bool IsTouchingBottomLimit() => 
            transform.position.y < GameManager.BottomEnemyLimit;
    }
}