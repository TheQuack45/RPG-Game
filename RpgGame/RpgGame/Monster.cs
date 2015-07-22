using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame
{
    class Monster
    {
        // Field declaration
        private int Health;
        public int health
        {
            get { return Health; }
            set { Health = value; }
        }

        private int EvadeChance;
        public int evadeChance
        {
            get { return EvadeChance; }
            set { EvadeChance = value; }
        }

        private int Damage;
        public int damage
        {
            get { return Damage; }
            set { Damage = value; }
        }

        private int Level;
        public int level
        {
            get { return Level; }
            set { Level = value; }
        }

        private string Name;
        public string name
        {
            get { return Name; }
            set { Name = value; }
        }

        private bool IsEvaded;
        public bool isEvaded
        {
            get { return IsEvaded; }
            set { IsEvaded = value; }
        }

        // Constructors
        public Monster()
        {

        }

        public Monster(int health, int evadeChance, int damage, int level, string name)
        {
            this.health = health;
            this.evadeChance = evadeChance;
            this.damage = damage;
            this.level = level;
            this.name = name;
        }

        // Methods definition
        public Monster Clone()
        {
            Monster newMonster = (Monster)this.MemberwiseClone();
            newMonster.name = (string)this.name.Clone();
            newMonster.level = this.level;
            newMonster.health = this.health;
            newMonster.evadeChance = this.evadeChance;
            newMonster.damage = this.damage;
            newMonster.isEvaded = false;

            return newMonster;
        }
    }
}
