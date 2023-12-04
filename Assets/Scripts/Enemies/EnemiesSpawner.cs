using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class EnemiesSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public Enemy enemyPrefab;
        public int enemyCount;
    }

   
    private int _enemiesInScene = 3;
    [SerializeField] private Transform[] _spawnPoints;

    [SerializeField] private TextMeshProUGUI _killCountUIText;
    [SerializeField] private TextMeshProUGUI _gameOverkillCountUIText;

    [SerializeField] private Wave[] _waves;
    private int _waveIndex = 0;
    private bool HasWaves => _waveIndex < _waves.Length;

    private int _killCount = 0;
    public List<Enemy> SpawnedEnemies { get; } = new List<Enemy>();

    [SerializeField] private Player _player;

    private void Start()
    {
        _killCountUIText.text = "Killed Enemies: " + _killCount.ToString();
        SpawnNextWave();
    }
    private void Update()
    {
        UpdateKillCountUI();
    }

    private void SpawnNextWave()
    {
        if (HasWaves)
        {
            Spawn();
            _waveIndex++;
        }
    }

    private void Spawn()
    {
        var currentWave = _waves[_waveIndex];
        for (int i = 0; i < currentWave.enemyCount; i++)
        {
           int  spawnPoint = Random.Range(0, _spawnPoints.Length);
            Enemy enemy = Instantiate(currentWave.enemyPrefab, _spawnPoints[spawnPoint].position,_spawnPoints[spawnPoint].rotation);
            SpawnedEnemies.Add(enemy);

            enemy.OnDied.AddListener(() => OnEnemyDead(enemy));
        }
    }

    private void OnEnemyDead(Enemy enemy)
    {
        _killCount++;
        UpdateKillCountUI();
        SpawnedEnemies.Remove(enemy);

        if (SpawnedEnemies.Count <= _enemiesInScene && HasWaves)
        {
            SpawnNextWave();
        }
    }

    private void UpdateKillCountUI()
    {
        _killCountUIText.text = "Killed Enemies: " + _killCount.ToString();
        _gameOverkillCountUIText.text=_killCountUIText.text;
    }
}