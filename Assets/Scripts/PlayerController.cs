using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SignalHandler
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float movementSpeed = 600f;
    [SerializeField] private float rotationSpeed = 75f;

    void Update()
    {
        Move();
    }

    void Move()
    {
        if(Input.GetAxis("Vertical") > 0f) rb.AddForce(transform.up * movementSpeed * Time.deltaTime);
        
        transform.Rotate(0f,0f,Input.GetAxis("Horizontal")*-rotationSpeed*Time.deltaTime);
    }
}
