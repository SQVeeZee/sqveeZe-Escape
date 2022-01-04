using UnityEngine;
using Zenject;

public class GameMonoInstaller : MonoInstaller
{
    [Header("GAME")]
    [SerializeField] protected LevelsController _levelsController = null;

    public override void InstallBindings()
    {
        BindGame();

        BindLevels();
    }

    protected virtual void BindGame()
    {
        //Container.BindInterfacesAndSelfTo<SlowMoManager>().FromInstance(_levelsController).AsSingle().NonLazy();

        //Container.BindInterfacesAndSelfTo<SlowMoManager>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
    }

    protected virtual void BindLevels()
    {
        Container.BindInterfacesAndSelfTo<LevelsController>().FromInstance(_levelsController).AsSingle().NonLazy();
    }
}
