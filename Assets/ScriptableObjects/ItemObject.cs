using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum ItemType
{
    NONE,
    POTION_HEALTH,
    POTION_ATTACK,
    POTION_DEFENCE
}

public abstract class ItemObject : ScriptableObject
{
    public GameObject itemPrefab;
    public ItemType itemType;
    public abstract void UseItem(BarProgress currProgress);

}

