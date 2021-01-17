using System;
using UnityEngine;
// Jumper

namespace Jumper
{

    public class Player : MonoBehaviour
    {
        Rigidbody2D _rigidbody;

        [SerializeField]
        float _speed;

        [SerializeField]
        private float _jumpForce;


        float _defaultgravity;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Water"))
            {
                Die();
            }

            if (other.collider.CompareTag("Goal"))
            {
                Win();
            }

            if (other.collider.CompareTag("Enemy"))
            {
                var enemyHit = other.gameObject.GetComponentInChildren<Enemy>();
                KnockBack(enemyHit);
            }

        }

        private void KnockBack(Enemy enemy)
        {
            _rigidbody.AddForce(new Vector2(1, 0) * enemy.Power);
            Destroy(enemy.gameObject);
        }

        private void Win()
        {
            GameManager.Instance.Win();
        }

        private void Die()
        {
            GameManager.Instance.Die();
        }

        void Start()
        {
            _rigidbody = GetComponentInChildren<Rigidbody2D>(); // Getting from children to allow for restructure of player GO.
            _defaultgravity = _rigidbody.gravityScale;
        }

        void Update()
        {
            var move = Input.GetAxisRaw("Horizontal");
            if (move != 0f)
            {
                Move(move);
            }

            if (Input.GetButton("Jump"))
            {
                Jump();
            }
        }

        private void Move(float move)
        {
            _rigidbody.velocity = new Vector2(move, 0);
        }

        private void Jump()
        {
            _rigidbody.AddForce(new Vector2(0, _jumpForce));
        }
    }
}