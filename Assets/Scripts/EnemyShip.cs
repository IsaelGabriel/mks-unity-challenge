using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyShip : Ship
{
    [SerializeField] protected float _movementForce = 600f;
    [SerializeField] protected float _playerRadius = 5f;
    protected Vector2 _position = new Vector2();

    protected override void ShipStart(){}
    protected override void ShipUpdate()
    {
        if(_dead) return;
        _position = new Vector2(transform.position.x,transform.position.y);
        transform.up = EnemyManager.PlayerPosition - _position;
        if(Vector2.Distance(_position, EnemyManager.PlayerPosition) > _playerRadius)
        {
            _body.AddForce(transform.up * _movementForce * Time.deltaTime);
        }
        EnemyUpdate();
    }

    protected override void Die()
    {
        _dead = true;
        SendSignal("EnemyDead");
        Destroy(_body);
        Destroy(gameObject.GetComponent<CapsuleCollider2D>());
        StartCoroutine(SelfDestruct());
    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    protected abstract void EnemyUpdate();
}
