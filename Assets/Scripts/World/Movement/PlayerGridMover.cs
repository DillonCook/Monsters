using UnityEngine;

namespace Monsters.World.Movement
{
    public sealed class PlayerGridMover
    {
        private readonly IWorldGridMap _map;

        public PlayerGridMover(IWorldGridMap map, Vector2Int startPosition)
        {
            _map = map;
            Position = startPosition;
        }

        public Vector2Int Position { get; private set; }

        public bool TryMove(GridDirection direction)
        {
            var next = Position + ToDelta(direction);
            if (!_map.IsWalkable(next))
            {
                return false;
            }

            Position = next;
            return true;
        }

        private static Vector2Int ToDelta(GridDirection direction)
        {
            return direction switch
            {
                GridDirection.Up => Vector2Int.up,
                GridDirection.Down => Vector2Int.down,
                GridDirection.Left => Vector2Int.left,
                GridDirection.Right => Vector2Int.right,
                _ => Vector2Int.zero
            };
        }
    }
}
