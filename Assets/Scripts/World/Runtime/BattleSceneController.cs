using Monsters.Core;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Monsters.World.Runtime
{
    public sealed class BattleSceneController : MonoBehaviour
    {
        [SerializeField] private TMP_Text battleStatusLabel;

        private void Start()
        {
            if (battleStatusLabel != null)
            {
                battleStatusLabel.text = "Wild encounter active";
            }
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
    }
}
