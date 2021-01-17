using UnityEngine;
// Jumper

namespace Jumper
{
    public class Enemy : MonoBehaviour
    {
        public float Power;
        public float Speed;

        Rigidbody2D _rigidbody;
        [SerializeField]
        float _forceMultiplier = 1;

        [SerializeField]
        Vector2 _horizontalBounds = new Vector2(-6, 6);


        void Start()
        {
            _rigidbody = GetComponentInChildren<Rigidbody2D>();
        }

        void Update()
        {
            _rigidbody.velocity = new Vector2(Speed * -1, _rigidbody.velocity.y);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                var player = other.gameObject.GetComponentInChildren<Player>();
                KnockBack(player);
            }
        }
        private void KnockBack(Player player)
        {
            var rb = player.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(-1, 0) * (Power * _forceMultiplier));
            Destroy(gameObject);
        }

    }
}