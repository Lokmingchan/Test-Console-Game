namespace TestGame.System.Base.Interfaces
{
    public interface IWeapon
    {
        bool Exists { get; }
        string Make { get; }
        string Type { get; }
        int Price { get; }
        int Strength { get; }
    }
}
