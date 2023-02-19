using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserShip : EnemyShip
{
    protected override void EnemyUpdate()
    {

    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if(c.gameObject.GetComponent<Ship>())
        {
            _health = 0;
            c.gameObject.GetComponent<Ship>().TakeDamage();
            CreateObj(_greatExplosionPrefab,transform,0f,true);
            Die();
        }
    }
}
