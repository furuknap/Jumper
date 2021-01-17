using UnityEngine;
// Jumper

namespace Jumper
{
    public class Enemy : MonoBehaviour
    {
        public float Power;
        public float Speed;

        Rigidbody2D _rigidbody;
        void Start()
        {
            _rigidbody = GetComponentInChildren<Rigidbody2D>();
        }

        void Update()
        {
            _rigidbody.velocity = new Vector2(Speed * -1, _rigidbody.velocity.y);
        }
    }
}