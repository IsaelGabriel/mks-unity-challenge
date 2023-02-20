using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Chaser Ship behaviour
/// </summary>
public class ChaserShip : EnemyShip
{
    protected override void EnemyUpdate() {} // Custom Update (Inherited from EnemyShip)

    void OnCollisionEnter2D(Collision2D c)
    {
        if(c.gameObject.GetComponent<Ship>()) // If it has collided with another ship
        {
            _health = 0; // Set health to 0
            c.gameObject.GetComponent<Ship>().TakeDamage(); // Deal damage to collided ship
            CreateObj(_greatExplosionPrefab,transform,0f,true); // Explode
            Die(); // Call Die method inherited from EnemyShip
        }
    }
}
