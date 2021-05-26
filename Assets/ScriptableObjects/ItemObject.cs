using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ItemObject", menuName ="ScriptableObjects/New Item Object", order = 0)]
public abstract class ItemObject : ScriptableObject
{
    public GameObject itemPrefab;
    public abstract void UseItem();
}
