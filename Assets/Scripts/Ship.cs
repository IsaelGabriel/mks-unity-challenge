using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : SignalHandler
{
    public Sprite[] Sprites;
    public int HP = 3;

    private SpriteRenderer _srenderer;

    void Start()
    {
        _srenderer = gameObject.GetComponent<SpriteRenderer>();
        _srenderer.sprite = Sprites[HP];
    }

    protected void TakeDamage()
    {
        if(HP <= 0) return;
        HP--;
        _srenderer.sprite = Sprites[HP];
    }

}
