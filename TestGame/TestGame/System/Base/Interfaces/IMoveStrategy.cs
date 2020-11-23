using TestGame.System.World;

namespace TestGame.System.Base.Interfaces
{
    public interface IMoveStrategy
    {
        string Move(ICharacter comp, ICharacter player, WorldMap map);
    }
}
