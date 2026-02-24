namespace Monsters.Core
{
    public interface ISceneRouter
    {
        SceneId ActiveScene { get; }
        void Load(SceneId sceneId);
    }
}
