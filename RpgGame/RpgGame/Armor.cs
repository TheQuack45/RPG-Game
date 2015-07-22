using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame
{
    class Armor : Item
    {
        private int ArmorPoints;
        public int armorPoints
        {
            get { return ArmorPoints; }
            set { ArmorPoints = value; }
        }

        public Armor()
        {

        }

        public Armor(string name, int cost, int stock, int armorPoints)
        {
            this.name = name;
            this.cost = cost;
            this.stock = stock;
            this.armorPoints = armorPoints;
        }
    }
}
