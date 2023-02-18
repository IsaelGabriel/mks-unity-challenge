using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : SignalHandler
{

    public Sprite[] Sprites;

    protected bool _isPlayer = false;
    [SerializeField] protected Rigidbody2D _body;
    private int _health = 3;
    private SpriteRenderer _srenderer;
    [SerializeField] private Transform _lifeBarContainer;
    [SerializeField] private Transform _lifeBar;
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] protected bool _dead = false;

    protected void Start()
    {
        _srenderer = gameObject.GetComponent<SpriteRenderer>();
        _srenderer.sprite = Sprites[_health];
        if(gameObject.tag == "Player") _isPlayer = true;
        GameManager.INSTANCE.AddListener(this);
        _body = gameObject.GetComponent<Rigidbody2D>();
        ShipStart();
    }

    protected abstract void ShipStart();

    protected void Update()
    {
        _lifeBarContainer.eulerAngles = new Vector3();
        ShipUpdate();
    }

    protected abstract void ShipUpdate();

    protected void TakeDamage()
    {
        if(_health <= 0) return;
        _health--;
        _srenderer.sprite = Sprites[_health];
        _lifeBar.localScale = new Vector2(_health / 3f,_lifeBar.localScale.y);
        _lifeBar.localPosition = new Vector3(-(3-_health)/6f,_lifeBar.localPosition.y,_lifeBar.localPosition.z);
        if(_health <= 0) Die();
    }

    protected abstract void Die();

    protected void OnTriggerEnter2D(Collider2D c) {
        if(c.gameObject.tag == "Cannonball")
        {
            bool ballOwnedByPlayer = c.gameObject.GetComponent<Cannonball>().OwnedByPlayer;
            if(_isPlayer != ballOwnedByPlayer)
            {
                TakeDamage();
                Destroy(c.gameObject);
            }
        }
    }

    protected void CreateBall(Transform firePoint, float xOffset, bool playerMade)
    {
        var ball = Instantiate(_ballPrefab);
    
        ball.transform.parent = firePoint;
        ball.transform.position = firePoint.position;
        ball.transform.localPosition += new Vector3(xOffset,0f,0f);
        ball.transform.rotation = firePoint.rotation;
        ball.transform.parent = null;
        ball.GetComponent<Cannonball>().OwnedByPlayer = playerMade;
    }

}
