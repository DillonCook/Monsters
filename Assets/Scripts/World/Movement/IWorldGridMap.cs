using UnityEngine;

namespace Monsters.World.Movement
{
    public interface IWorldGridMap
    {
        bool IsInBounds(Vector2Int position);
        bool IsWalkable(Vector2Int position);
        WorldTileType GetTileType(Vector2Int position);
        InteractionType GetInteractionType(Vector2Int position);
    }
}
