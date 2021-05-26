using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ClassDataInstaller", menuName = "Installers/ClassDataInstaller")]
public class ClassDataInstaller : MonoInstaller
{
    public InventoryManager inventoryManager;
    public override void InstallBindings()
    {
        Container.BindInstance<InventoryManager>(inventoryManager).AsSingle();
    }
}
