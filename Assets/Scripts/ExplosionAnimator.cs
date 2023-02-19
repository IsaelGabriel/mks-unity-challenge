using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Sprite[] _frames;
    [SerializeField] private float _minSize = 0.1f, _maxSize = 2f, _growthSpeed = 20f, _destructionDelay = 0.5f;
    private float _destructionCount = 0f;


    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector2(_minSize,_minSize);
        _renderer.sprite = _frames[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(_destructionCount > 0f)
        {
            _destructionCount -= Time.deltaTime;
            if(_destructionCount <= 0f) Destroy(gameObject);
            return;
        }

        if(transform.localScale.x >= _maxSize)
        {
            _destructionCount = _destructionDelay;
            _renderer.sprite = _frames[2];
            return;
        }
        transform.localScale += new Vector3(1f,1f,0f) * Time.deltaTime * _growthSpeed;
        if(transform.localScale.x > _maxSize) transform.localScale = new Vector2(_maxSize,_maxSize);
        if(transform.localScale.x > _maxSize/2) _renderer.sprite = _frames[1];
    }
}
