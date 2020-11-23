using TestGame.System.Base.BaseClasses;
using TestGame.System.Base.Interfaces;
using TestGame.System.World;

namespace TestGame.System.AI.Move
{
    public class MovementCoward : BaseMoveStrategy
    {
        public override string Move(ICharacter comp, ICharacter player, WorldMap map)
        {
            return MoveAway(comp, player, map);
        }
    }
}
