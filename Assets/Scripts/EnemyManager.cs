using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SignalHandler
{
    public static Vector2 PlayerPosition = new Vector2();
    
    [SerializeField] private float _spawnCooldown = 5f;
    [SerializeField] private float _updatePositionDelay = 0.5f;

    private Transform _player;

    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        PlayerPosition = _player.position;
    }

    void Update()
    {
        StartCoroutine(UpdatePlayerPosition());
    }

    private IEnumerator UpdatePlayerPosition()
    {
        Vector2 newPos = _player.position;
        yield return new WaitForSeconds(_updatePositionDelay);
        PlayerPosition = newPos;
    }
}
