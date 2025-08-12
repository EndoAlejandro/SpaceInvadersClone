using SpaceInvaders.Core;
using SpaceInvaders.Player;
using UnityEngine;

namespace SpaceInvaders.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyProjectile : PoolableMonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private float _speed;

        public void Setup(Vector3 position, float speed)
        {
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
        }

        private void DestroyProjectile()
        {
            ReturnToPool();
        }
    }
}