using System.Reflection;
using Monsters.Core;
using UnityEngine;

namespace Monsters.UI
{
    public sealed class TitleMenuPresenter : MonoBehaviour
    {
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private Component continueStatusLabel;

        private void Start()
        {
            if (settingsPanel != null)
            {
                settingsPanel.SetActive(false);
            }

            SetStatusLabel("No save yet");
        }

        public void OnNewGameClicked()
        {
            if (GameBootstrapper.Instance == null)
            {
                SetStatusLabel("System loading");
                return;
            }

            GameBootstrapper.Instance.StartNewGame();
        }

        public void OnContinueClicked()
        {
            if (GameBootstrapper.Instance == null)
            {
                SetStatusLabel("System loading");
                return;
            }

            var loaded = GameBootstrapper.Instance.TryContinue();
            SetStatusLabel(loaded ? string.Empty : "No save found");
        }

        public void OnSettingsClicked()
        {
            if (settingsPanel != null)
            {
                settingsPanel.SetActive(!settingsPanel.activeSelf);
            }
        }

        private void SetStatusLabel(string value)
        {
            if (continueStatusLabel == null)
            {
                return;
            }

            var textProperty = continueStatusLabel.GetType().GetProperty("text", BindingFlags.Instance | BindingFlags.Public);
            if (textProperty != null && textProperty.CanWrite)
            {
                textProperty.SetValue(continueStatusLabel, value);
            }
        }
    }
}
