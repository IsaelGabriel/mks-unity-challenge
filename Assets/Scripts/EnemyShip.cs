using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship
{
    [SerializeField] private float _playerRadius = 5f;
    [SerializeField] private int _killPoints = 5;

    protected override void ShipStart()
    {
        
    }
    protected override void ShipUpdate()
    {
        transform.up = EnemyManager.PlayerPosition - new Vector2(transform.position.x,transform.position.y);
        
    }
}
