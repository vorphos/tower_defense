using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float _speed = 3f;
    [SerializeField] private int _health = 10;
    [SerializeField] private int _reward = 10;
    [SerializeField] private int _damage = 1;

    private GameManager _gameManager;
    private WaveManager _waveManager;
    private Waypoints _waypoints;

    private int _waypointsIndex = 0;
    private bool _isDead = false;

    public void Init(GameManager newGameManager, WaveManager newWaveManager, Waypoints newWaypoints)
    {
        _gameManager = newGameManager;
        _waveManager = newWaveManager;
        _waypoints = newWaypoints;
    }

    private void Update()
    {
        Transform target = _waypoints.GetPoint(_waypointsIndex);

        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
        transform.LookAt(target.position);

        if (Vector3.Distance(transform.position, target.position) <= 0.05f)
        {
            _waypointsIndex++;

            if (_waypointsIndex >= _waypoints.GetCount())
            {
                ReachEnd();
            }
        }


    }

    public void TakeDamage(int damage)
    {
        if (_isDead) return;
        _health -= damage;

        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _isDead = true;
        _gameManager.AddMoney(_reward);
        _waveManager.EnemiesRemoved();
        Destroy(gameObject);
    }

    private void ReachEnd()
    {
        _isDead = true;
        _gameManager.LoseLife(_damage);
        _waveManager.EnemiesRemoved();
        Destroy(gameObject);
    }
}        

