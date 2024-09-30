using Zenject;

public class EventBusInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<EventBus>().FromNew().AsSingle();
    }
}