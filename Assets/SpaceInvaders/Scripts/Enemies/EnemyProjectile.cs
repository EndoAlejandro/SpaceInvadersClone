using SpaceInvaders.Core;
using SpaceInvaders.Player;
using UnityEngine;

namespace SpaceInvaders.Enemies
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyProjectile : PoolableMonoBehaviour
    {
        private BoxCollider2D _collider;
        private Rigidbody2D _rigidbody;
        private float _speed;

        public void Setup(Vector3 position, float speed)
        {
            _collider ??= GetComponent<BoxCollider2D>();
            _rigidbody ??= GetComponent<Rigidbody2D>();

            transform.position = position;
            _speed = speed;
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(transform.position + Vector3.down * _speed * Time.fixedDeltaTime);

            if (transform.position.y < GameManager.BottomEdge)
            {
                DestroyProjectile();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerController player))
            {
                player.Kill();
                DestroyProjectile();
            }
            else if (other.TryGetComponent(out Projectile _))
            {
                DestroyProjectile();
            }
            else if (other.TryGetComponent(out Shield shield))
            {
                shield.DestroyShieldTile(_collider, true);
                DestroyProjectile();
            }
        }

        private void DestroyProjectile()
        {
            ReturnToPool();
        }
    }
}