using System.Collections.Generic;
using Monsters.Core;
using Monsters.World.Encounter;
using Monsters.World.Movement;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Monsters.World.Runtime
{
    public sealed class TownSceneController : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Transform playerVisual;
        [SerializeField] private Camera worldCamera;
        [SerializeField] private TMP_Text hudLabel;

        [Header("Map")]
        [SerializeField] private Vector2Int mapSize = new(12, 16);
        [SerializeField] private Vector2Int playerStart = new(1, 1);
        [SerializeField] private float gridCellSize = 1f;
        [SerializeField] private List<Vector2Int> blockedTiles = new();
        [SerializeField] private List<Vector2Int> biomeTiles = new();

        [Header("Encounter")]
        [Range(0f, 1f)]
        [SerializeField] private float biomeEncounterChance = 0.2f;
        [SerializeField] private int encounterSeed = 42;

        [Header("Mobile Input")]
        [SerializeField] private bool enableSwipeInput = true;
        [SerializeField] private float minSwipeDistanceInches = 0.35f;
        [SerializeField] private float swipeFallbackDpi = 160f;
        [SerializeField] private float movementCooldownSeconds = 0.08f;

        private PlayerGridMover _playerMover;
        private GridWorldMap _map;
        private EncounterTriggerService _encounterTriggerService;
        private SwipeDirectionInput _swipeDirectionInput;
        private Vector2 _touchStart;
        private bool _isTouchTracking;
        private float _nextAllowedMoveTime;

        private void Awake()
        {
            _map = new GridWorldMap(BuildTileMap());
            _playerMover = new PlayerGridMover(_map, playerStart);

            var rng = new SeededEncounterRng(encounterSeed);
            _encounterTriggerService = new EncounterTriggerService(rng, new EncounterConfig(biomeEncounterChance));
            var swipeThreshold = SwipeDirectionInput.CalculateThresholdPixels(minSwipeDistanceInches, swipeFallbackDpi);
            _swipeDirectionInput = new SwipeDirectionInput(swipeThreshold);

            _nextAllowedMoveTime = Time.unscaledTime + movementCooldownSeconds;
            RenderPlayer();
            UpdateHud("Explore");
        }

        private void Update()
        {
            if (!enableSwipeInput)
            {
                return;
            }

            HandleSwipeInput();
        }

        public void MoveUp() => TryMove(GridDirection.Up);
        public void MoveDown() => TryMove(GridDirection.Down);
        public void MoveLeft() => TryMove(GridDirection.Left);
        public void MoveRight() => TryMove(GridDirection.Right);

        private void HandleSwipeInput()
        {
            if (Input.touchCount <= 0)
            {
                return;
            }

            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                _touchStart = touch.position;
                _isTouchTracking = true;
                return;
            }

            if (!_isTouchTracking)
            {
                return;
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                _isTouchTracking = false;

                if (_swipeDirectionInput.TryGetDirection(_touchStart, touch.position, out var direction))
                {
                    TryMove(direction);
                }
            }
        }

        private void TryMove(GridDirection direction)
        {
            if (Time.unscaledTime < _nextAllowedMoveTime)
            {
                return;
            }
            if (!_playerMover.TryMove(direction))
            {
                UpdateHud("Blocked");
                return;
            }

            RenderPlayer();

            if (!_encounterTriggerService.TryTriggerEncounter(_map, _playerMover.Position))
            {
                UpdateHud("Move");
                return;
            }

            WorldRuntimeState.Instance.SetCameraPosition(worldCamera != null ? worldCamera.transform.position : Vector3.zero);
            WorldRuntimeState.Instance.EnterBattle();
            UpdateHud("Encounter!");

            if (GameBootstrapper.Instance != null)
            {
                GameBootstrapper.Instance.EnterBattlePlaceholder();
                return;
            }

            SceneManager.LoadScene("BattleScene");
        }

        private WorldTileType[,] BuildTileMap()
        {
            var tiles = new WorldTileType[mapSize.x, mapSize.y];

            foreach (var point in blockedTiles)
            {
                if (IsWithinMap(point))
                {
                    tiles[point.x, point.y] = WorldTileType.Blocked;
                }
            }

            foreach (var point in biomeTiles)
            {
                if (IsWithinMap(point) && tiles[point.x, point.y] != WorldTileType.Blocked)
                {
                    tiles[point.x, point.y] = WorldTileType.Biome;
                }
            }

            return tiles;
        }

        private bool IsWithinMap(Vector2Int point)
        {
            return point.x >= 0 && point.y >= 0 && point.x < mapSize.x && point.y < mapSize.y;
        }

        private void RenderPlayer()
        {
            if (playerVisual != null)
            {
                playerVisual.position = new Vector3(_playerMover.Position.x * gridCellSize, _playerMover.Position.y * gridCellSize, 0f);
            }
        }

        private void UpdateHud(string state)
        {
            if (hudLabel == null)
            {
                return;
            }

            var tileType = _map.GetTileType(_playerMover.Position);
            hudLabel.text = $"{state}  X:{_playerMover.Position.x} Y:{_playerMover.Position.y} Tile:{tileType}  CD:{movementCooldownSeconds:0.00}s";
        }
    }
}
