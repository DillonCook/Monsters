using UnityEngine;

namespace Monsters.World.Movement
{
    public sealed class GridWorldMap : IWorldGridMap
    {
        private readonly WorldTileType[,] _tiles;

        public GridWorldMap(WorldTileType[,] tiles)
        {
            _tiles = tiles;
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
    }
}
