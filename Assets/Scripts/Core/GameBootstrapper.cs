using Monsters.Save;
using UnityEngine;

namespace Monsters.Core
{
    public sealed class GameBootstrapper : MonoBehaviour
    {
        private ServiceRegistry _services;
        private ISceneRouter _sceneRouter;
        private AppState _appState = AppState.Booting;

        public static GameBootstrapper Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            _services = new ServiceRegistry();
            _services.Register<ISaveService>(new LocalSaveService());
            _sceneRouter = new SceneRouter();

            _appState = AppState.Title;
            _sceneRouter.Load(SceneId.Title);
        }

        public void StartNewGame()
        {
            _appState = AppState.InTown;
            _sceneRouter.Load(SceneId.Town);
        }

        public bool TryContinue()
        {
            var saveService = _services.Resolve<ISaveService>();
            if (saveService == null || !saveService.HasSave())
            {
                return false;
            }

            _appState = AppState.InTown;
            _sceneRouter.Load(SceneId.Town);
            return true;
        }

        public void EnterBattlePlaceholder()
        {
            _appState = AppState.InBattle;
            _sceneRouter.Load(SceneId.Battle);
        }

        public AppState CurrentState => _appState;
    }
}
