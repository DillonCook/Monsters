using System.Reflection;
using Monsters.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Monsters.World.Runtime
{
    public sealed class BattleSceneController : MonoBehaviour
    {
        [SerializeField] private Component battleStatusLabel;

        public void Configure(Component battleStatusLabelReference)
        {
            battleStatusLabel = battleStatusLabelReference;
        }

        private void Start()
        {
            SetLabelText("Wild encounter active");
        }

        public void ReturnToTown()
        {
            WorldRuntimeState.Instance.ReturnFromBattle();

            if (GameBootstrapper.Instance != null)
            {
                GameBootstrapper.Instance.StartNewGame();
                return;
            }

            SceneManager.LoadScene("TownScene");
        }

        private void SetLabelText(string value)
        {
            if (battleStatusLabel == null)
            {
                return;
            }

            var textProperty = battleStatusLabel.GetType().GetProperty("text", BindingFlags.Instance | BindingFlags.Public);
            if (textProperty != null && textProperty.CanWrite)
            {
                textProperty.SetValue(battleStatusLabel, value);
            }
        }
    }
}
