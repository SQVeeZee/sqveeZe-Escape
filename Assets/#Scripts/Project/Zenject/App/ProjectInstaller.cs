using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SetConfigs();

        BindControllers();
        BindTools();
    }

    protected virtual void SetConfigs()
    {
        Application.targetFrameRate = 60;
        Input.multiTouchEnabled = false;
    }

    protected virtual void BindControllers()
    {
        //Container.BindInterfacesAndSelfTo<TimeScaleController>().FromNew().AsSingle().NonLazy();
    }

    protected virtual void BindTools()
    {

    }
}
