using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Ship Part", menuName = "Custom Object/Ship Part")]
public class ShipPart : ScriptableObject
{
    [SerializeField] public Sprite[] sprite = new Sprite[4];
}
