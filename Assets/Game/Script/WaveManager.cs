using System.Collections;
using UnityEngine;

[System.Serializable]
public class Wave
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int count = 5;
    [SerializeField] private float spawnInterval = 1f;
    
    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }
    
    public float GetSpawnInterval()
    {
        return spawnInterval;
        
    }

    public int GetCount()
    {
        return count;
    }
}
public class WaveManager : MonoBehaviour
{
   [SerializeField] private GameManager gameManager;
   
   
   [SerializeField] private Wave[] _waves;
   [SerializeField] private float _timeBetweenWaves = 10f;
   [SerializeField] private float _countdown = 5f;
   
   private int _currentWave = 0;
   private int _enemiesAlive = 0;
   private bool _isSpawning = false;

   public int GetWaveNumber()
   {
       return _currentWave;
   }
   
   public int GetWaveCount()
   {
       return _waves.Length;
   }

   public int GetEnemiesAlive()
   {
       return _enemiesAlive;
   }
   
   public float GetCountDown()
   {
       return _countdown;
   }
   
  
    public void EnemiesRemoved()
    {
        _enemiesAlive--;
    }

    private void Update()
    {
        if (gameManager.IsGameOver()) return;
        if (_isSpawning || _enemiesAlive > 0) return;
        
        if (_currentWave >= _waves.Length)
        {
         gameManager.EndGame(true);
         return;
        }
        
        _countdown -= Time.deltaTime;
        if (_countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            _countdown = _timeBetweenWaves;
        }
        
    }
    
    public void StartWaveNow()
    {
        _countdown = 0f;
    }

    public bool IsWaiting()
    {
        return !_isSpawning && _enemiesAlive == 0 && _currentWave < _waves.Length;
    }

    private IEnumerator SpawnWave()
    {
        _isSpawning = true;
        Wave wave = _waves[_currentWave];
        _currentWave++;

        for (int i = 0; i < wave.GetCount(); i++)
        {
            SpawnEnemy(wave.GetEnemyPrefab());
            yield return new WaitForSeconds(wave.GetSpawnInterval());
        }
        
        _isSpawning = false;
        
    }

    private void SpawnEnemy(GameObject prefab)
    {
        
    }
}
