using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame
{
    class Forest
    {
        // Variable declaration
        private Random randomNumberGen = new Random();

        // Fields declaration
        private IQueryable<Monster> MonsterStock;
        public IQueryable<Monster> monsterStock
        {
            get { return MonsterStock; }
            set { MonsterStock = value; }
        }

        // Constructors
        public Forest()
        {

        }

        public Forest(IQueryable<Monster> monsterStock)
        {
            this.monsterStock = monsterStock;
        }

        // Method definition
        public Monster chooseMonster(int level)
        {
            if (randomNumberGen.Next(100) > 5)
            {
                // Normal fight with monster at appropriate level
                IQueryable<Monster> possibleMonsters = from monster in this.monsterStock
                                                       where monster.level <= level
                                                       select monster;
                return possibleMonsters.ToList<Monster>()[randomNumberGen.Next(possibleMonsters.ToList<Monster>().Count)].Clone();
            }
            else
            {
                // 5% chance of fighting monster above player level
                return this.monsterStock.ToList<Monster>()[randomNumberGen.Next(monsterStock.ToList<Monster>().Count)].Clone();
            }
        }

        public bool fight(Player gamePlayer, Monster fightMonster)
        {
            string choice;

            while (gamePlayer.health > 0 && fightMonster.health > 0)
            {
                Console.WriteLine("You have " + gamePlayer.health + " health and the monster has " + fightMonster.health + " health.");
                Console.WriteLine("What do you want to do? (attack, block, potion, or run)");
                choice = Console.ReadLine();
                if (choice == "attack")
                {
                    // Player chose to attack monster
                    // Calc crit chance
                    if (randomNumberGen.Next(100) <= gamePlayer.critChance)
                    {
                        // Player critted; do two times damage
                        Console.WriteLine("Critical hit on the monster!");
                        if (gamePlayer.armorPoints > 0)
                        {
                            // Player has at least one armor points
                            // Calculates damage with armor based off this equation:
                            // damage = (opponent damage - (opponent damage / (100 / player armor)))
                            gamePlayer.health -= fightMonster.damage - (fightMonster.damage / (100 / gamePlayer.armorPoints));
                        }
                        else
                        {
                            // Player has no armor points
                            // Calculates damage without armor
                            gamePlayer.health -= fightMonster.damage;
                        }
                        fightMonster.health -= (gamePlayer.damage * 2);
                    }
                    else
                    {
                        // Player did not crit; do standard damage
                        if (gamePlayer.armorPoints > 0)
                        {
                            // Player has at least one armor points
                            // Calculates damage with armor based off this equation:
                            // damage = (opponent damage - (opponent damage / (100 / player armor)))
                            gamePlayer.health -= fightMonster.damage - (fightMonster.damage / (100 / gamePlayer.armorPoints));
                        }
                        else
                        {
                            // Player has no armor points
                            // Calculates damage without armor
                            gamePlayer.health -= fightMonster.damage;
                        }
                        fightMonster.health -= gamePlayer.damage;
                    }
                }
                else if (choice == "block")
                {
                    // Player chose to block
                    // Calc block chance
                    if (randomNumberGen.Next(100) > gamePlayer.blockChance)
                    {
                        // Player successfully blocked; negate enemy damage
                        Console.WriteLine("You blocked successfully and took no damage, as well as dealing some to the monster!");
                        fightMonster.health -= (gamePlayer.damage / 2);
                    }
                    else
                    {
                        // Player did not block successfully; take damage and deal none
                        Console.WriteLine("You did not block successfully!");
                        gamePlayer.health -= fightMonster.damage;
                    }
                }
                else if (choice == "potion")
                {
                    // Player chose to use potion
                    // List potions in inventory

                    var potionList = from potion in gamePlayer.inventory.AsQueryable<Item>()
                                     where potion is Potion
                                     select (Potion)potion;

                    int i = 0;
                    foreach (Potion cPotion in potionList)
                    {
                        Console.WriteLine((i + 1) + ". {0} heals {1} points. You have {2}.", cPotion.name, cPotion.healPoints,
                            (from potion in potionList where (potion.name == cPotion.name) select potion).ToList().Count);
                        i++;
                    }

                    // Select potion, heal player, remove potion from inventory
                    Console.WriteLine("Write the index of the potion you want to use: ");
                    int chosenIndex = 1;
                    try
                    {
                        chosenIndex = (Int32.Parse(Console.ReadLine()) - 1);
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("I don't understand that command.");
                        continue;
                    }
                    gamePlayer.addHealth(potionList.ToList<Potion>()[chosenIndex].healPoints);
                    gamePlayer.inventory.Remove(gamePlayer.inventory.First(x => x.name == potionList.ToList<Potion>()[chosenIndex].name));
                }
                else if (choice == "run")
                {
                    // Player chose to run
                    // Calc evade chance
                    if (randomNumberGen.Next(100) <= fightMonster.evadeChance)
                    {
                        // Player successfully evaded
                        // Return to village
                        fightMonster.health = 0;
                        fightMonster.isEvaded = true;
                    }
                    else
                    {
                        // Player did not evade successfully. Take small damage and continue fight
                        int evadeDamage = (fightMonster.damage / 3);
                        Console.WriteLine("You were unable to escape the monster! You took " + evadeDamage + " damage.");
                        gamePlayer.health -= evadeDamage;
                    }
                }
            }

            if (gamePlayer.health <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int lootCalc(int level)
        {
            return randomNumberGen.Next(level, level * 5);
        }
    }
}
