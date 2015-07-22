using System;
using System.Collections.Generic;
using System.Linq;

namespace RpgGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to this game");
            Console.WriteLine("What is your name?");
            // Creates player with given name, 20 health, 5 damage, 10 coins, 25% crit chance, 0 armor points, 40% block chance
            Player gamePlayer = new Player(Console.ReadLine(), 20, 5, 10, 25, 0, 40);
            Forest fightForest = new Forest((new List<Monster> { new Monster(10, 75, 1, 1, "Beetle"), new Monster(20, 50, 3, 2, "Bear"), new Monster(15, 20, 5, 2, "mountainLionThing"), new Monster(25, 50, 5, 3, "Wild Beast") }).AsQueryable<Monster>());
            // TODO: Convert to weapon/potion/armor/etc system
            Shop buyShop = new Shop((new List<Item> { new Weapon("basicSword", 7, 20, 2), new Armor("basicChestplate", 25, 3, 5), new Potion("basicPotion", 10, 5, 5) } ).AsQueryable<Item>());

            // Greets player and prompts, begins game
            Console.WriteLine("Hello " + gamePlayer.name + "!");
            while (true)
            {
                Console.WriteLine("You have " +
                                  gamePlayer.health +
                                  " health, " +
                                  gamePlayer.coins +
                                  " coins, " +
                                  gamePlayer.damage +
                                  " damage per hit, and a " +
                                  gamePlayer.critChance +
                                  "% crit chance.");
                Console.WriteLine("What would you like to do? (village, forest, or shop)");

                string choice = Console.ReadLine();
                if (choice.Equals("village"))
                {
                    // Player heals at village
                    Console.WriteLine("You have slept at the village. You are healed.");
                    gamePlayer.fillHealth();
                }
                else if (choice.Equals("forest"))
                {
                    // Player goes to forest to fight monsters
                    Console.WriteLine("You have gone to the forest. Are you ready to fight? (y/n)");
                    string answer = Console.ReadLine();
                    if (answer.Equals("y"))
                    {
                        // Player is ready to fight. Choose monster and begin
                        Monster fightMonster = fightForest.chooseMonster(gamePlayer.level);
                        Console.WriteLine("You are a fighting a " + fightMonster.name + ".");
                        if (fightForest.fight(gamePlayer, fightMonster))
                        {
                            if (fightMonster.isEvaded)
                            {
                                // Monster was evaded; do not give gold
                                Console.WriteLine("You successfully evaded the monster!");
                            }
                            else
                            {
                                // Monster was defeated; give gold and increase level
                                Console.WriteLine("You successfully defeated the monster.");
                                int lootGold = fightForest.lootCalc(fightMonster.level);
                                Console.WriteLine("You looted " + lootGold + " gold from the monster.");
                                gamePlayer.coins += lootGold;
                                gamePlayer.level += 1;
                            }
                        }
                        else
                        {
                            // Player defeated. 
                            Console.WriteLine("You were defeated. Luckily, a kindly villager found you, " +
                            "brought you back to the village and healed you, but not before looters found you. " +
                            "You have lost all your belongings.");
                            gamePlayer.clearInv();
                            gamePlayer.fillHealth();
                            gamePlayer.deadLevel();
                        }
                    }
                    else if (answer.Equals("n"))
                    {
                        // Player is not ready to fight. Return to choice menu
                        Console.WriteLine("Returning to choice menu.");
                    }
                }
                else if (choice.Equals("shop"))
                {
                    // Player goes to shop to buy items
                    Console.WriteLine("You are at the shop. You have " + gamePlayer.coins + " coins.");
                    Console.WriteLine("Shop items: ");
                    int i = 0;
                    foreach (Item cItem in buyShop.availableItems)
                    {
                        // List each available item in shop
                        if (cItem is Weapon) {
                            Weapon cItemWeapon = null;
                            try
                            {
                                cItemWeapon = (Weapon)cItem;
                            }
                            catch (InvalidCastException e)
                            {

                            }
                            Console.WriteLine(i + ". {0} is a weapon hitting {1} damage, with {2} in stock at {3} coins each.", cItemWeapon.name, cItemWeapon.damage, cItemWeapon.stock, cItemWeapon.cost);
                        }
                        else if (cItem is Armor)
                        {
                            Armor cItemArmor = null;
                            try
                            {
                                cItemArmor = (Armor)cItem;
                            }
                            catch (InvalidCastException e)
                            {

                            }
                            Console.WriteLine(i + ". {0} is an armor piece with {1} points, {2} in stock at {3} coins each.", cItemArmor.name, cItemArmor.armorPoints, cItemArmor.stock, cItemArmor.cost);
                        }
                        else if (cItem is Potion)
                        {
                            Potion cItemPotion = null;
                            try
                            {
                                cItemPotion = (Potion)cItem;
                            }
                            catch (InvalidCastException e)
                            {

                            }
                            Console.WriteLine(i + ". {0} is a potion healing {1} points, with {2} in stock at {3} coins each.", cItemPotion.name, cItemPotion.healPoints, cItemPotion.stock, cItemPotion.cost);
                        }
                        i++;
                    }

                    // Loop through purchases until user chooses to leave
                    while (true)
                    {
                        // Receive user input of which item to buy
                        Console.WriteLine("Write index of item you want to buy, or type nothing to leave.");
                        string input = Console.ReadLine();

                        if (input == "")
                        {
                            // User does not want to buy anything, leave shop
                            break;
                        }
                        else
                        {
                            // Check if user can buy item specified
                            Item chosenItem = buyShop.availableItems.ToList<Item>()[Int32.Parse(input)];
                            if (gamePlayer.coins >= chosenItem.cost)
                            {
                                // User has enough coins to purchase; check stock
                                if (buyShop.buyItem(chosenItem))
                                {
                                    gamePlayer.coins -= chosenItem.cost;
                                    Console.WriteLine("You have purchased the item. You now have " + gamePlayer.coins + " coins.");

                                    if (chosenItem is Weapon)
                                    {
                                        Weapon chosenItemWeapon = null;
                                        try { chosenItemWeapon = (Weapon)chosenItem; }
                                        catch (InvalidCastException e) { }
                                        gamePlayer.damage = chosenItemWeapon.damage;
                                    }
                                    else if (chosenItem is Armor)
                                    {
                                        Armor chosenItemArmor = null;
                                        try { chosenItemArmor = (Armor)chosenItem; }
                                        catch (InvalidCastException e) { }
                                        gamePlayer.armorPoints = chosenItemArmor.armorPoints;
                                    }
                                    else if (chosenItem is Potion)
                                    {
                                        Potion chosenItemPotion = null;
                                        try { chosenItemPotion = (Potion)chosenItem; }
                                        catch (InvalidCastException e) { }
                                        gamePlayer.inventory.Add(chosenItemPotion);
                                    }
                                }
                            }
                            else
                            {
                                // User does not have enough coins to purchase. Loop again
                                Console.WriteLine("You do not have enough coins! You have " + gamePlayer.coins + " coins.");
                            }

                        }
                    }
                }
                else
                {
                    // Unrecognized command
                    Console.WriteLine("I don't recognize that command.");
                }
            }
        }
    }
}
