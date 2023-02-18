using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Ship
{
    [SerializeField] private Rigidbody2D rb;

    // Movement

    [SerializeField] private float movementSpeed = 600f;
    [SerializeField] private float rotationSpeed = 75f;
    
    // Shooting
    
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private Transform _frontFirePoint;
    [SerializeField] private Transform _lateralFirePoint;
    [SerializeField] private float _lateralFireDistance;
    [SerializeField] private float _shootingCooldown = 0.5f;
    private float _shootingCooldownCount = 0f;
    
    protected override void ShipStart()
    {
        AddListener(GameManager.INSTANCE);
    }

    protected override void ShipUpdate()
    {
        Move();
        if(_shootingCooldownCount > 0f)
        {
            _shootingCooldownCount -= Time.deltaTime;
            return;
        }
        if(Input.GetButtonDown("Fire1"))
        {
            ShootSingle();
        }
    }

    void Move()
    {
        if(Input.GetAxis("Vertical") > 0f) rb.AddForce(transform.up * movementSpeed * Time.deltaTime);
        
        transform.Rotate(0f,0f,Input.GetAxis("Horizontal")*-rotationSpeed*Time.deltaTime);
    }

    void ShootSingle()
    {
        var ball = Instantiate(_ballPrefab);
        ball.transform.position = _frontFirePoint.position;
        ball.transform.rotation = _frontFirePoint.rotation;
        ball.transform.parent = null;
        ball.GetComponent<Cannonball>().OwnedByPlayer = true;
        _shootingCooldownCount = _shootingCooldown;
    }
}
