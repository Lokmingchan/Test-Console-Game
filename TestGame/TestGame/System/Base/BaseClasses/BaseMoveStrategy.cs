using System;
using TestGame.System.Base.Interfaces;
using TestGame.System.World;

namespace TestGame.System.Base.BaseClasses
{
    public class BaseMoveStrategy : IMoveStrategy
    {
        private Random r;
        private string[] directions = new string[] { "U", "D", "L", "R" };

        public virtual string Move(ICharacter comp, ICharacter player, WorldMap map)
        {
            return MoveRandom(comp, map);
        }

        public string MoveRandom(ICharacter comp, WorldMap map)
        {
            int x;
            r = new Random();
            do {
                x = r.Next(0, 4);
            } while (!comp.Location.CanMove(directions[x], map));
            return directions[x];
        }

        public string MoveTowards(ICharacter comp, ICharacter player, WorldMap map)
        {
            if (comp.Location.Equals(player.Location.PreviousLocation()))
                return MoveRandom(comp, map);

            int xDiff = Math.Abs(comp.Location.X - player.Location.PreviousX);
            int yDiff = Math.Abs(comp.Location.Y - player.Location.PreviousY);

            if (xDiff >= yDiff)
            {
                if (comp.Location.X - player.Location.PreviousX >= 0)
                    return "L";
                else
                    return "R";
            }
            else
            {
                if (comp.Location.Y - player.Location.PreviousY >= 0)
                    return "U";
                else
                    return "D";
            }
        }

        public string MoveAway(ICharacter comp, ICharacter player, WorldMap map)
        {
            if (comp.Location.X == map.Width - 1 && comp.Location.Y == map.Height - 1)
                return MoveRandom(comp, map);

            int xDiff = Math.Abs(comp.Location.X - player.Location.PreviousX);
            int yDiff = Math.Abs(comp.Location.Y - player.Location.PreviousY);

            if (xDiff < yDiff)
            {
                if (player.Location.PreviousX <= comp.Location.X && comp.Location.X >= 0 && comp.Location.CanMove("R", map))
                    return "R";
                else if (player.Location.PreviousX >= comp.Location.X && comp.Location.X < map.Width && comp.Location.CanMove("L", map))
                    return "L";
                else if (player.Location.PreviousY <= comp.Location.Y && comp.Location.Y >= 0 && comp.Location.CanMove("D", map))
                    return "D";
                else if (player.Location.PreviousY >= comp.Location.Y && comp.Location.Y < map.Height && comp.Location.CanMove("U", map))
                    return "U";
            }
            else
            {
                if (player.Location.PreviousY >= comp.Location.Y && comp.Location.Y < map.Height && comp.Location.CanMove("U", map))
                    return "U";
                else if (player.Location.PreviousY <= comp.Location.Y && comp.Location.Y >= 0 && comp.Location.CanMove("D", map))
                    return "D";
                else if (player.Location.PreviousX >= comp.Location.X && comp.Location.X < map.Width && comp.Location.CanMove("L", map))
                    return "L";
                else if (player.Location.PreviousX <= comp.Location.X && comp.Location.X >= 0 && comp.Location.CanMove("R", map))
                    return "R";
            }

            return MoveRandom(comp, map);
        }
    }
}
