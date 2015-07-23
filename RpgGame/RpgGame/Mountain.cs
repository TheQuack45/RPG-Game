using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame
{
    class Mountain
    {
        // Variable declaration
        Random randomNumberGen = new Random();

        // Fields declaration
        private IQueryable<Boss> BossStock;
        public IQueryable<Boss> bossStock
        {
            get { return BossStock; }
            set { BossStock = value; }
        }

        // Constructors
        public Mountain()
        {

        }
        
        public Mountain(IQueryable<Boss> bossStock)
        {
            this.bossStock = bossStock;
        }

        // Methods definition
        public Boss chooseBoss(int level)
        {
            // Select appropriate boss for level, or select lowest level boss
            // if no appropriate boss is available
            IQueryable<Boss> possibleBosses = null;
            possibleBosses = (from boss in this.bossStock
                              where boss.level <= level
                              select boss);
            if (!possibleBosses.ToList<Boss>().Any<Boss>())
            {
                // No bosses are appropriate for player's level; select lowest level boss available
                IQueryable<Boss> orderedBosses = from boss in this.bossStock
                                                 orderby boss.level ascending
                                                 select boss;
                return orderedBosses.ToList<Boss>()[0].Clone();
            }
            else
            {
                // Appropriate bosses available; return random boss
                return possibleBosses.ToList<Boss>()[randomNumberGen.Next(possibleBosses.ToList<Boss>().Count)].Clone();
            }
        }
        
        public bool fight(Player gamePlayer, Boss fightBoss)
        {
            string choice;
            bool isSuperCrit = false;

            Console.WriteLine("You are fighting " + fightBoss.name + "!");

            while (gamePlayer.health > 0 && fightBoss.health > 0)
            {
                // Check if supercrit occurred
                if (fightBoss.superCritAmt == 100)
                {
                    if (randomNumberGen.Next(100) <= fightBoss.superCritChance)
                    {
                        // Super crit amount is 100 and chance passed
                        isSuperCrit = true;
                        fightBoss.resetSuperCrit();
                    }
                }
                
                // Fight content
                Console.WriteLine("You have " + gamePlayer.health + " health and the boss has " + fightBoss.health + " health.");
                Console.WriteLine("What do you want to do? (attack, block, potion, or run)");
                choice = Console.ReadLine();
                if (choice == "attack")
                {
                    // Check if supercrit occurred
                    if (isSuperCrit)
                    {
                        // Supercrit occurred; do 10 times damage
                        Console.WriteLine("The boss has hit you with a super crit!");
                        gamePlayer.health -= (fightBoss.damage * 10);
                    }

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
                            gamePlayer.health -= fightBoss.damage - (fightBoss.damage / (100 / gamePlayer.armorPoints));
                        }
                        else
                        {
                            // Player has no armor points
                            // Calculates damage without armor
                            gamePlayer.health -= fightBoss.damage;
                        }
                        fightBoss.health -= (gamePlayer.damage * 2);
                    }
                    else
                    {
                        // Player did not crit; do standard damage
                        if (gamePlayer.armorPoints > 0)
                        {
                            // Player has at least one armor points
                            // Calculates damage with armor based off this equation:
                            // damage = (opponent damage - (opponent damage / (100 / player armor)))
                            gamePlayer.health -= fightBoss.damage - (fightBoss.damage / (100 / gamePlayer.armorPoints));
                        }
                        else
                        {
                            // Player has no armor points
                            // Calculates damage without armor
                            gamePlayer.health -= fightBoss.damage;
                        }
                        fightBoss.health -= gamePlayer.damage;
                    }
                }
                else if (choice == "block")
                {
                    // Player chose to block
                    // Calc block chance
                    if (randomNumberGen.Next(100) > gamePlayer.blockChance)
                    {
                        // Player successfully blocked; decrease/negate enemy damage
                        if (isSuperCrit)
                        {
                            // Super crit occurred. Take a quarter of super crit damage
                            Console.WriteLine("The monster hit you with a super crit, but you blocked part of the damage!");
                            Console.WriteLine("You took " + (fightBoss.damage / 4) + " damage.");
                            gamePlayer.health -= (fightBoss.damage / 4);
                        }
                        else
                        {
                            // Super crit did not occur. Negate all damage
                            Console.WriteLine("You blocked successfully and took no damage, as well as dealing some to the monster!");
                            fightBoss.health -= (gamePlayer.damage / 2);
                        }
                    }
                    else
                    {
                        // Player did not block successfully; take damage and deal none
                        Console.WriteLine("You did not block successfully!");
                        gamePlayer.health -= fightBoss.damage;
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
                            (from potion in potionList where (potion.name == cPotion.name) select potion).ToList().Count
                            );
                        i++;
                    }

                    // Select potion, heal player, remove potion from inventory
                    Console.WriteLine("Write the index of the potion you want to use: ");
                    int chosenIndex = 1;
                    try {
                        chosenIndex = (Int32.Parse(Console.ReadLine()) - 1);
                    } catch (FormatException e)
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
                    if (randomNumberGen.Next(100) <= fightBoss.evadeChance)
                    {
                        // Player successfully evaded
                        // Return to village
                        fightBoss.health = 0;
                        fightBoss.isEvaded = true;
                    }
                    else
                    {
                        // Player did not evade successfully. Check if supercrit occurred
                        if (isSuperCrit)
                        {
                            // Super crit occurred. Take 10x evade damage (10 * normalDamage / 3)
                            Console.WriteLine("You were unable to escape the boss, and the boss hit you with a super crit!");
                            Console.WriteLine("You took " + (fightBoss.damage * 4) + " damage!");
                            gamePlayer.health -= (fightBoss.damage * 4);
                        }
                        else
                        {
                            // Super crit did not occur. Take evade damage (normalDamage / 3)
                            int evadeDamage = (fightBoss.damage / 3);
                            Console.WriteLine("You were unable to escape the boss! You took " + evadeDamage + " damage.");
                            gamePlayer.health -= evadeDamage;
                        }
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
            return randomNumberGen.Next((level * 2), (level * 10));
        }
    }
}
