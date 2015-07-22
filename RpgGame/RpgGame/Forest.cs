﻿using System;
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
                        gamePlayer.health -= fightMonster.damage;
                        fightMonster.health -= (gamePlayer.damage * 2);
                    }
                    else
                    {
                        // Player did not crit; do standard damage
                        gamePlayer.health -= fightMonster.damage;
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
