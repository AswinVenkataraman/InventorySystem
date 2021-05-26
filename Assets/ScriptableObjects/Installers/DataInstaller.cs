using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "DataInstaller", menuName = "Installers/DataInstaller")]
public class DataInstaller : ScriptableObjectInstaller<DataInstaller>
{
    public List<InventoryData> playerInventoryData;
    public List<InventoryData> objectInventoryData;

    public BarProgress healthBar;
    public BarProgress attackBar;
    public BarProgress defenceBar;

    public override void InstallBindings()
    {
        Container.BindInstance<List<InventoryData>>(playerInventoryData).When(context => string.Equals("playerInventoryData", context.MemberName));
        Container.BindInstance<List<InventoryData>>(objectInventoryData).When(context => string.Equals("objectInventoryData", context.MemberName));

        Container.BindInstance<BarProgress>(healthBar).When(context => string.Equals("healthBar", context.MemberName));
        Container.BindInstance<BarProgress>(attackBar).When(context => string.Equals("attackBar", context.MemberName)); ;
        Container.BindInstance<BarProgress>(defenceBar).When(context => string.Equals("defenceBar", context.MemberName)); ;

    }
}

public enum ItemType
{
    NONE,
    POTION_HEALTH,
    POTION_ATTACK,
    POTION_DEFENCE
}

[System.Serializable]
public class InventoryData
{
    public ItemObject itemObj;
    public ItemType itemType;
    public int amount;
}
