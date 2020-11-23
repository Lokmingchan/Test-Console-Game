using TestGame.System.Base.BaseClasses;
using TestGame.System.Classes;
using TestGame.System.Objects;

namespace TestGame.System.Characters
{
    public class PlayerCharacter : BaseCharacter
    {
        const char PlayerSymbol = 'X';

        public PlayerCharacter(string name, char mapSymbol, int str, long money, PlayerLocation location, Weapon weapon, Stash stash)
        {
            Name = name;
            Symbol = PlayerSymbol;
            Location = location;
            BaseStrength = str;
            Weapon = weapon;
            Stash = stash;
            Money = money;
            Life = 100;
        }
    }
}
