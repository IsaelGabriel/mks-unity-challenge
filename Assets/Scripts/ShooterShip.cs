using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Shooter Ship behaviour
/// </summary>
public class ShooterShip : EnemyShip
{
    [SerializeField] private Transform _firePoint; // Front cannon's firePoint
    [SerializeField] private float _fireCooldown = 2f; // Minimum time between shots
    private float _fireCooldownCount = 0f; // Counts the time between shots

    protected override void EnemyUpdate() // Custom Update (inherited form EnemyShip)
    {
        if(_fireCooldownCount > 0f) // If counting time between shots
        {
            _fireCooldownCount -= Time.deltaTime; // Decrease by time between frames
            return; // Stop EnemyUpdate
        }
        if(Vector2.Distance(_position, EnemyManager.PlayerPosition) <= _playerRadius) // If distance from player is less or equal to desired
        {
            CreateBall(_firePoint,0,false); // Shoot
            _fireCooldownCount = _fireCooldown; // Start cooldown
        }
    }
}