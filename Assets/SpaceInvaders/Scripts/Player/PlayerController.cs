using SpaceInvaders.Input;
using UnityEngine;

namespace SpaceInvaders.Player
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _acceleration = 1f;
        [SerializeField] private float _deceleration = 1f;
        [SerializeField] private float _maxSpeed = 1f;

        private BoxCollider2D _collider;
        private Rigidbody2D _rigidbody;
        private InputReader _input;

        private Vector3 _velocity;

        private void Awake()
        {
            _input = GameManager.Input;

            _collider = GetComponent<BoxCollider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            float inputMovement = _input.Movement;
            float acceleration = _acceleration;
            Vector3 targetVelocity = Vector3.right * (inputMovement * _maxSpeed);

            if (Mathf.Abs(inputMovement) < 0.05f)
            {
                targetVelocity = Vector3.zero;
                acceleration = _deceleration;
            }

            var safeSpace = 0.1f;

            // Side checks.
            if (transform.position.x < GameManager.LeftEdge)
            {
                _rigidbody.MovePosition(new Vector3(GameManager.LeftEdge, transform.position.y, transform.position.z));
                _velocity = Vector3.zero;
                return;
            }

            if (transform.position.x > GameManager.RightEdge)
            {
                _rigidbody.MovePosition(new Vector3(GameManager.RightEdge, transform.position.y, transform.position.z));
                _velocity = Vector3.zero;
                return;
            }

            // Custom physics.
            _velocity = Vector3.MoveTowards(_velocity, targetVelocity, acceleration * Time.fixedDeltaTime);
            _rigidbody.MovePosition(transform.position + _velocity * Time.fixedDeltaTime);
        }

        public void Kill()
        {
            GameManager.LoseGame();
        }
    }
}