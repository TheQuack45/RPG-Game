using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame
{
    class Weapon : Item
    {
        // Fields declaration
        private int Damage;
        public int damage
        {
            get { return Damage; }
            set { Damage = value; }
        }

        // Constructors
        public Weapon()
        {

        }

        public Weapon(string name, int damage, int cost, int stock)
        {
            this.name = name;
            this.damage = damage;
            this.cost = cost;
            this.stock = stock;
        }
    }
}
