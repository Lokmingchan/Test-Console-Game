using System;
using System.Collections.Generic;
using System.Net;
using TestGame.System.AI;
using TestGame.System.Base.BaseClasses;
using TestGame.System.Base.Interfaces;
using TestGame.System.Characters;
using TestGame.System.Classes;
using TestGame.System.Objects;

namespace TestGame.System.World
{
    public class Game
    {
        const string GameTitle = "TestGame";
        const string GameVersion = "v1.0";
        private int _turnCount;
        private WorldMap _map;
        private List<ICharacter> _players;
        Random _r;

        public void Start()
        {
            Intro();
            Setup();
            Play();
        }

        public void Intro()
        {
            Console.WriteLine("Welcome to " + GameTitle + " " + GameVersion);
            Console.WriteLine("Press enter to start");
            Console.ReadLine();
        }

        public void Setup()
        {
            const int x = 2;
            _r = new Random();
            Console.Clear();
            //Console.Write("Number of Players (1 - 4): ");
            //do
            //{
            //    input = Console.ReadLine();
            //} while (!int.TryParse(input, out x) && x < 1 && x > 4);
            //Console.Write(" OK!");
            //Console.WriteLine();
            SetupPlayers(x);
            SetupMap(4, 4);
            _turnCount = 0;
            Console.WriteLine("Setup Complete!");
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }

        public void SetupPlayers(int x)
        {
            _players = new List<ICharacter>();
            Console.Write("Player Name: ");
            var name = Console.ReadLine();
            Console.Write(" OK!");
            Console.WriteLine();
            _players.Add(new PlayerCharacter(name, 'X', _r.Next(1, 6), 100, new PlayerLocation(0, 0), new Weapon(0, 0), new Stash(0, 0)));
            x -= 1;
            while(x != 0)
            {
                var cname = "Comp" + (x + 1);
                _players.Add(new CompCharacter(cname, 'C', _r.Next(1, 6), 100, new PlayerLocation(_r.Next(0, 4), _r.Next(0, 4)), new Weapon(0, 0), new Stash(0, 0), new AIProfile()));
                x -= 1;
            }
        }

        public void SetupMap(int width, int height)
        {
            _map = new WorldMap(width, height);
        }

        public void Play()
        {
            var endGame = false;

            while (!endGame && _turnCount <= 500)
            {
                try
                {
                    DisplayMap();
                    MapLocation location = SetupLocation(_players[0]);
                    //DetectCollisions();
                    DisplayStatus(location);
                    ResolveActions(location);
                    ResolveComputerActions();
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public MapLocation SetupLocation(ICharacter player)
        {
            
            Stash stash = GenerateStash();
            Weapon weapon = GenerateWeapon();
            Health health = GenerateHealth();

            MapLocation location = new MapLocation(player.Location, _players, stash, weapon, health);

            return location;
        }

        public void ResolveActions(MapLocation location)
        {
            var move = false;
            Console.WriteLine("Action (U)p, (D)own, (L)eft, (R)ight, (B)uy Stash, (S)ell Stash, (W)eapon");
            do
            {
                DisplayPrompt();
                var input = Console.ReadLine();
                var switchInput = input?.ToUpper().Substring(0, 1);

                switch (switchInput)
                {
                    case "U":
                    case "D":
                    case "L":
                    case "R":
                        move = _players[0].Location.Move(input, _map);
                        if (!move) Console.WriteLine("Can't move that way");
                        break;
                    case "B":
                        if (_players[0].Money >= location.Stash.Amount * location.Stash.Price)
                        {
                            if (_players[0].BuyStash(location.Stash))
                                location.Stash.Amount = 0;
                            else
                                Console.WriteLine("Can't Buy Stash");
                        }
                        else
                        {
                            Stash stashToBuy = location.Stash.CalculatePurchasableStash(_players[0].Money);
                            if (_players[0].BuyStash(stashToBuy))
                            {
                                location.Stash.Amount -= stashToBuy.Amount;
                                Console.WriteLine("Stash Available: " + location.Stash.Amount + " for $" + location.Stash.Price);
                            }
                            else
                                Console.WriteLine("Can't Buy Stash");
                        }
                        break;
                    case "S":
                        if (_players[0].Stash.Amount <= 0)
                        {
                            Console.WriteLine("Can't Sell Stash");
                        }
                        else
                        {
                            var amount = _players[0].Stash.Amount;
                            if (_players[0].SellStash(location.Stash))
                            {
                                location.Stash.Amount += amount;
                                Console.WriteLine("Stash Available: " + location.Stash.Amount + " for $" + location.Stash.Price);
                            }
                            else
                                Console.WriteLine("Can't Sell Stash");
                        }
                        break;
                    case "W":
                        if (location.WeaponPurchase.Exists && _players[0].BuyWeapon(location.WeaponPurchase))
                            Console.WriteLine("Weapon Purchased!");
                        else
                            Console.WriteLine("Can't Buy Weapon");
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        continue;

                }
            } while (!move);
            _turnCount++;
        }

        public void ResolveComputerActions()
        {
            for (var p = 1; p < _players.Count; p++)
            {                
                CompCharacter c = (CompCharacter)_players[p];
                c.Location.Move(c.AI.MoveStrategy.Move(c, _players[0], _map), _map);

                MapLocation mapLocation = SetupLocation(_players[p]);

                if (mapLocation.WeaponPurchase.Exists && 
                    mapLocation.WeaponPurchase.Strength > c.Weapon.Strength &&
                    mapLocation.WeaponPurchase.Price < c.Money * .75)
                    c.BuyWeapon(mapLocation.WeaponPurchase);

                ShopCommand shopCmd = c.AI.StashStrategy.Action(c.Money, c.Stash, mapLocation.Stash);
                if (shopCmd == ShopCommand.Buy)
                    c.BuyStash(mapLocation.Stash);
                else if (shopCmd == ShopCommand.Sell)
                    c.SellStash(mapLocation.Stash);

                _players[p] = c;
            }
        }

        public void DisplayMap()
        {
            Console.Clear();
            _map.DrawMap(_players);
        }

        public void DisplayStatus(MapLocation location)
        {
            Console.WriteLine("Turn Count: " + _turnCount);
            Console.WriteLine("Stash Available: " + location.Stash.Amount + " for $" + location.Stash.Price);

            if (location.HealthPickUp.Exists)
            {
                Console.WriteLine("Health Pickup! +" + location.HealthPickUp.HealAmount);
                _players[0].PickUpHealth(location.HealthPickUp);
                location.HealthPickUp.Exists = false;
            }
            if (location.WeaponPurchase.Exists)
            {
                Console.WriteLine("Weapon Available for Purchase: " + location.WeaponPurchase.Make + " " + location.WeaponPurchase.Type + "(" + location.WeaponPurchase.Strength + ")" + " for $" + location.WeaponPurchase.Price);
            }
        }

        public void DisplayPrompt()
        {
            string labelHealth = "LIFE(" + _players[0].Life + ")";
            string labelStash = "STASH(" + _players[0].Stash + ")";
            string labelStrength = "STR(" + _players[0].BaseStrength + ")";
            string labelWeapon = "WPN(" + _players[0].Weapon.Strength + ")";
            string labelMoney = "$(" + _players[0].Money + ")";
            Console.Write(labelHealth  + "|" + labelStrength + "|" + labelWeapon + "|" + labelStash + "|" + labelMoney + ": ");
        }

        public Stash GenerateStash()
        {
            int minStashPrice = 5 + ((_turnCount / 10) * 4);
            int maxStashPrice = 15 + ((_turnCount / 10) * 9) + 1;
            var stashPrice = _r.Next(minStashPrice, maxStashPrice);
            var stashAmount = _r.Next(1, 100);
            return new Stash(stashAmount, stashPrice);
        }

        public Weapon GenerateWeapon()
        {
            int minWeaponStrength = 0, maxWeaponStrength = 0, minWeaponPrice = 0, maxWeaponPrice = 0;
            bool weaponPurchase = _r.Next(1, 7) == 1;

            if (weaponPurchase)
            {
                minWeaponStrength = 2 + ((_turnCount / 10) * 2);
                maxWeaponStrength = 6 + ((_turnCount / 10) * 3);

                minWeaponPrice = 12 + ((_turnCount / 10) * 7);
                maxWeaponPrice = 21 + ((_turnCount / 10) * 18);
            }

            int weaponStrength = weaponPurchase ? _r.Next(minWeaponStrength, maxWeaponStrength) : 0;
            int weaponPrice = weaponPurchase ? _r.Next(minWeaponPrice, maxWeaponPrice) * weaponStrength + 1 : 0;
            return new Weapon(weaponPrice, weaponStrength);
        }

        public Health GenerateHealth()
        {
            bool healthPickUp = _r.Next(1, 11) == 1;
            int healAmount = healthPickUp ? _r.Next(5, 26) : 0;

            return new Health(healAmount);
        }
    }
}
