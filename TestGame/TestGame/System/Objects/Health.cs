namespace TestGame.System.Objects
{
    public class Health
    {
        private bool exists;
        private int healAmount;

        public bool Exists { get => exists; set => exists = value; }
        public int HealAmount { get => healAmount; protected set => healAmount = value; }

        public Health(int healAmount)
        {
            Exists = healAmount != 0;
            HealAmount = healAmount;
        }
    }
}
