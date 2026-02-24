using Monsters.World.Movement;
using UnityEngine;

namespace Monsters.World.Runtime
{
    public sealed class SwipeDirectionInput
    {
        private readonly float _minimumSwipeDistance;

        public SwipeDirectionInput(float minimumSwipeDistance)
        {
            _minimumSwipeDistance = minimumSwipeDistance;
        }

        public bool TryGetDirection(Vector2 startPosition, Vector2 endPosition, out GridDirection direction)
        {
            var delta = endPosition - startPosition;
            if (delta.magnitude < _minimumSwipeDistance)
            {
                direction = GridDirection.Up;
                return false;
            }

            if (Mathf.Abs(delta.x) >= Mathf.Abs(delta.y))
            {
                direction = delta.x >= 0f ? GridDirection.Right : GridDirection.Left;
                return true;
            }

            direction = delta.y >= 0f ? GridDirection.Up : GridDirection.Down;
            return true;
        }
    }
}
