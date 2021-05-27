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
        Container.BindInstance<List<InventoryData>>(playerInventoryData).When(context => string.Equals("playerInventoryData", context.MemberName)).NonLazy();
        Container.BindInstance<List<InventoryData>>(objectInventoryData).When(context => string.Equals("objectInventoryData", context.MemberName)).NonLazy();

        Container.BindInstance<BarProgress>(healthBar).AsSingle();

        /*Container.BindInstance<BarProgress>(healthBar).AsCached().When(context => string.Equals("healthBar", context.MemberName)).NonLazy();
        Container.BindInstance<BarProgress>(attackBar).AsCached().When(context => string.Equals("attackBar", context.MemberName)).NonLazy();
        Container.BindInstance<BarProgress>(defenceBar).AsCached().When(context => string.Equals("defenceBar", context.MemberName)).NonLazy();*/

        Container.QueueForInject(playerInventoryData);
        Container.QueueForInject(objectInventoryData);
        Container.QueueForInject(healthBar);
       // Container.QueueForInject(attackBar);
       // Container.QueueForInject(defenceBar);

    }

}

[System.Serializable]
public class BarProgress
{
    public barType currBarType;
    public int currBarValue;
}

[System.Serializable]
public class InventoryData
{
    public ItemObject itemObj;
    public int amount;
}
