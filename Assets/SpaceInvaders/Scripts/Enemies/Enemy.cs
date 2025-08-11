using SpaceInvaders.Core;
using UnityEngine;

namespace SpaceInvaders.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : PoolableMonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody;

        public void Setup(Transform parent, Vector3 position, Sprite sprite)
        {
            _rigidbody ??= GetComponent<Rigidbody2D>();
            _spriteRenderer ??= GetComponentInChildren<SpriteRenderer>();
            _spriteRenderer.sprite = sprite;

            // _rigidbody.MovePosition(position);
            transform.SetParent(parent, true);
            transform.position = position;
        }
    }
}