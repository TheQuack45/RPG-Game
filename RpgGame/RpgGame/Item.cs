using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame
{
    class Item
    {
        private string Name;
        public string name
        {
            get { return Name; }
            set { Name = value; }
        }

        private int Cost;
        public int cost
        {
            get { return Cost; }
            set { Cost = value; }
        }

        private int Stock;
        public int stock
        {
            get { return Stock; }
            set { Stock = value; }
        }
    }
}
