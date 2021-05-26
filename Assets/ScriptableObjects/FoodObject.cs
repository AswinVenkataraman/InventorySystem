using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "FoodObject", menuName = "ScriptableObjects/New Food Object", order = 1)]
public class FoodObject : ItemObject
{
    public int restoreHP;

    [Inject]
    BarProgress healthBar;

    public override void UseItem()
    {
        healthBar.currBarValue += restoreHP;
    }

}
