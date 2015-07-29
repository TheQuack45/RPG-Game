using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace RpgGame
{
    static class SaveAndLoad
    {
        // Methods declaration
        public static void save(Player gamePlayer)
        {
            // Saves current player information into text file
            string acctName = Environment.UserName;
            TextWriter saveWriter = null;
            try {
                saveWriter = new StreamWriter("C:\\Users\\" + acctName + "\\Desktop\\" + gamePlayer.name + "-Save.txt");
            } catch (FileNotFoundException e)
            {
                Console.WriteLine("Save write failed.");
            }
            saveWriter.WriteLine("health:" + gamePlayer.health);
            saveWriter.WriteLine("healthcap:" + gamePlayer.health);
            saveWriter.WriteLine("damage:" + gamePlayer.damage);
            saveWriter.WriteLine("coins:" + gamePlayer.coins);
            saveWriter.WriteLine("crit:" + gamePlayer.critChance);
            saveWriter.WriteLine("armor:" + gamePlayer.armorPoints);
            saveWriter.WriteLine("block:" + gamePlayer.blockChance);
            saveWriter.WriteLine("level:" + gamePlayer.level);
            saveWriter.Close();
        }

        public static Player load(string name)
        {
            string acctName = Environment.UserName;
            TextReader saveReader = null;
            try {
                saveReader = new StreamReader("C:\\Users\\" + acctName + "\\Desktop\\" + name + "-Save.txt");
            } catch (FileNotFoundException e)
            {
                Console.WriteLine("Save file load failed.");
                return null;
            }

            string fullFile = saveReader.ReadToEnd();
            int health = Int32.Parse(Regex.Split(Regex.Match(fullFile, "health:[\\d]{1,}").ToString(), "health:")[1]);
            int healthCap = Int32.Parse(Regex.Split(Regex.Match(fullFile, "healthcap\\:[\\d]{1,}").ToString(), "healthcap:")[1]);
            int damage = Int32.Parse(Regex.Split(Regex.Match(fullFile, "damage:[\\d]{1,}").ToString(), "damage:")[1]);
            int coins = Int32.Parse(Regex.Split(Regex.Match(fullFile, "coins:[\\d]{1,}").ToString(), "coins:")[1]);
            int crit = Int32.Parse(Regex.Split(Regex.Match(fullFile, "crit:[\\d]{1,}").ToString(), "crit:")[1]);
            int armor = Int32.Parse(Regex.Split(Regex.Match(fullFile, "armor:[\\d]{1,}").ToString(), "armor:")[1]);
            int block = Int32.Parse(Regex.Split(Regex.Match(fullFile, "block:[\\d]{1,}").ToString(), "block:")[1]);
            int level = Int32.Parse(Regex.Split(Regex.Match(fullFile, "level:[\\d]{1,}").ToString(), "level:")[1]);

            return new Player(name, health, healthCap, damage, coins, crit, armor, block, level);
        }
    }
}
