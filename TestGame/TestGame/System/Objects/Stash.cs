using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame.System.Objects
{
    public class Stash
    {
        private int amount;
        private int price;

        public int Amount { get => amount; set => amount = value; }
        public int Price { get => price; set => price = value; }

        public Stash(int amount, int price)
        {
            Amount = amount;
            Price = price;
        }

        public Stash CalculatePurchasableStash(long money)
        {
            if (money >= Amount * Price)
            {
                return this;
            }
            else
            {
                var amount = (int)(money / Price);
                return new Stash(amount, Price);
            }
        }
    }
}
