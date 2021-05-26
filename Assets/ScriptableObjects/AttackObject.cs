using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "AttackObject", menuName = "ScriptableObjects/New Attack Object", order = 2)]
public class AttackObject : ItemObject
{
    public int attackPoints;

    [Inject]
    BarProgress attackBar;
    public override void UseItem()
    {
        attackBar.currBarValue += attackPoints;
    }

}
