using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GunInstaller : MonoInstaller
{
    [Header("GAME")]
    [SerializeField] protected GunController _gunController = null;

    public override void InstallBindings()
    {
        BindControllers();
    }

    private void BindControllers()
    {
        Container.BindInterfacesAndSelfTo<GunController>().FromInstance(_gunController).AsSingle()
            .NonLazy();
    }
}
