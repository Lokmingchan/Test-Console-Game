using TestGame.System.AI;
using TestGame.System.Base.BaseClasses;
using TestGame.System.Classes;
using TestGame.System.Objects;

namespace TestGame.System.Characters
{
    public class CompCharacter : BaseCharacter
    {
        public CompCharacter(string name, char mapSymbol, int str, long money, PlayerLocation location, Weapon weapon, Stash stash, AIProfile ai)
        {
            Name = name;
            Symbol = mapSymbol;
            Location = location;
            BaseStrength = str;
            Weapon = weapon;
            Stash = stash;
            Money = money;
            AI = ai;
        }
    }
}
