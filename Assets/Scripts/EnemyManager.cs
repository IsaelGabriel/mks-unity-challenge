using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SignalHandler
{
    public static Vector2 PlayerPosition = new Vector2();
    
    [SerializeField] private float _spawnCooldown = 5f;
    [SerializeField] private float _updatePositionDelay = 0.5f;
    [SerializeField] private Vector2[] _spawnPoints;
    [SerializeField] private GameObject[] _prefabs;

    private Transform _player;
    private float _spawnCooldownCount = 0f;

    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        PlayerPosition = _player.position;
    }

    void Update()
    {
        if(Time.timeScale == 0f) return;

        _spawnCooldownCount -= Time.deltaTime;
        if(_spawnCooldownCount <= 0f) Spawn();

        StartCoroutine(UpdatePlayerPosition());
    }

    private void Spawn()
    {
        Vector2 spawnPosition = _spawnPoints[Random.Range(0,_spawnPoints.Length)];
        int enemyType = Random.Range(0,_prefabs.Length);

        var enemy = Instantiate(_prefabs[enemyType]);

        enemy.transform.position = spawnPosition;
        enemy.transform.parent = null;
        _spawnCooldownCount = _spawnCooldown;

    }


    private IEnumerator UpdatePlayerPosition()
    {
        Vector2 newPos = _player.position;
        yield return new WaitForSeconds(_updatePositionDelay);
        PlayerPosition = newPos;
    }
}
