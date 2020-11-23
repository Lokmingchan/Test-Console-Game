using TestGame.System.AI;
using TestGame.System.Classes;
using TestGame.System.Objects;

namespace TestGame.System.Base.Interfaces
{
    public interface ICharacter
    {
        string Name { get; }
        char Symbol { get; }
        int Life { get; }
        int BaseStrength { get; }
        long Money { get; }
        PlayerLocation Location { get; }
        Weapon Weapon { get; }
        Stash Stash { get; }
        AIProfile AI { get; }

        int GetTotalStrength();
        bool TakeDamage(int damage);
        bool DropWeapon();
        bool PickUpHealth(Health h);
        bool BuyWeapon(Weapon w);
        bool BuyStash(Stash locationStash);
        bool SellStash(Stash locationStash);
    }
}
