using TestGame.System.Base.BaseClasses;
using TestGame.System.Objects;

namespace TestGame.System.Base.Interfaces
{
    public interface IShopStrategy
    {
        ShopCommand Action(long money, Stash compStash, Stash locationStash);
        ShopCommand CoinFlip();

    }
}
