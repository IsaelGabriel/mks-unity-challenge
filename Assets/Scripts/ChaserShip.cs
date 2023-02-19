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
        if(c.gameObject.tag == "Player")
        {
            _health = 0;
            c.gameObject.GetComponent<PlayerController>().TakeDamage();
            CreateObj(_greatExplosionPrefab,transform,0f,true);
            Die();
        }
    }
}
