using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : SignalHandler
{

    public Sprite[] Sprites;

    private int _health = 3;
    private SpriteRenderer _srenderer;
    [SerializeField] private Transform _lifeBarContainer;
    [SerializeField] private Transform _lifeBar;

    protected void Start()
    {
        _srenderer = gameObject.GetComponent<SpriteRenderer>();
        _srenderer.sprite = Sprites[_health];
    }

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
    }



}