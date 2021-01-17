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


        void Start()
        {

        }

        void Update()
        {
            _spawnTimer += Time.deltaTime;
            if (_spawnTimer > _spawnInterval)
            {
                _spawnTimer = 0f;
                SpawnEnemy();
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
            enemy.Power = RandomService.Instance.Range(5, 15);
            enemy.Speed = RandomService.Instance.Range(1, 5);
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
            FindObjectsOfType<Enemy>().ToList().ForEach(e => Destroy(e.gameObject));
        }

        internal void Win()
        {
            _statusText.text = "You hero!";
            Reset();
        }
    }
}