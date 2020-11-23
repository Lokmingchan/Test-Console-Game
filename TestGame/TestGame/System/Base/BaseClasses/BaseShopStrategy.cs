using System;
using System.Collections.Generic;
using System.Linq;
using TestGame.System.Base.Interfaces;
using TestGame.System.Objects;

namespace TestGame.System.Base.BaseClasses
{
    public class ShopCommand
    {
        private ShopCommand(string value) { Value = value; }
        public string Value { get; set; }
        public static ShopCommand Buy => new ShopCommand("Buy");
        public static ShopCommand Sell => new ShopCommand("Sell");
        public static ShopCommand Hold => new ShopCommand("Hold");
    }

    public class BaseShopStrategy : IShopStrategy
    {
        private const int NumOfRecords = 20;
        private Random _r;
        private readonly Queue<int> _historicalPrices = new Queue<int>();

        public virtual ShopCommand Action(long money, Stash compStash, Stash locationStash)
        {
            RecordPrices(locationStash.Price);
            return CoinFlip();
        }

        public ShopCommand CoinFlip()
        {
            _r = new Random();
            int x = _r.Next(0, 3);
            switch (x)
            {
                case 0:
                    return ShopCommand.Buy;
                case 1:
                    return ShopCommand.Sell;
                case 2:
                    return ShopCommand.Hold;
            }

            return ShopCommand.Hold;
        }

        public ShopCommand SellHigh(long money, Stash compStash, Stash locationStash)
        {
            return locationStash.Price <= compStash.Price ? ShopCommand.Buy : ShopCommand.Sell;
        }

        public ShopCommand BuyLow(long money, Stash compStash, Stash locationStash)
        {
            var averagePrice = _historicalPrices.Average();
            if (locationStash.Price < averagePrice)
                return ShopCommand.Buy;
            return locationStash.Price > compStash.Price ? ShopCommand.Sell : ShopCommand.Hold;
        }

        public ShopCommand BuySellAroundAverage(long money, Stash compStash, Stash locationStash, double priceAdjust = 0.0)
        {
            var averagePrice = _historicalPrices.Average();
            return locationStash.Price < averagePrice * (1 - priceAdjust)
                ? ShopCommand.Buy
                : (locationStash.Price > averagePrice * (1 + priceAdjust) 
                ? ShopCommand.Sell 
                : ShopCommand.Hold);
        }

        protected void RecordPrices(int locationPrice)
        {
            _historicalPrices.Enqueue(locationPrice);
            if (_historicalPrices.Count > NumOfRecords)
                _historicalPrices.Dequeue();
        }
    }
}
