using System;
using System.Collections.Generic;
using TestGame.System.Base.Interfaces;
using TestGame.System.Classes;

namespace TestGame.System.World
{
    public class WorldMap
    {
        private int width;
        private int height;

        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }

        public WorldMap(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void DrawMap(IEnumerable<ICharacter> players)
        {
            for(var y = 0; y < Height; y++)
            {
                Console.Write("|");
                for(var x = 0; x < Width; x++)
                {
                    PlayerLocation loc = new PlayerLocation(x, y);
                    foreach (ICharacter p in players)
                    {
                        if (p.Location.Equals(loc))
                            Console.Write(p.Symbol);
                        else
                            Console.Write(" ");
                    }
                    Console.Write("|");
                }
                Console.WriteLine();
            }
        }
    }
}
