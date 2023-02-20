using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Explosion simple animation code
/// </summary>
public class ExplosionAnimator : MonoBehaviour
{
    [SerializeField] private float _targetSize = 2f, _growthSpeed = 2f; // Variables for explosions desired size and growth speed

    void Update()
    {
        if(transform.localScale.x + _growthSpeed * Time.deltaTime > _targetSize) transform.localScale = new Vector2(1f,1f) * _targetSize; // If size is going to become bigger than desired size, make it desired size
        else transform.localScale += new Vector3(1f,1f,0f) * _growthSpeed * Time.deltaTime; // Else, grow
        
        if(transform.localScale.x >= _targetSize) Destroy(gameObject); // If size equal or bigger than desired size, destroy object
    }
}
