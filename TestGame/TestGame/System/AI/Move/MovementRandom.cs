using System;
using TestGame.System.Base.BaseClasses;
using TestGame.System.Base.Interfaces;
using TestGame.System.World;

namespace TestGame.System.AI.Move
{
    public class MovementRandom : BaseMoveStrategy
    {
        public override string Move(ICharacter comp, ICharacter player, WorldMap map)
        {
            return base.Move(comp, player, map);
        }
    }
}
