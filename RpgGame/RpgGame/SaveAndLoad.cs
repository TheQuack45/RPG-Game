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
            //string acctName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
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
            string[] firstRegexArr = Regex.Split(Regex.Match(saveReader.ReadToEnd(), "health:[\\d]{1,}").ToString(), "health:");
            int health = Int32.Parse(firstRegexArr[1]);
            // TODO: Fix file loading; line below throws IndexOutOfBoundsException
            int healthCap = Int32.Parse(Regex.Split(Regex.Match(saveReader.ToString(), "healthcap:[\\d]{1,}").ToString(), "healthcap:")[1]);
            int damage = Int32.Parse(Regex.Split(Regex.Match(saveReader.ToString(), "damage:[\\d]{1,}").ToString(), "damage:")[1]);
            int coins = Int32.Parse(Regex.Split(Regex.Match(saveReader.ToString(), "coins:[\\d]{1,}").ToString(), "coins:")[1]);
            int crit = Int32.Parse(Regex.Split(Regex.Match(saveReader.ToString(), "crit:[\\d]{1,}").ToString(), "crit:")[1]);
            int armor = Int32.Parse(Regex.Split(Regex.Match(saveReader.ToString(), "armor:[\\d]{1,}").ToString(), "armor:")[1]);
            int block = Int32.Parse(Regex.Split(Regex.Match(saveReader.ToString(), "block:[\\d]{1,}").ToString(), "block:")[1]);
            int level = Int32.Parse(Regex.Split(Regex.Match(saveReader.ToString(), "level:[\\d]{1,}").ToString(), "level:")[1]);

            return new Player("name", health, healthCap, damage, coins, crit, armor, block, level);
        }
    }
}
