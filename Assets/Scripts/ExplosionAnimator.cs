using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Sprite[] _frames;
    [SerializeField] private float _targetSize = 2f, _growthSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        /*_renderer.sprite = _frames[0];*/
    }

    // Update is called once per frame
    void Update()
    {
        /*
        _durationCount += Time.deltaTime;
        if(_durationCount >= _duration) Destroy(gameObject);
        if(_durationCount >= _duration * 2/3)
        {
            _renderer.sprite = _frames[2];
        }else if(_durationCount >= _duration/3)
        {
            _renderer.sprite = _frames[1];
        }*/

        if(transform.localScale.x + _growthSpeed * Time.deltaTime > _targetSize) transform.localScale = new Vector2(1f,1f) * _targetSize;
        else transform.localScale += new Vector3(1f,1f,0f) * _growthSpeed * Time.deltaTime;
        
        if(transform.localScale.x >= _targetSize) Destroy(gameObject);
    }
}
