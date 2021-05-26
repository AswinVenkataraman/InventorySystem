using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum barType
{
    Health,
    Attack,
    Defence
}

[CreateAssetMenu(fileName = "BarProgress", menuName = "ScriptableObjects/New Bar Object", order = 99)]
public class BarProgress : ScriptableObject
{
    public barType currBarType;
    public int currBarValue;

}
