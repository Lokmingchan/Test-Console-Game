using System;
using TestGame.System.Base.Interfaces;

namespace TestGame.System.Objects
{
    public class Weapon : IWeapon
    {
        private string[] makes = new string[] { "Coltqette", "Firesmith", "Platinum", "Anubis", "Mephisto"};
        private string[] types = new string[] { "Pistol", "Knife", "Rifle", "Shotgun", "Taser", "Sword", "Katana"};

        private string make;
        private string type;
        private int price;
        private int strength;
        private bool exists;

        public string Make { get => make; protected set => make = value; }
        public string Type { get => type; protected set => type = value; }
        public int Price { get => price; protected set => price = value; }
        public int Strength { get => strength; protected set => strength = value; }
        public bool Exists { get => exists; set => exists = value; }

        public Weapon(int price, int strength)
        {
            if (price == 0 && strength == 0)
            {
                Exists = false;
                Price = 0;
                Strength = 0;
                Make = "None";
                Type = "None";
            }
            else
            {
                Exists = true;
                Random r = new Random();
                Price = price;
                Strength = strength;
                Make = RandomMake(r);
                Type = RandomType(r);
            }
        }

        protected string RandomMake(Random r)
        {
            return makes[r.Next(0, makes.Length)];
        }

        protected string RandomType(Random r)
        {
            return types[r.Next(0, makes.Length)];
        }
    }
}
