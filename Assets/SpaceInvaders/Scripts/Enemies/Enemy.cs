using System;
using SpaceInvaders.Core;
using UnityEngine;

namespace SpaceInvaders.Enemies
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : PoolableMonoBehaviour
    {
        private BoxCollider2D _collider;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody;

        public void Setup(Transform parent, Vector3 position, Sprite sprite)
        {
            _collider ??= GetComponent<BoxCollider2D>();
            _rigidbody ??= GetComponent<Rigidbody2D>();
            
            _spriteRenderer ??= GetComponentInChildren<SpriteRenderer>();
            _spriteRenderer.sprite = sprite;

            transform.SetParent(parent, true);
            transform.position = position;
        }

        public bool WillTouchBorder(Vector3 moveDistance)
        {
            return transform.position.x + moveDistance.x < GameManager.LeftEdge
                || transform.position.x + moveDistance.x > GameManager.RightEdge;
        }
    }
}