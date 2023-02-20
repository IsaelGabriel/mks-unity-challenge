using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Ship
{

    // Movement

    [SerializeField] private float movementSpeed = 600f;
    [SerializeField] private float rotationSpeed = 75f;
    
    // Shooting
    
    [SerializeField] private Transform _frontFirePoint;
    [SerializeField] private Transform _lateralFirePoint;
    [SerializeField] private float _lateralFireDistance;
    [SerializeField] private float _shootingCooldown = 0.5f;
    [SerializeField] private float _tripleShotForce = 40f;
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
        }else if(Input.GetButtonDown("Fire2")) ShootTriple();
    }

    void Move()
    {
        if(Input.GetAxis("Vertical") > 0f) _body.AddForce(transform.up * movementSpeed * Time.deltaTime);
        
        transform.Rotate(0f,0f,Input.GetAxis("Horizontal")*-rotationSpeed*Time.deltaTime);
    }

    void ShootSingle()
    {
        CreateBall(_frontFirePoint,0f,true);
        _shootingCooldownCount = _shootingCooldown;
    }

    void ShootTriple()
    {
        CreateBall(_lateralFirePoint,0f,true);
        CreateBall(_lateralFirePoint,_lateralFireDistance,true);
        CreateBall(_lateralFirePoint,-_lateralFireDistance,true);
        _body.AddForce(-transform.right * _tripleShotForce);
        _shootingCooldownCount = _shootingCooldown * 6;
    }

    protected override void Die()
    {
        _dead = true;
        SendSignal("ChangeScene: MatchEnd");
    }
}
