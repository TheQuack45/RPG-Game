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

        // Constructors

        // Methods definition
        public bool fight(Player gamePlayer, Boss fightBoss)
        {
            string choice;
            bool isSuperCrit = false;

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
                        gamePlayer.health -= fightBoss.damage;
                        fightBoss.health -= (gamePlayer.damage * 2);
                    }
                    else
                    {
                        // Player did not crit; do standard damage
                        gamePlayer.health -= fightBoss.damage;
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

                    // TODO: Add potion usage
                    
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
    }
}
