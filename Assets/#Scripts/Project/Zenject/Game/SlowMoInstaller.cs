using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SlowMoInstaller : MonoInstaller
{
    [Header("GAME")]
    [SerializeField] protected SlowMoController _slowMoController = null;

    public override void InstallBindings()
    {
        BindControllers();
    }

    private void BindControllers()
    {
        Container.BindInterfacesAndSelfTo<SlowMoController>().FromInstance(_slowMoController).AsSingle()
            .NonLazy();
    }
}
