using SimpleSpaceShooter.Core;
using UnityEngine;

namespace SimpleSpaceShooter.Movement {
    public struct SpaceShipBounds {
        
        public SpaceShipBounds(PolygonCollider2D collider, Arena arena, float scaleX, float scaleY) {
            MinX = float.MaxValue;
            MaxX = float.MinValue;
            MinY = float.MaxValue;
            MaxY = float.MinValue;

            foreach (Vector2 point in collider.points) {
                if (MinX > point.x) {
                    MinX = point.x;
                } else if (MaxX < point.x) {
                    MaxX = point.x;
                }

                if (MinY > point.y) {
                    MinY = point.y;
                } else if (MaxY < point.y) {
                    MaxY = point.y;
                }
            }

            MinX *= scaleX;
            MaxX *= scaleX;
            MinY *= scaleY;
            MaxY *= scaleY;

            Width = Mathf.Abs(MaxX - MinX);
            Height = Mathf.Abs(MaxY - MinY);

            float halfArenaWidth = arena.ArenaWidth / 2.0f;
            float halfArenaHeight = arena.ArenaHeight / 2.0f;
            MaxXWorld = halfArenaWidth - Mathf.Abs(MaxX) - arena.ArenaMargin;
            MinXWorld = Mathf.Abs(MinX) - halfArenaWidth + arena.ArenaMargin;
            MaxYWorld = halfArenaHeight - Mathf.Abs(MaxY) - arena.ArenaMargin;
            MinYWorld = Mathf.Abs(MinY) - halfArenaHeight + arena.ArenaMargin;
        }

        public float MinX { get; private set; }
        public float MaxX { get; private set; }
        public float MinY { get; private set; }
        public float MaxY { get; private set; }

        public float MinXWorld { get; private set; }
        public float MaxXWorld { get; private set; }
        public float MinYWorld { get; private set; }
        public float MaxYWorld { get; private set; }

        public float Width { get; private set; }
        public float Height { get; private set; }

    }
}