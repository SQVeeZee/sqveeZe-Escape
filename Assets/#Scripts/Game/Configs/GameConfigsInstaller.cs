using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameConfigsInstaller", menuName = MenuPath, order = MenuOrder)]
public class GameConfigsInstaller : ScriptableObjectInstaller<GameConfigsInstaller>
{
    private const string MenuPath = "Installers/GameConfigsInstaller";
    private const int MenuOrder = int.MinValue + 100;

    [Space]
    [SerializeField] protected GameConfigs _gameConfigs = null;

    public override void InstallBindings()
    {
        Container.BindInterfacesTo<GameConfigs>().FromInstance(_gameConfigs).AsSingle().NonLazy();
    }
}
