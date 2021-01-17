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

        [SerializeField]
        Vector2 _horizontalBounds = new Vector2(-6, 6);
        [SerializeField]
        Vector2 _verticalBounds = new Vector2(-3, 1.8f);



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

            //if (other.collider.CompareTag("Enemy"))
            //{
            //    var enemyHit = other.gameObject.GetComponentInChildren<Enemy>();
            //    KnockBack(enemyHit);
            //}

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


            //var x = Mathf.Clamp(transform.position.x, _horizontalBounds.x, _horizontalBounds.y);
            //var y = Mathf.Clamp(transform.position.y, _verticalBounds.x, _verticalBounds.y);
            //transform.position = new Vector3(x, y, -1);

        }

        private void Move(float move)
        {
            if (GameManager.Instance.CanMove)
            {
                _rigidbody.velocity = new Vector2(move, 0);

            }
        }

        private void Jump()
        {
            _rigidbody.AddForce(new Vector2(0, _jumpForce));
        }
    }
}