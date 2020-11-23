using System;
using System.Collections.Generic;
using TestGame.System.Base.BaseClasses;
using TestGame.System.Base.Interfaces;
using TestGame.System.Objects;

namespace TestGame.System.Classes
{
    public class MapLocation : BaseLocation
    {
        List<ICharacter> players;
        Stash stash;
        Weapon weaponPurchase;
        Health healthPickUp;

        public List<ICharacter> Players { get => players; protected set => players = value; }
        public Stash Stash { get => stash; set => stash = value; }
        public Weapon WeaponPurchase { get => weaponPurchase; set => weaponPurchase = value; }
        public Health HealthPickUp { get => healthPickUp; set => healthPickUp = value; }

        public MapLocation(BaseLocation location, IEnumerable<ICharacter> allPlayers, Stash stash, Weapon weaponPurchase, Health healthPickUp)
        {
            X = location.X;
            Y = location.Y;
            Stash = stash;
            WeaponPurchase = weaponPurchase;
            HealthPickUp = healthPickUp;

            Players = CheckIfPlayersHere(allPlayers);
            
        }

        private List<ICharacter> CheckIfPlayersHere(IEnumerable<ICharacter> allPlayers)
        {
            List<ICharacter> playersHere = new List<ICharacter>();
            foreach (ICharacter p in allPlayers)
            {
                if (p.Location.Equals(this))
                {
                    playersHere.Add(p);
                }
            }
            return playersHere;
        }

        public bool IsTherePlayerClash()
        {
            return Players.Count > 1;
        }
    }
}
