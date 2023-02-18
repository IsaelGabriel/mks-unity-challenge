using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemyShip : EnemyShip
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireCooldown = 2f;
    private float _fireCooldownCount = 0f;

    protected override void EnemyUpdate()
    {
        if(_fireCooldownCount > 0f)
        {
            _fireCooldownCount -= Time.deltaTime;
            return;
        }
        if(Vector2.Distance(_position, EnemyManager.PlayerPosition) <= _playerRadius)
        {
            CreateBall(_firePoint,0,false);
            _fireCooldownCount = _fireCooldown;
        }
    }
}
