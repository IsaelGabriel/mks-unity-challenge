using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base code for enemies
///</summary>
public abstract class EnemyShip : Ship
{
    [SerializeField] protected float _playerRadius = 5f; // Desired distance from player

    protected Vector2 _position = new Vector2(); // Current position

    protected override void ShipStart(){} // Custom Start (Inherited from Ship)

    protected override void ShipUpdate() // Custom Update (Inherited from Ship)
    {
        if(_dead) return; // Doesn't update if EnemyShip is dead
        _position = new Vector2(transform.position.x,transform.position.y); // Update _position
        transform.up = EnemyManager.PlayerPosition - _position; //Rotate Based on PlayerPosition's value
        if(Vector2.Distance(_position, EnemyManager.PlayerPosition) > _playerRadius) // If distance from player is bigger than desired distance
        {
            _body.AddForce(transform.up * _movementForce * Time.deltaTime); // Move forward
        }
        EnemyUpdate();
    }

    protected abstract void EnemyUpdate(); // Custom child Update

    protected override void Die() // Custom death method (Inherited from Ship)
    {
        _dead = true;
        Destroy(_body); // Destroy Rigidbody2D
        Destroy(gameObject.GetComponent<CapsuleCollider2D>()); // Destroy Collider
        StartCoroutine(SelfDestruct());
    }

    private IEnumerator SelfDestruct() // Delay object's destruction
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}