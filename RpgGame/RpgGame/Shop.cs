using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame
{
    class Shop
    {
        // Fields declaration
        private IQueryable<Item> AvailableItems;
        public IQueryable<Item> availableItems
        {
            get { return AvailableItems; }
            set { AvailableItems = value; }
        }

        // Constructors
        public Shop()
        {

        }

        public Shop(IQueryable<Item> availableItems)
        {
            this.availableItems = availableItems;
        }

        // Methods definition
        public bool buyItem(Item purchasedItem)
        {
            if (purchasedItem.stock > 0)
            {
                // Item is in stock; permit purchase
                purchasedItem.stock -= 1;
                return true;
            }
            else
            {
                // Item is not in stock; do not permit purchase
                return false;
            }
        }
    }
}
