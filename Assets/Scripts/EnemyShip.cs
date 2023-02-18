using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship
{
    [SerializeField] protected float _movementForce = 600f;
    [SerializeField] private float _playerRadius = 5f;
    [SerializeField] private int _killPoints = 5;

    protected override void ShipStart(){}
    protected override void ShipUpdate()
    {
        Vector2 vPos = new Vector2(transform.position.x,transform.position.y);
        transform.up = EnemyManager.PlayerPosition - vPos;
        if(Vector2.Distance(vPos,EnemyManager.PlayerPosition) > _playerRadius)
        {
            _body.AddForce(transform.up * _movementForce * Time.deltaTime);
        }
    }
}
