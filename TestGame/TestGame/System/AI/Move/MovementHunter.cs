using System;
using TestGame.System.Base.BaseClasses;
using TestGame.System.Base.Interfaces;
using TestGame.System.World;

namespace TestGame.System.AI.Move
{
    public class MovementHunter : BaseMoveStrategy
    {
        public override string Move(ICharacter comp, ICharacter player, WorldMap map)
        {
            return MoveTowards(comp, player, map);  
        }
    }
}
