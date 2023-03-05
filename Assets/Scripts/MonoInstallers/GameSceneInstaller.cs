using Player;
using UnityEngine;
using Zenject;

namespace MonoInstallers
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private RocketInputService _inputServicePrefab;

        public override void InstallBindings()
        {
            BindInputService();
        }

        private void BindInputService()
        {
            RocketInputService inputService = Container.InstantiatePrefabForComponent<RocketInputService>(_inputServicePrefab);
            Container.Bind<RocketInputService>().FromInstance(inputService).AsSingle();
        }
    }
}