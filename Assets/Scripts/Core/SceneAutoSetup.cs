using Monsters.UI;
using Monsters.World.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Monsters.Core
{
    /// <summary>
    /// Creates a minimal, playable scene shell at runtime so the project boots cleanly in a fresh Unity 6 clone.
    /// </summary>
    public static class SceneAutoSetup
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void EnsureSceneIsBootstrapped()
        {
            var sceneName = SceneManager.GetActiveScene().name;
            EnsureEventSystem();

            switch (sceneName)
            {
                case "TitleScene":
                    EnsureBootstrapper();
                    EnsureTitleScene();
                    break;
                case "TownScene":
                    EnsureTownScene();
                    break;
                case "BattleScene":
                    EnsureBattleScene();
                    break;
            }
        }

        private static void EnsureBootstrapper()
        {
            if (GameBootstrapper.Instance != null)
            {
                return;
            }

            var bootstrapObject = new GameObject("GameBootstrapper");
            bootstrapObject.AddComponent<GameBootstrapper>();
        }

        private static void EnsureTitleScene()
        {
            var camera = EnsureMainCamera(new Vector3(0f, 0f, -10f));
            camera.backgroundColor = new Color(0.06f, 0.08f, 0.12f);
            camera.clearFlags = CameraClearFlags.SolidColor;

            if (Object.FindFirstObjectByType<TitleMenuPresenter>() != null)
            {
                return;
            }

            var canvas = CreateCanvas("TitleCanvas");
            var title = CreateText(canvas.transform, "Title", "MONSTERS", 56, TextAnchor.UpperCenter, new Vector2(0.5f, 0.84f), new Vector2(0.8f, 0.15f));
            title.color = Color.white;

            var statusLabel = CreateText(canvas.transform, "Status", "No save yet", 28, TextAnchor.MiddleCenter, new Vector2(0.5f, 0.42f), new Vector2(0.8f, 0.1f));
            statusLabel.color = new Color(0.9f, 0.92f, 0.98f);

            var newGameButton = CreateButton(canvas.transform, "NewGame", "New Game", new Vector2(0.5f, 0.62f));
            var continueButton = CreateButton(canvas.transform, "Continue", "Continue", new Vector2(0.5f, 0.52f));
            var settingsButton = CreateButton(canvas.transform, "Settings", "Settings", new Vector2(0.5f, 0.32f));

            var settingsPanel = new GameObject("SettingsPanel", typeof(RectTransform), typeof(Image));
            settingsPanel.transform.SetParent(canvas.transform, false);
            var panelRect = settingsPanel.GetComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(0.2f, 0.08f);
            panelRect.anchorMax = new Vector2(0.8f, 0.24f);
            panelRect.offsetMin = Vector2.zero;
            panelRect.offsetMax = Vector2.zero;
            settingsPanel.GetComponent<Image>().color = new Color(0.12f, 0.15f, 0.2f, 0.95f);
            CreateText(settingsPanel.transform, "SettingsLabel", "Settings coming soon", 24, TextAnchor.MiddleCenter, new Vector2(0.5f, 0.5f), new Vector2(0.9f, 0.8f));
            settingsPanel.SetActive(false);

            var presenter = canvas.gameObject.AddComponent<TitleMenuPresenter>();
            presenter.Configure(settingsPanel, statusLabel);

            newGameButton.onClick.AddListener(presenter.OnNewGameClicked);
            continueButton.onClick.AddListener(presenter.OnContinueClicked);
            settingsButton.onClick.AddListener(presenter.OnSettingsClicked);
        }

        private static void EnsureTownScene()
        {
            var camera = EnsureMainCamera(new Vector3(3.5f, 3.5f, -12f));
            camera.orthographic = true;
            camera.orthographicSize = 5.5f;
            camera.backgroundColor = new Color(0.1f, 0.14f, 0.19f);

            var existing = Object.FindFirstObjectByType<TownSceneController>();
            if (existing != null)
            {
                return;
            }

            var playerVisual = GameObject.CreatePrimitive(PrimitiveType.Quad);
            playerVisual.name = "PlayerVisual";
            Object.Destroy(playerVisual.GetComponent<Collider>());
            playerVisual.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
            playerVisual.GetComponent<MeshRenderer>().material.color = new Color(0.96f, 0.82f, 0.25f);

            var floor = GameObject.CreatePrimitive(PrimitiveType.Quad);
            floor.name = "TownFloor";
            Object.Destroy(floor.GetComponent<Collider>());
            floor.transform.position = new Vector3(3.5f, 3.5f, 1f);
            floor.transform.localScale = new Vector3(8f, 8f, 1f);
            floor.GetComponent<MeshRenderer>().material.color = new Color(0.19f, 0.36f, 0.24f);

            var canvas = CreateCanvas("TownCanvas");
            var hudLabel = CreateText(canvas.transform, "Hud", "Explore", 24, TextAnchor.MiddleLeft, new Vector2(0.5f, 0.92f), new Vector2(0.96f, 0.08f));

            var controlsRoot = new GameObject("Controls", typeof(RectTransform));
            controlsRoot.transform.SetParent(canvas.transform, false);
            var controlsRect = controlsRoot.GetComponent<RectTransform>();
            controlsRect.anchorMin = new Vector2(0f, 0f);
            controlsRect.anchorMax = new Vector2(1f, 0.25f);
            controlsRect.offsetMin = Vector2.zero;
            controlsRect.offsetMax = Vector2.zero;

            var up = CreateButton(controlsRoot.transform, "Up", "↑", new Vector2(0.18f, 0.66f));
            var left = CreateButton(controlsRoot.transform, "Left", "←", new Vector2(0.08f, 0.36f));
            var right = CreateButton(controlsRoot.transform, "Right", "→", new Vector2(0.28f, 0.36f));
            var down = CreateButton(controlsRoot.transform, "Down", "↓", new Vector2(0.18f, 0.08f));
            var interact = CreateButton(controlsRoot.transform, "Interact", "Interact", new Vector2(0.8f, 0.36f), new Vector2(180f, 60f));

            var townObject = new GameObject("TownSceneController");
            var controller = townObject.AddComponent<TownSceneController>();
            controller.Configure(playerVisual.transform, camera, hudLabel);

            up.onClick.AddListener(controller.MoveUp);
            down.onClick.AddListener(controller.MoveDown);
            left.onClick.AddListener(controller.MoveLeft);
            right.onClick.AddListener(controller.MoveRight);
            interact.onClick.AddListener(controller.Interact);
        }

        private static void EnsureBattleScene()
        {
            var camera = EnsureMainCamera(new Vector3(0f, 0f, -10f));
            camera.backgroundColor = new Color(0.16f, 0.07f, 0.08f);
            camera.clearFlags = CameraClearFlags.SolidColor;

            var existing = Object.FindFirstObjectByType<BattleSceneController>();
            if (existing != null)
            {
                return;
            }

            var canvas = CreateCanvas("BattleCanvas");
            var label = CreateText(canvas.transform, "BattleStatus", "Wild encounter active", 32, TextAnchor.MiddleCenter, new Vector2(0.5f, 0.6f), new Vector2(0.9f, 0.12f));
            var returnButton = CreateButton(canvas.transform, "ReturnToTown", "Return", new Vector2(0.5f, 0.4f), new Vector2(220f, 72f));

            var battleObject = new GameObject("BattleSceneController");
            var controller = battleObject.AddComponent<BattleSceneController>();
            controller.Configure(label);
            returnButton.onClick.AddListener(controller.ReturnToTown);
        }

        private static Camera EnsureMainCamera(Vector3 position)
        {
            var camera = Camera.main;
            if (camera == null)
            {
                var cameraObject = new GameObject("Main Camera");
                cameraObject.tag = "MainCamera";
                camera = cameraObject.AddComponent<Camera>();
            }

            camera.transform.position = position;
            return camera;
        }

        private static void EnsureEventSystem()
        {
            if (Object.FindFirstObjectByType<EventSystem>() != null)
            {
                return;
            }

            var eventSystemObject = new GameObject("EventSystem");
            eventSystemObject.AddComponent<EventSystem>();
            eventSystemObject.AddComponent<StandaloneInputModule>();
        }

        private static Canvas CreateCanvas(string name)
        {
            var existing = Object.FindFirstObjectByType<Canvas>();
            if (existing != null)
            {
                return existing;
            }

            var canvasObject = new GameObject(name, typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            var canvas = canvasObject.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            var scaler = canvasObject.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1080f, 1920f);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 0.5f;

            return canvas;
        }

        private static Text CreateText(Transform parent, string name, string value, int size, TextAnchor alignment, Vector2 anchorCenter, Vector2 anchorSize)
        {
            var textObject = new GameObject(name, typeof(RectTransform), typeof(Text));
            textObject.transform.SetParent(parent, false);
            var rect = textObject.GetComponent<RectTransform>();
            rect.anchorMin = anchorCenter - (anchorSize * 0.5f);
            rect.anchorMax = anchorCenter + (anchorSize * 0.5f);
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            var text = textObject.GetComponent<Text>();
            text.text = value;
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.fontSize = size;
            text.alignment = alignment;

            return text;
        }

        private static Button CreateButton(Transform parent, string name, string label, Vector2 anchorCenter, Vector2? size = null)
        {
            var buttonObject = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(Button));
            buttonObject.transform.SetParent(parent, false);

            var rect = buttonObject.GetComponent<RectTransform>();
            var buttonSize = size ?? new Vector2(220f, 72f);
            rect.anchorMin = anchorCenter;
            rect.anchorMax = anchorCenter;
            rect.sizeDelta = buttonSize;
            rect.anchoredPosition = Vector2.zero;

            var image = buttonObject.GetComponent<Image>();
            image.color = new Color(0.2f, 0.55f, 0.9f, 0.95f);

            var labelText = CreateText(buttonObject.transform, "Label", label, 28, TextAnchor.MiddleCenter, new Vector2(0.5f, 0.5f), new Vector2(1f, 1f));
            labelText.color = Color.white;

            return buttonObject.GetComponent<Button>();
        }
    }
}
