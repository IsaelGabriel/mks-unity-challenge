using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cannonball behaviour
/// <summary>
public class Cannonball : MonoBehaviour
{
    [HideInInspector] public bool OwnedByPlayer = false; // Made to be checked when hitting a ship, define by parent ship

    [SerializeField] private float _movementForce = 200f; // Force added when created
    private Rigidbody2D _body; // Object's Rigidbody2D
    private float _destroyCount = 5f; // Countdown for self destruction in case it doesn't hit anything

    void Start()
    {
        _body = GetComponent<Rigidbody2D>(); // Get object's Rigidbody2D
        _body.AddForce(_movementForce * transform.up); // Add force forward
    }

    void Update()
    {
        _destroyCount -= Time.deltaTime; // Decrease countdown
        if(_destroyCount <= 0f) Destroy(gameObject); // Destroy object in case it stays alive for more than 5 seconds
    }
}
