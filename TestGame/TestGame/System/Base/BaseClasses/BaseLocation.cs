using System;
using TestGame.System.World;

namespace TestGame.System.Base.BaseClasses
{
    public class BaseLocation : IEquatable<BaseLocation>
    {
        private int x;
        private int y;

        public int X { get => x; protected set => x = value; }
        public int Y { get => y; protected set => y = value; }

        public bool Equals(BaseLocation Other)
        {
            if (X == Other.X && Y == Other.Y)
                return true;

            return false;
        }
    }
}
