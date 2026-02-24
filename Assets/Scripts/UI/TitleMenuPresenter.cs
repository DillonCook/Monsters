using Monsters.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Monsters.UI
{
    public sealed class TitleMenuPresenter : MonoBehaviour
    {
        [SerializeField] private Button newGameButton;
        [SerializeField] private Button continueButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private TMP_Text continueStatusLabel;

        private void Start()
        {
            newGameButton.onClick.AddListener(OnNewGameClicked);
            continueButton.onClick.AddListener(OnContinueClicked);
            settingsButton.onClick.AddListener(OnSettingsClicked);
            settingsPanel.SetActive(false);
            continueStatusLabel.text = "No save yet";
        }

        private void OnNewGameClicked()
        {
            if (GameBootstrapper.Instance == null)
            {
                continueStatusLabel.text = "System loading";
                return;
            }

            GameBootstrapper.Instance.StartNewGame();
        }

        private void OnContinueClicked()
        {
            if (GameBootstrapper.Instance == null)
            {
                continueStatusLabel.text = "System loading";
                return;
            }

            var loaded = GameBootstrapper.Instance.TryContinue();
            continueStatusLabel.text = loaded ? string.Empty : "No save found";
        }

        private void OnSettingsClicked()
        {
            settingsPanel.SetActive(!settingsPanel.activeSelf);
        }
    }
}
