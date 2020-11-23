using TestGame.System.Base.BaseClasses;
using TestGame.System.Objects;

namespace TestGame.System.AI.BuySell
{
    public class BuySellAroundAveragePlus75 : BaseShopStrategy
    {
        public override ShopCommand Action(long money, Stash compStash, Stash locationStash)
        {
            RecordPrices(locationStash.Price);
            return BuySellAroundAverage(money, compStash, locationStash, .75);
        }
    }
}
