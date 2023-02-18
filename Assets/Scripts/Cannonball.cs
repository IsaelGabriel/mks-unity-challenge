using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : SignalHandler
{

    [HideInInspector] public string ownerType = "player";

    [SerializeField] private float movementForce = 50f;
    private Rigidbody2D _body;

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _body.AddForce(movementForce * transform.up);
    }
    
    void Update()
    {
        
    }
}
