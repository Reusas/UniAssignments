using System;
using System.IO;
using System.Collections.Generic;


namespace Assignment
{
    public partial class Tasks
    {
        // Create an instance of the shop
        // This is used to keep track of the shops current state
        private Shop _theShop = new Shop();

        // Create an instance of the character
        // This is used to keep track of the characters current state
        private Character _theCharacter = new Character(100,150);

        private SaveSystem saveSystem = new SaveSystem();


        // TASK 1 -----------------------------------------------
        // 1a - Create the item hierarchy
        // 1b - Load the items from the file and create instances of the items
        // 1c - Add the items to the shop list
        // ------------------------------------------------------

        // TODO: This should load the items from the file and create instances of the items adding them to the shop.
        public void LoadShop()
        {
            StreamReader fileReader = new StreamReader("items.txt");
            string lineReadFromFile = fileReader.ReadLine();


            saveSystem.addItemsToShop(fileReader, lineReadFromFile, _theShop);


            fileReader.Close();
            _theShop.SortShop();
        }


        // TASK 2 -------------------------------------------------------
        // 2a - Buy Items from the shop into the inventory 
        // 2b - Sell items from the inventory into the shop 
        // 2c - Update the character's constructur so that when it is created it starts with the correct gold and max weight
        //      Update the characters gold based on the items that the character has equipped in their 3 type inventories
        //      Update the Buy Items to only buy items if the player has enough gold
        // 2d - Add a new item to the shop 
        // 2e - Remove a selected item from the shop 
        // --------------------------------------------------------------


        // TODO: This is called when the Buy button is clicked.
        // The selected item in the shop has been passed to this method for you.
        // This should call the appropriate methods provided by the shop and character classes
        public bool BuyItemFromShop(Item item)
        {
            _theCharacter.BuyItem(item);
            return true;
            
        }

        // TODO: This is called when the Sell button is clicked.
        // The selected item in the inventory has been passed to this method for you.
        // This should call the appropriate methods provided by the shop and character classes
        public bool SellItemToShop(Item item)
        {
            _theCharacter.SellItem(item);
            return true;
        }

        // TODO: This is called when the Create button is clicked.
        // This should create a new dialog box which allows a new item to be created.
        public void CreateShopItem()
        {
            itemCreatorForm itemCreator = new itemCreatorForm();
            itemCreator._shop = _theShop;
            itemCreator.ShowDialog();
            
        }

        // TODO: This is called when the Remove button is clicked.
        // This should remove the selected item from the shop.
        public bool RemoveShopItem(Item item)
        {
            _theShop.RemoveItem(item);
            return true;
            //throw new NotImplementedException();
        }

        // TASK 3 -------------------------------------------------------
        // 3a - Eqiup character with item in correct list (hand, clothing or spell)
        // 3b - Unequip the selected item so it appears back in the inventory
        // 3c - Update the cleaning magic label
        // 3d - Update the protective magic label
        // 3e - Update the Equip character so that it only Equips based on the equipping rules
        // 3f - Sort any list of type List<Item> by implementing the IComparable interface.
        //      You will need to call sort in the right places. 
        // --------------------------------------------------------------

        // TODO: This is called when the Euip button is clicked.
        // The selected item in the inventory has been passed to this method for you.
        // This should call the appropriate method provided by the character class
        public bool EquipItem(Item item)
        {

            _theCharacter.EquipItem(item);
            return true;


            //throw new NotImplementedException();
        }

        // TODO: This is called when the Uneuip button is clicked.
        // The selected item in the character’s panel has been passed to this method for you.
        // This should call the appropriate method provided by the character class
        public bool UnequipItem(Item item)
        {
            _theCharacter.UnequipItem(item);
            return true;
        }


        // TASK 4 -------------------------------------------------------
        // 4a - Save the current state of the program
        // 4b - Load the current state of the program
        // --------------------------------------------------------------

        // TODO: This is called when the Save menu item is clicked.
        // This should save the state of the program.
        public void SaveState()
        {
            saveSystem.SaveState(_theCharacter,_theShop);
        }

        // TODO: This is called when the Load menu item is clicked.
        // This should load a previous state of the program.
        public void LoadState()
        {
            List<Item>[] items = { _theCharacter.InventoryList, _theCharacter.ClothingList, _theCharacter.HandItemList, _theCharacter.SpellList };
            saveSystem.assingLists(items);

            
            saveSystem.LoadState(_theCharacter,_theShop);
        }

    }
}
