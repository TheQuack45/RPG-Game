using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame
{
    class Boss
    {
        // Fields declaration
        private string Name;
        public string name
        {
            get { return Name; }
            set { Name = value; }
        }
        private int Damage;
        public int damage
        {
            get { return Damage; }
            set { Damage = value; }
        }
        private int Health;
        public int health
        {
            get { return Health; }
            set { Health = value; }
        }
        private int Level;
        public int level
        {
            get { return Level; }
            set { Level = value; }
        }
        private int SuperCritChance;
        public int superCritChance
        {
            get { return SuperCritChance; }
            set { SuperCritChance = value; }
        }
        private int EvadeChance;
        public int evadeChance
        {
            get { return EvadeChance; }
            set { EvadeChance = value; }
        }
        private bool IsEvaded;
        public bool isEvaded
        {
            get { return IsEvaded; }
            set { IsEvaded = value; }
        }
        private int SuperCritIncrement;
        public int superCritIncrement
        {
            get { return SuperCritIncrement; }
            set { SuperCritIncrement = value; }
        }
        private int SuperCritAmt;
        public int superCritAmt
        {
            get { return SuperCritAmt; }
        }

        // Constructors
        public Boss()
        {

        }

        public Boss(string name, int damage, int health, int level, int superCritChance)
        {
            this.name = name;
            this.damage = damage;
            this.health = health;
            this.level = level;
            this.superCritChance = superCritChance;
        }

        // Methods definition
        public void resetSuperCrit()
        {
            this.SuperCritAmt = 0;
        }
    }
}
