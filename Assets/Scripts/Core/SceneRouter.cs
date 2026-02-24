using System;
using UnityEngine.SceneManagement;

namespace Monsters.Core
{
    public sealed class SceneRouter : ISceneRouter
    {
        public SceneId ActiveScene { get; private set; } = SceneId.Title;

        public void Load(SceneId sceneId)
        {
            var sceneName = sceneId switch
            {
                SceneId.Title => "TitleScene",
                SceneId.Town => "TownScene",
                SceneId.Battle => "BattleScene",
                _ => throw new ArgumentOutOfRangeException(nameof(sceneId), sceneId, "Unknown scene id")
            };

            ActiveScene = sceneId;
            SceneManager.LoadScene(sceneName);
        }
    }
}
