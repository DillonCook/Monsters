using UnityEngine;

namespace Monsters.World.Runtime
{
    public sealed class WorldRuntimeState
    {
        private static readonly WorldRuntimeState Shared = new();

        private bool _hadControlBeforeBattle;
        private Vector3 _cameraPositionBeforeBattle;

        private WorldRuntimeState()
        {
        }

        public static WorldRuntimeState Instance => Shared;

        public bool HasPlayerControl { get; private set; } = true;
        public Vector3 ActiveCameraPosition { get; private set; }

        public void SetCameraPosition(Vector3 position)
        {
            ActiveCameraPosition = position;
        }

        public void EnterBattle()
        {
            _hadControlBeforeBattle = HasPlayerControl;
            _cameraPositionBeforeBattle = ActiveCameraPosition;
            HasPlayerControl = false;
        }

        public void ReturnFromBattle()
        {
            HasPlayerControl = _hadControlBeforeBattle;
            ActiveCameraPosition = _cameraPositionBeforeBattle;
        }
    }
}
