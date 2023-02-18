using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : SignalHandler
{

    [HideInInspector] public bool OwnedByPlayer = false;

    [SerializeField] private float _movementForce = 200f;
    private Rigidbody2D _body;
    private float _destroyCount = 5f;


    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _body.AddForce(_movementForce * transform.up);
    }

    void Update()
    {
        _destroyCount -= Time.deltaTime;
        if(_destroyCount <= 0f) Destroy(gameObject);    
    }
}
