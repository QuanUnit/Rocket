using RocketEnvironment;
using RocketEnvironment.InputServices;
using UnityEngine;
using Zenject;

namespace MonoInstallers
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private Vector3 _rocketSpawnPosition;
        [SerializeField] private Rocket _rocketPrefab;

        public override void InstallBindings()
        {
            BindMobileInputService();
            BindRocket();
        }

        private void BindMobileInputService()
        {
            Container.BindInterfacesTo<MobileRocketInputService>().FromNew().AsSingle();
        }

        private void BindRocket()
        {
            Rocket rocketInstance = Container.
                InstantiatePrefabForComponent<Rocket>(_rocketPrefab, _rocketSpawnPosition, Quaternion.identity, null);

            Container.Bind<Rocket>().FromInstance(rocketInstance).AsSingle();
        }
    }
}