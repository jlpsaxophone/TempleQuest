using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempleQuest
{
    public static class Collisions
    {
        public static bool CollidesWith(this BoundingRectangle a, BoundingRectangle b)
        {
            return !(a.X > b.X + b.Width || a.X + a.Width < b.X || a.Y > b.Y + b.Height || a.Y + a.Height < b.Y);
        }

        public static void CheckWallCollision(Player player, Wall[] walls)
        {
            foreach(Wall w in walls)
            {
                if (CollidesWith(player.bounds, w.bounds))
                {
                    player.bounds.X = player.lastPosition.X;
                    player.bounds.Y = player.lastPosition.Y;
                }               
            }
        }
    }
}
