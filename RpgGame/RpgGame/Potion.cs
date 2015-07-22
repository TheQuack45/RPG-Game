using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame
{
    class Potion : Item
    {
        private int HealPoints;
        public int healPoints
        {
            get { return HealPoints; }
            set { HealPoints = value; }
        }

        public Potion()
        {

        }

        public Potion(string name, int cost, int stock, int healPoints)
        {
            this.name = name;
            this.cost = cost;
            this.stock = stock;
            this.healPoints = healPoints;
        }
    }
}
