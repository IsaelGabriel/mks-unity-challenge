using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Ship
{

    // Movement

    [SerializeField] private float _movementSpeed = 150f; // Force applied for movement 
    [SerializeField] private float _rotationSpeed = 75f; //  Speed in which the player rotates
    
    // Shooting
    
    [SerializeField] private Transform _frontFirePoint; // Front cannon's fire point transform
    [SerializeField] private Transform _lateralFirePoint; // Middle side cannon's fire point transform
    [SerializeField] private float _lateralFireDistance; // Offset for side cannons
    [SerializeField] private float _shootingCooldown = 0.5f; // Cooldown applied when shooting, get multiplied by 6 when triple shooting
    [SerializeField] private float _tripleShotRecoil = 40f; // Force applied to player after triple shooting
    private float _shootingCooldownCount = 0f; // Made for counting time between shots
    
    protected override void ShipStart(){} // Custom Start (Inherited from Ship)

    protected override void ShipUpdate() // Custom Update (Inherited from Ship)
    {
        Move();
        if(_shootingCooldownCount > 0f)
        {
            _shootingCooldownCount -= Time.deltaTime;
            return;
        }
        if(Input.GetButton("Fire1"))
        {
            ShootSingle();
        }else if(Input.GetButton("Fire2")) ShootTriple();
    }

    void Move() // Apply movement force and rotate
    {
        if(Input.GetAxis("Vertical") > 0f) _body.AddForce(transform.up * _movementSpeed * Time.deltaTime); // If 'W' or 'Up' are pressed, apply force forward
        
        transform.Rotate(0f,0f,Input.GetAxis("Horizontal")*-_rotationSpeed*Time.deltaTime); // Rotate based on the "Horizontal" axis' value
    }

    void ShootSingle() // Shoot a single cannonball
    {
        CreateBall(_frontFirePoint,0f,true); // Create a cannonball at the front cannon's fire point
        _shootingCooldownCount = _shootingCooldown; // Start cooldown count
    }

    void ShootTriple() // Shoot three cannonballs from the side cannons
    {
        CreateBall(_lateralFirePoint,0f,true); // Create cannonball at middle side cannon
        CreateBall(_lateralFirePoint,_lateralFireDistance,true); // Create cannonball at right side cannon
        CreateBall(_lateralFirePoint,-_lateralFireDistance,true);// Create cannonball at left side cannon
        _body.AddForce(-transform.right * _tripleShotRecoil); // Apply recoil to player
        _shootingCooldownCount = _shootingCooldown * 6; // Start cooldown count
    }

    protected override void Die() // Custom death function (Inherited from Ship)
    {
        _dead = true;
        SendSignal("ChangeScene: MatchEnd"); // Send signal for the current scene to change
    }
}
