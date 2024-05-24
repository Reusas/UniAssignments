using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class ItemCreatorClass
    {
        Shop shopToAddItemTo;
        
        List<Item> createdItems = new List<Item>();
        Item itemToAdd;

        public void CreateNewItem(string itemType, string itemName, string itemDescription, int itemCost ,int itemWeight, int cleaningMagic, int protectiveMagic)
        {

            switch (itemType)
            {
                case "Clothing":
                    Clothing clothingItem = new Clothing(itemName, itemCost, itemWeight, cleaningMagic, protectiveMagic, itemDescription);
                    itemToAdd = clothingItem;
                    break;
                case "Spell":
                    Spell spellItem = new Spell(itemName, itemCost, itemWeight, cleaningMagic, protectiveMagic, itemDescription);
                    itemToAdd = spellItem;
                    break;
                case "Left Handed Item":
                    Left_hand leftHandedItem = new Left_hand(itemName, itemCost, itemWeight, cleaningMagic, protectiveMagic, itemDescription);
                    itemToAdd = leftHandedItem;
                    break;
                case "Right Handed Item":
                    Right_hand rightHandedItem = new Right_hand(itemName, itemCost, itemWeight, cleaningMagic, protectiveMagic, itemDescription);
                    itemToAdd = rightHandedItem;
                    break;
                case "Two Handed Item":
                    Two_hand twoHandedItem = new Two_hand(itemName, itemCost, itemWeight, cleaningMagic, protectiveMagic, itemDescription);
                    itemToAdd = twoHandedItem;
                    break;

            }


        }

        public void updateShop(Shop shop)
        {
            shop.AddItem(itemToAdd);
            shop.SortShop();
        }


    }
}
