using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "DefenceObject", menuName = "ScriptableObjects/New Defence Object", order = 2)]
public class DefenceObject : ItemObject
{
    public int defencePoints;

    [Inject]
    BarProgress defenceBar;
    public override void UseItem()
    {
        defenceBar.currBarValue += defencePoints;
    }

}
