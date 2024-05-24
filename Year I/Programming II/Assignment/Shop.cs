using System.Collections.Generic;

namespace Assignment
{
    public class Shop
    {
        // This is the list that manages the items in the shop inventory
        private List<Item> _shopList = new List<Item>();

        // getter for the _shopList
        public List<Item> ShopList
        {
            get
            {
                return _shopList;
            }
        }

        // This method should add an item to the shop
        public void AddItem(Item item)
        {
            
            _shopList.Add(item);
           
        }

        public void SortShop()
        {
            _shopList.Sort();
        }

        // This method should remove an item from the shop
        // If it is successful it should return true otherwise false
        public bool RemoveItem(Item item)
        {
            return _shopList.Remove(item);
        }
    }
}
