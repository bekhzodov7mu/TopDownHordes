using TopDownHordes.UI;
using UnityEngine;
using Zenject;

namespace TopDownHordes.Installers
{
    public class GamePlayInstaller : MonoInstaller
    {
        [SerializeField] private GamePlayUI _gamePlayUI;
        
        public override void InstallBindings()
        {
            Container.Bind<GamePlayUI>().FromInstance(_gamePlayUI).AsSingle();;
        }
    }
}