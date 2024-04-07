using System;
using System.Collections.Generic;
using System.Threading;
using Collections.Scopes;
using Cysharp.Threading.Tasks;
using TopDownHordes.Enemies;
using TopDownHordes.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TopDownHordes
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Scriptable Objects")]
        [SerializeField] private LevelBalanceData _levelBalanceData;
        
        [Header("Stats")]
        [SerializeField] private float _spawnInterval = 1f;
        [SerializeField] private int _maxEnemyCount = 10;
        [SerializeField] private float _spawnPadding = 1f; // Space outside camera view 
        
        [Header("World")]
        [SerializeField] private Transform _player;

        private readonly HashSet<EnemyLinker> _spawnedEnemies = new();

        private EnemiesBalanceInfo _currentBalanceInfo;
        
        private CancellationTokenSource _spawnCancellationTokenSource = new();
        private Camera _camera;

        private float _timer;
        private int _progressionIndex;
        
        private void Awake()
        {
            _camera = Camera.main;
        }

        private async void Start()
        {
            await AdjustBalance(gameObject.GetCancellationTokenOnDestroy());
            SpawnEnemies(_spawnCancellationTokenSource.Token).Forget();
        }

        private async UniTask AdjustBalance(CancellationToken cancellationToken)
        {
            while (_progressionIndex < _levelBalanceData.EnemiesBalanceInfo.Length - 1)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancellationToken);
                _timer++;

                if (_timer >= _levelBalanceData.EnemiesBalanceInfo[_progressionIndex].TimeRequired)
                {
                    _progressionIndex++;
                    _currentBalanceInfo = _levelBalanceData.EnemiesBalanceInfo[_progressionIndex];
                }
            }
        }

        private async UniTask SpawnEnemies(CancellationToken cancellationToken)
        {
            while (_spawnedEnemies.Count < _maxEnemyCount)
            {
                SpawnEnemy();
                await UniTask.Delay(TimeSpan.FromSeconds(_spawnInterval), cancellationToken: cancellationToken);
            }
        }
        
        private void SpawnEnemy() 
        {
            Vector3 spawnPosition = GetRandomSpawnPoint();
            
            var enemy = Instantiate(GetRandomEnemy(), spawnPosition, Quaternion.identity);
             enemy.EnemyMovement.SetTarget(_player);
             enemy.EnemyHealth.OnEnemyDied += OnEnemyDied;

            _spawnedEnemies.Add(enemy);
        }

        private void OnEnemyDied(EnemyLinker enemyLinker)
        {
            enemyLinker.EnemyHealth.OnEnemyDied -= OnEnemyDied;
            _spawnedEnemies.Remove(enemyLinker);
            
            _spawnCancellationTokenSource?.Cancel();
            
            _spawnCancellationTokenSource = new CancellationTokenSource();
            SpawnEnemies(_spawnCancellationTokenSource.Token).Forget();
        }

        private EnemyLinker GetRandomEnemy()
        {
            // Convert spawn probabilities into accumulative percentages
            using (HashSetScope<(EnemyLinker, float)>.Create(out var accumulativeSpawnPercentages))
            {
                float accumulativePercentage = 0.0f;
                foreach ((EnemyLinker linker, float probability) in _currentBalanceInfo.EnemiesSpawnProbability)
                {
                    accumulativePercentage += probability;
                    accumulativeSpawnPercentages.Add((linker, accumulativePercentage));
                }

                // Generate a random value between 0 and the accumulative maximum
                float randomValue = Random.Range(0.0f, accumulativePercentage);

                // Select the EnemyLinker whose range contains the generated random value
                foreach ((EnemyLinker enemy, float spawnPercentage) in accumulativeSpawnPercentages)
                {
                    if (randomValue <= spawnPercentage)
                    {
                        return enemy;
                    }
                }
            }

            // In case no range was found (which should not happen if the probabilities are properly normalized), just return the first enemy
            //return _currentBalanceInfo.EnemiesSpawnProbability.Keys.First();
            throw new Exception("");
        }
        
        private Vector3 GetRandomSpawnPoint()
        {
            // Get camera boundaries
            float left = _camera.ViewportToWorldPoint(new Vector3(0, 0)).x;
            float right = _camera.ViewportToWorldPoint(new Vector3(1, 0)).x;
            float bottom = _camera.ViewportToWorldPoint(new Vector3(0, 0)).y;
            float top = _camera.ViewportToWorldPoint(new Vector3(0, 1)).y;

            // Determine a random side from which to spawn the enemy (0 = top, 1 = right, 2 = bottom, 3 = left)
            int side = Random.Range(0, 4);

            switch (side)
            {
                case 0: // Top
                    return new Vector3(Random.Range(left, right), top + _spawnPadding);
                case 1: // Right
                    return new Vector3(right + _spawnPadding, Random.Range(bottom, top));
                case 2: // Bottom
                    return new Vector3(Random.Range(left, right), bottom - _spawnPadding);
                case 3: // Left
                    return new Vector3(left - _spawnPadding, Random.Range(bottom, top));
                default:
                    throw new Exception($"Wrong side number {side}!");
            }
        }
    }
}