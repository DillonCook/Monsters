using System.Collections.Generic;
using UnityEngine;

namespace Monsters.World.Movement
{
    public sealed class GridWorldMap : IWorldGridMap
    {
        private readonly WorldTileType[,] _tiles;
        private readonly Dictionary<Vector2Int, InteractionType> _interactionLookup;

        public GridWorldMap(WorldTileType[,] tiles, IReadOnlyCollection<WorldInteractionPoint> interactionPoints)
        {
            _tiles = tiles;
            _interactionLookup = new Dictionary<Vector2Int, InteractionType>();

            foreach (var point in interactionPoints)
            {
                if (IsInBounds(point.TilePosition) && point.InteractionType != InteractionType.None)
                {
                    _interactionLookup[point.TilePosition] = point.InteractionType;
                }
            }
        }

        public bool IsInBounds(Vector2Int position)
        {
            return position.x >= 0 && position.y >= 0
                   && position.x < _tiles.GetLength(0)
                   && position.y < _tiles.GetLength(1);
        }

        public bool IsWalkable(Vector2Int position)
        {
            return IsInBounds(position) && _tiles[position.x, position.y] != WorldTileType.Blocked;
        }

        public WorldTileType GetTileType(Vector2Int position)
        {
            return IsInBounds(position) ? _tiles[position.x, position.y] : WorldTileType.Blocked;
        }

        public InteractionType GetInteractionType(Vector2Int position)
        {
            return _interactionLookup.TryGetValue(position, out var interaction)
                ? interaction
                : InteractionType.None;
        }
    }
}
