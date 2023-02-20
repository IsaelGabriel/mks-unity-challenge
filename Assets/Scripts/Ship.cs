using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all the ships in the game (Player and enemies)
/// </summary>
public abstract class Ship : SignalHandler
{

    public Sprite[] Sprites; // List of ship state sprites (Sprites[0] --> Full health, Sprites[3] --> Ship wrecked)
    protected bool _isPlayer = false;
    protected int _health = 3; // Ship health

    // Components
    protected Rigidbody2D _body; // Ship's Rigidbody2D
    private SpriteRenderer _srenderer; // Ship's SpriteRenderer

    // Editor variables
    [SerializeField] private Transform _lifeBarContainer; // Object that parents Ship's life bar
    [SerializeField] private Transform _lifeBar; // Ship's life bar transform
    [SerializeField] private GameObject _ballPrefab, _miniExplosionPrefab; // Prefabs for Cannonball and mini Explosion (created when shooting)
    [SerializeField] protected GameObject _greatExplosionPrefab; // Prefab for Great Explosion (created on Ship's death)
    [SerializeField] protected bool _dead = false; // Made to check before update so ships don't move while dead

    protected void Start()
    {
        _srenderer = gameObject.GetComponent<SpriteRenderer>(); // Get SpriteRenderer
        _srenderer.sprite = Sprites[_health]; // Set Sprite to current health (3)
        if(gameObject.tag == "Player") _isPlayer = true; // if Game Object's tag is "Player", then mark the bool
        AddListener(GameManager.INSTANCE); // Add the current Game Manager to the Ship's Listener List
        _body = gameObject.GetComponent<Rigidbody2D>(); // Get Rigidbody2D
        ShipStart();
    }

    protected abstract void ShipStart(); // Custom Start for child classes

    protected void Update()
    {
        if(Time.timeScale <= 0f) return; // Update Doesn't run if time scale is 0 or less
        _lifeBarContainer.eulerAngles = new Vector3(); // Reset _lifeBarContainer's rotation
        ShipUpdate();
    }

    protected abstract void ShipUpdate(); // Custom Update for child classes

    public void TakeDamage() // Ship takes 1 damage
    {
        if(_health <= 0) return; // If Ship is already dead, return
        _health--; // Decrease health by 1
        _srenderer.sprite = Sprites[_health]; // Update Ship's sprite to match its health
        _lifeBar.localScale = new Vector2(_health / 3f,_lifeBar.localScale.y); // Update Ship's life bar to match its health
        _lifeBar.localPosition = new Vector3(-(3-_health)/6f,_lifeBar.localPosition.y,_lifeBar.localPosition.z); // Reset Ship's life bar position
        if(_health <= 0)
        {
            if(!_isPlayer) SendSignal("EnemyDead"); // Emmit signal to all Listeners
            CreateObj(_greatExplosionPrefab,transform,0f,true); // Create Great Explosion
            Die();
        }
    }

    protected abstract void Die(); // Custom death code for child classes

    protected void OnTriggerEnter2D(Collider2D c) {
        if(c.gameObject.tag == "Cannonball")
        {
            bool ballOwnedByPlayer = c.gameObject.GetComponent<Cannonball>().OwnedByPlayer;
            if(!_isPlayer || (_isPlayer && !ballOwnedByPlayer)) // If this ship is an EnemyShip or a Player hit by an enemy
            {
                TakeDamage();
                Destroy(c.gameObject);
            }
        }
    }


    protected GameObject CreateObj(GameObject prefab, Transform creationPoint, float xOffset, bool parentedByThis) // Instantiate a prefab
    {
        var obj = Instantiate(prefab);

        obj.transform.parent = creationPoint;
        obj.transform.position = new Vector3(creationPoint.position.x,creationPoint.position.y,prefab.transform.localPosition.z); // Make its x and y position match creationPoint and its z position match the prefab
        obj.transform.localPosition += new Vector3(xOffset,0f,0f); // Change its position by xOffset's value
        obj.transform.rotation = creationPoint.rotation; // Make its rotation match creationPoint
        obj.transform.parent = (parentedByThis)? transform : null; // Set its parent
        return obj;
    }

    protected void CreateBall(Transform firePoint, float xOffset, bool playerMade) // Shoot a cannonball
    {
        CreateObj(_miniExplosionPrefab,firePoint,xOffset,true); // Create a mini explosion at firePoint
        CreateObj(_ballPrefab,firePoint,xOffset,false).GetComponent<Cannonball>().OwnedByPlayer = playerMade; // Create a cannonball and set its OwnedByPlayer property
    }

}
