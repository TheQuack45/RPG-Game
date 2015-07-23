using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
            TextWriter saveWriter = new StreamWriter("C:\\Users\\" + acctName + "\\Desktop\\" + gamePlayer.name + "-Save.txt");
            saveWriter.WriteLine("health:" + gamePlayer.health);
            saveWriter.WriteLine("damage:" + gamePlayer.damage);
            saveWriter.WriteLine("coins:" + gamePlayer.coins);
            saveWriter.WriteLine("crit:" + gamePlayer.critChance);
            saveWriter.WriteLine("armor:" + gamePlayer.armorPoints);
            saveWriter.WriteLine("block:" + gamePlayer.blockChance);
            saveWriter.Close();
        }
    }
}
