using System;
using System.Collections.Generic;
using TestGame.System.AI.BuySell;
using TestGame.System.AI.Move;
using TestGame.System.Base.Interfaces;

namespace TestGame.System.AI
{
    public class AIProfile
    {
        List<Type> moveTypes = new List<Type> {
            typeof(MovementRandom),
            typeof(MovementHunter),
            typeof(MovementCoward)
        };

        List<Type> buySellTypes = new List<Type>
        {
            typeof(BuySellRandom),
            typeof(BuySellSellHigh),
            typeof(BuySellBuyLow),
            typeof(BuySellAroundAverage),
            typeof(BuySellAroundAveragePlus10),
            typeof(BuySellAroundAveragePlus25),
            typeof(BuySellAroundAveragePlus50),
            typeof(BuySellAroundAveragePlus75)
        };

        // Move Strategy
        private IMoveStrategy moveStrategy;

        // BuySell Strategy
        private IShopStrategy stashStrategy;
        private IShopStrategy weaponStrategy;


        public IMoveStrategy MoveStrategy { get => moveStrategy; protected set => moveStrategy = value; }
        public IShopStrategy StashStrategy { get => stashStrategy; set => stashStrategy = value; }
        public IShopStrategy WeaponStrategy { get => weaponStrategy; set => weaponStrategy = value; }

        public AIProfile()
        {
            Random r = new Random();
            MoveStrategy = (IMoveStrategy)Activator.CreateInstance(moveTypes[r.Next(0, moveTypes.Count)]);
            StashStrategy = (IShopStrategy)Activator.CreateInstance(buySellTypes[r.Next(0, buySellTypes.Count)]);
            WeaponStrategy = (IShopStrategy)Activator.CreateInstance(buySellTypes[r.Next(0, buySellTypes.Count)]);
        }

        public AIProfile(IMoveStrategy moveStrategy, IShopStrategy stashStrategy, IShopStrategy weaponStrategy)
        {
            MoveStrategy = moveStrategy;
            StashStrategy = stashStrategy;
            WeaponStrategy = weaponStrategy;
        }
    }
}
