using Monsters.World.Movement;
using UnityEngine;

namespace Monsters.World.Runtime
{
    public sealed class SwipeDirectionInput
    {
        private readonly float _minimumSwipeDistancePixels;

        public SwipeDirectionInput(float minimumSwipeDistancePixels)
        {
            _minimumSwipeDistancePixels = Mathf.Max(1f, minimumSwipeDistancePixels);
        }

        public bool TryGetDirection(Vector2 startPosition, Vector2 endPosition, out GridDirection direction)
        {
            var delta = endPosition - startPosition;
            if (delta.magnitude < _minimumSwipeDistancePixels)
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

        public static float CalculateThresholdPixels(float inches, float fallbackDpi = 160f)
        {
            var safeDpi = Screen.dpi > 0f ? Screen.dpi : fallbackDpi;
            return Mathf.Max(1f, inches * safeDpi);
        }
    }
}
