using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy Manager behaviour
/// </summary>
public class EnemyManager : SignalHandler
{
    public static Vector2 PlayerPosition = new Vector2(); // Delayed Player Position sent to all EnemyShips 
    
    // Editor variables
    [SerializeField] private float _minPlayerDistance = 10f; // Minimal player distance when spawning enemies
    [SerializeField] private float _updatePositionDelay = 0.5f; // PlayerPosition update delay
    [SerializeField] private Vector2 _minSpawnPoint, _maxSpawnPoint; // Range from where enemies can be spawned
    [SerializeField] private GameObject[] _prefabs; // Enemy prefabs

    // Private variables
    private Transform _player; // Player transform
    private float _spawnCooldownCount = 0f; // Count for time between enemy spawns

    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Transform>(); // Find Player
        PlayerPosition = _player.position; // Get Player's initial position
    }

    void Update()
    {
        if(Time.timeScale <= 0f) return; // Doesn't update if timeScale is equal or less than 0

        _spawnCooldownCount -= Time.deltaTime; // Decrease _spawnCooldownCount by time between frames
        if(_spawnCooldownCount <= 0f) Spawn(); // Spawn an enemy if _spawnCooldownCount has ended

        StartCoroutine(UpdatePlayerPosition()); // Update PlayerPosition with delay
    }

    private void Spawn() // Spawns an enemy
    {
        Vector3 spawnPosition = GetSpawnPosition(); // Get a random spawn position for enemies
        int enemyType = Random.Range(0,_prefabs.Length); // Get a random enemy type (_prefabs index)

        var enemy = Instantiate(_prefabs[enemyType]); // Instantiate enemy type

        enemy.transform.position = spawnPosition; // Set enemy's position to randomly generated position
        enemy.transform.parent = null;
        _spawnCooldownCount = GameManager.EnemySpawnTime; // Reset cooldown count

    }

    private Vector2 GetSpawnPosition() // Get a random spawn position
    {
        Vector3 spawnPosition = new Vector3(Random.Range(_minSpawnPoint.x,_maxSpawnPoint.x),Random.Range(_minSpawnPoint.y,_maxSpawnPoint.y),0f); // Generate random position
        if(Vector3.Distance(spawnPosition,_player.position) < _minPlayerDistance) return GetSpawnPosition(); // If spawnPosition's distance position is less than desired, run GetSpawnPosition again 
        return spawnPosition;
    }


    private IEnumerator UpdatePlayerPosition() // Update PlayerPosition variable with delay
    {
        Vector2 newPos = _player.position; // Get current position
        yield return new WaitForSeconds(_updatePositionDelay); // Delay it by _updatePositionDelay's value
        PlayerPosition = newPos; // Update PlayerPosition
    }
}
