using UnityEngine;
using Zenject;

public class GameMonoInstaller : MonoInstaller
{
    [Header("GAME")]
    [SerializeField] protected GameController _gameController = null;
    [SerializeField] protected LevelsController _levelsController = null;

    public override void InstallBindings()
    {
        BindGame();

        BindLevels();
    }

    protected virtual void BindGame()
    {
        Container.BindInterfacesAndSelfTo<GameController>().FromInstance(_gameController).AsSingle().NonLazy();

        Debug.Log("!!!");

        //Container.BindInterfacesAndSelfTo<SlowMoManager>().FromInstance(_slowMoManager).AsSingle().NonLazy();

        //Container.BindInterfacesAndSelfTo<SlowMoManager>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();

        Debug.Log("!!!");
    }

    protected virtual void BindLevels()
    {
        Container.BindInterfacesAndSelfTo<LevelsController>().FromInstance(_levelsController).AsSingle().NonLazy();
    }
}
