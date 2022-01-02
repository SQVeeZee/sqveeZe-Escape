using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindTools();
    }

    protected virtual void BindTools()
    {
        Container.BindInterfacesAndSelfTo<MonoBehaviourController>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
    }
}
