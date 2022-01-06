using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GunInstaller : MonoInstaller
{
    [Header("GAME")]
    [SerializeField] protected GunController _gunController = null;

    [Header("Configs")]
    [SerializeField] protected GunConfigs _gunConfigs = null;

    public override void InstallBindings()
    {
        BindControllers();

        BindConfigs();
    }

    private void BindControllers()
    {
        Container.BindInterfacesAndSelfTo<GunController>().FromInstance(_gunController).AsSingle()
            .NonLazy();
    }

    protected virtual void BindConfigs()
    {
        Container.Bind<IGunConfigs>().FromInstance(_gunConfigs).AsSingle().NonLazy();
    }
}
