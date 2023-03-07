using RocketEnvironment.InputServices;
using Zenject;

namespace MonoInstallers
{
    public class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindMobileInputService();
        }

        private void BindMobileInputService()
        {
            Container.BindInterfacesTo<MobileRocketInputService>().FromNew().AsSingle();
        }
    }
}