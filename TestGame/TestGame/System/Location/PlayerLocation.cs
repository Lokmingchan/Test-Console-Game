using TestGame.System.Base.BaseClasses;
using TestGame.System.World;

namespace TestGame.System.Classes
{
    public class PlayerLocation : BaseLocation
    {
        private int previousX;
        private int previousY;

        public int PreviousX { get => previousX; protected set => previousX = value; }
        public int PreviousY { get => previousY; protected set => previousY = value; }

        public PlayerLocation(int x, int y)
        {
            X = x;
            Y = y;
            PreviousX = x;
            PreviousY = y;
        }

        public bool Move(string i, WorldMap m)
        {

            switch(i){
                case "U":
                    if (Y - 1 < 0) return false;
                    SetPrevious();
                    Y -= 1;
                    return true;
                case "D":
                    if (Y + 1 >= m.Height) return false;
                    SetPrevious();
                    Y += 1;
                    return true;
                case "L":
                    if (X - 1 < 0) return false;
                    SetPrevious();
                    X -= 1;
                    return true;
                case "R":
                    if (X + 1 >= m.Width) return false;
                    SetPrevious();
                    X += 1;
                    return true;
            }

            return false;
        }

        public bool CanMove(string i, WorldMap m)
        {

            switch (i)
            {
                case "U":
                    if (Y - 1 < 0) return false;
                    return true;
                case "D":
                    if (Y + 1 >= m.Height) return false;
                    return true;
                case "L":
                    if (X - 1 < 0) return false;
                    return true;
                case "R":
                    if (X + 1 >= m.Width) return false;
                    return true;
            }

            return false;
        }

        private void SetPrevious()
        {
            PreviousY = Y;
            PreviousX = X;
        }

        public PlayerLocation PreviousLocation()
        {
            return new PlayerLocation(PreviousX, PreviousY);
        } 
    }
}
