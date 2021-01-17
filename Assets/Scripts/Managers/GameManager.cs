using System;
using System.Linq;
using TMPro;
using UnityEngine;
// Jumper

namespace Jumper
{
    public class GameManager : SingletonComponent<GameManager>
    {
        float _spawnTimer;

        [SerializeField]
        float _spawnInterval = 3.0f;

        [SerializeField]
        TextMeshProUGUI _statusText;

        [SerializeField]
        Player _player;

        [SerializeField]
        GameObject _playerSpawnPoint;

        [SerializeField]
        Enemy _enemyPrefab;

        int _enemyCount;

        public bool CanMove;
        public float CanMoveTimer = 0.0f;
        [SerializeField]
        float _canMoveTimeout = 1.0f;


        public bool CanJump;
        public float CanJumpTimer = 0.0f;
        [SerializeField]
        float _canJumpTimeout = 1.0f;


        public void Hit()
        {
            CanMoveTimer = _canMoveTimeout;
        }

        void Update()
        {
            _spawnTimer += Time.deltaTime;
            if (_spawnTimer > _spawnInterval)
            {
                _spawnTimer = 0f;
                SpawnEnemy();
            }

            if (CanMoveTimer > 0)
            {
                CanMoveTimer -= Time.deltaTime;
                CanMove = false;
            }
            else
            {
                CanMove = true;
            }

            if (CanJumpTimer > 0)
            {
                CanJumpTimer -= Time.deltaTime;
                CanJump = false;
            }
            else
            {
                CanJump = true;
            }
        }

        private void SpawnEnemy()
        {
            var enemy = Instantiate(_enemyPrefab);
            if (_enemyCount++ < 3)
            {
                // Force initial enemies to spawn lower to force player to jump
                enemy.transform.position = new Vector3(5, RandomService.Instance.Range(-2, -1.5f), -1);

            }
            else
            {
                enemy.transform.position = new Vector3(5, RandomService.Instance.Range(-2, 1), -1);

            }
            enemy.Power = RandomService.Instance.Range(5, 10);
            enemy.Speed = RandomService.Instance.Range(1, 5);
        }

        internal void Jump()
        {
            CanJumpTimer = _canJumpTimeout;
        }

        internal void Die()
        {
            _statusText.text = "You dead!";
            Reset();

        }

        private void Reset()
        {
            _player.transform.position = _playerSpawnPoint.transform.position;
            _player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            _enemyCount = 0;
            CanMoveTimer = 0;
            CanJumpTimer = 0;
            CanMove = true;
            CanJump = true;
            FindObjectsOfType<Enemy>().ToList().ForEach(e => Destroy(e.gameObject));
        }

        internal void Win()
        {
            _statusText.text = "You hero!";
            Reset();
        }
    }
}