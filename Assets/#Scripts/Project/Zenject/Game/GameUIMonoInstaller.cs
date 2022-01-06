using UnityEngine;
using Zenject;

public class GameUIMonoInstaller : MonoInstaller
{
    [Header("Controlls")]
    [SerializeField] protected UIGameClickControls _gameClickControls = null;

    [Header("Screens")]
    [SerializeField] protected UILevelCompleteScreen _levelCompleteScreen = null;
    [SerializeField] protected UILevelFailedScreen _levelFailedScreen = null;
    [SerializeField] protected UIGameScreen _gameScreen = null;
    [SerializeField] protected UITransitionScreen _transitionScreen = null;

    public override void InstallBindings()
    {
        BindControls();
        BindScreens();
    }

    protected virtual void BindControls()
    {
        Container.BindInterfacesAndSelfTo<UIGameClickControls>().FromInstance(_gameClickControls).AsSingle()
            .NonLazy();
    }

    protected virtual void BindScreens()
    {
        Container.BindInterfacesAndSelfTo<UILevelCompleteScreen>().FromInstance(_levelCompleteScreen).AsSingle()
            .NonLazy();
        Container.BindInterfacesAndSelfTo<UILevelFailedScreen>().FromInstance(_levelFailedScreen).AsSingle()
            .NonLazy();
        Container.BindInterfacesAndSelfTo<UIGameScreen>().FromInstance(_gameScreen).AsSingle()
            .NonLazy();
        Container.BindInterfacesAndSelfTo<UITransitionScreen>().FromInstance(_transitionScreen).AsSingle()
            .NonLazy();
    }
}
