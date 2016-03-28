using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame
{
    class Player
    {
        // Fields declaration
        private string Name;
        public string name
        {
            get { return Name; }
            set { Name = value; }
        }

        private int HealthCapacity;
        public int healthCapacity
        {
            get { return HealthCapacity; }
            set { HealthCapacity = value; }
        }

        private int Health;
        public int health
        {
            get { return Health; }
            set { Health = value; }
        }

        private int Damage;
        public int damage
        {
            get { return Damage; }
            set { Damage = value; }
        }

        private int Coins;
        public int coins
        {
            get { return Coins; }
            set { Coins = value; }
        }

        private int Level;
        public int level
        {
            get { return Level; }
            set { Level = value; }
        }

        private int CritChance;
        public int critChance
        {
            get { return CritChance; }
            set { CritChance = value; }
        }

        private int ArmorPoints;
        public int armorPoints
        {
            get { return ArmorPoints; }
            set { ArmorPoints = value; }
        }

        private int BlockChance;
        public int blockChance
        {
            get { return BlockChance; }
            set { BlockChance = value; }
        }

        private List<Item> Inventory;
        public List<Item> inventory
        {
            get { return Inventory; }
            set { Inventory = value; }
        }

        // Constructors
        public Player()
        {

        }

        public Player(string name, int healthCapacity, int damage, int coins, int critChance, int armorPoints, int blockChance)
        {
            this.name = name;
            this.healthCapacity = healthCapacity;
            this.health = healthCapacity;
            this.damage = damage;
            this.coins = coins;
            this.critChance = critChance;
            this.armorPoints = armorPoints;
            this.blockChance = blockChance;
            this.level = 1;
            this.inventory = new List<Item>();
        }

        public Player(string name, int health, int healthCapacity, int damage, int coins, int critChance, int armorPoints, int blockChance, int level)
        {
            this.name = name;
            this.healthCapacity = healthCapacity;
            this.health = health;
            this.health = healthCapacity;
            this.damage = damage;
            this.coins = coins;
            this.critChance = critChance;
            this.armorPoints = armorPoints;
            this.blockChance = blockChance;
            this.level = level;
            this.inventory = new List<Item>();
        }

        // Methods definition
        public void fillHealth() {
            addHealth(this.healthCapacity - this.health);
        }

        public int addHealth(int addedHealth) {
            if ((this.health + addedHealth) >= this.healthCapacity)
            {
                this.health = this.healthCapacity;
                return this.health;
            }
            else
            {
                this.health += addedHealth;
                return this.health;
            }
        }

        public void clearInv()
        {
            this.inventory = new List<Item>();
            this.coins = 0;
        }

        public void deadLevel()
        {
            // Half level on death
            this.level = (this.level / 2);
        }
    }
}
