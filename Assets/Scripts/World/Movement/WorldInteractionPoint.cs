using UnityEngine;

namespace Monsters.World.Movement
{
    public readonly struct WorldInteractionPoint
    {
        public WorldInteractionPoint(Vector2Int tilePosition, InteractionType interactionType)
        {
            TilePosition = tilePosition;
            InteractionType = interactionType;
        }

        public Vector2Int TilePosition { get; }
        public InteractionType InteractionType { get; }
    }
}
