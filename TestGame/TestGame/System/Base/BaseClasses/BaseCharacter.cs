using TestGame.System.AI;
using TestGame.System.Base.Interfaces;
using TestGame.System.Classes;
using TestGame.System.Objects;

namespace TestGame.System.Base.BaseClasses
{
    public class BaseCharacter : ICharacter
    {
        private string name;
        private char symbol;
        private int health;
        private int baseStrength;
        private long money;
        private PlayerLocation location;
        private Weapon weapon;
        private Stash stash;
        AIProfile ai;

        public string Name { get => name; protected set => name = value; }
        public char Symbol { get => symbol; protected set => symbol = value; }
        public int Life { get => health; protected set => health = value; }
        public int BaseStrength { get => baseStrength; protected set => baseStrength = value; }
        public PlayerLocation Location { get => location; protected set => location = value; }
        public Weapon Weapon { get => weapon; protected set => weapon = value; }
        public Stash Stash { get => stash; protected set => stash = value; }
        public long Money { get => money; protected set => money = value; }
        public AIProfile AI { get => ai; protected set => ai = value; }

        public bool TakeDamage(int damage)
        {
            if (Life - damage <= 0)
                return false;

            Life -= damage;
            return true;
        }

        public int GetTotalStrength()
        {
            return BaseStrength + Weapon.Strength;
        }

        public bool DropWeapon()
        {
            if (Weapon.Strength == 0)
                return false;

            Weapon = new Weapon(0, 0);
            return true;
        }

        public bool PickUpHealth(Health h)
        {
            Life += h.HealAmount;
            if (Life > 100) Life = 100;
            return true;
        }

        public bool BuyWeapon(Weapon w)
        {
            if (w.Price > Money)
                return false;

            if (Weapon.Strength > w.Strength)
                return false;

            Weapon = w;
            Money -= w.Price;
            return true;
        }

        public bool BuyStash(Stash locationStash)
        {
            if (locationStash.Amount * locationStash.Price > Money)
                return false;

            Money -= locationStash.Amount * locationStash.Price;
            Stash.Price = locationStash.Price;
            Stash.Amount += locationStash.Amount;
            return true;
        }

        public bool SellStash(Stash locationStash)
        {
            Money += Stash.Amount * locationStash.Price;
            Stash.Price = 0;
            Stash.Amount = 0;
            return true;
        }
    }
}
