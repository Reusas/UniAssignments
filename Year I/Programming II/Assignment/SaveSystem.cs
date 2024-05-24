using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Assignment
{
    class SaveSystem
    {
        public int _gold;
        public int _currentWeight;
        public int _cleaningMagic;
        public int _protectiveMagic;
        public int _maxWeight;
        public int _numberLeftHandEquipped = 0;
        public int _numberRightHandEquipped = 0;
        public int _numberTwoHandEquipped = 0;
        public int _numberSpellsEquipped = 0;
        public int _numberClothingEquipped = 0;

        public List<Item> _inventoryList = new List<Item>(); 
        public List<Item> _handItemList = new List<Item>();
        public List<Item> _clothingList = new List<Item>();
        public List<Item> _spellList = new List<Item>();

        public List<Item> _shopList = new List<Item>();



        public void SaveState(Character character, Shop shop)
        {
            _gold = character.Gold;
            _currentWeight = character.CurrentWeight;
            _cleaningMagic = character.CleaningMagic;
            _protectiveMagic = character.ProtectiveMagic;
            _maxWeight = character.MaxWeight;
            _numberClothingEquipped = character.NumberClothingEquipped;
            _numberSpellsEquipped = character.NumberSpellsEquipped;
            _numberLeftHandEquipped = character.NumberLeftHandEquipped;
            _numberRightHandEquipped = character.NumberRightHandEquipped;
            _numberTwoHandEquipped = character.NumberTwoHandEquipped;

            _inventoryList = character.InventoryList;
            _handItemList = character.HandItemList;
            _clothingList = character.ClothingList;
            _spellList = character.SpellList;

            _shopList = shop.ShopList;

            string data = _gold  + "|" + _cleaningMagic + "|" + _protectiveMagic + "|" + _maxWeight + "|" + _numberClothingEquipped + "|" + _numberSpellsEquipped + "|" + _numberLeftHandEquipped + "|" + _numberRightHandEquipped + "|" + _numberTwoHandEquipped;

            StreamWriter writer = new StreamWriter("User.txt",false);
            writer.WriteLine(data);

            writer.WriteLine();


            writer.Close();

            writeItemToFile(_inventoryList,"InventoryList.txt");
            writeItemToFile(_handItemList,"HandItemList.txt");
            writeItemToFile(_clothingList,"ClothingList.txt");
            writeItemToFile(_spellList,"SpellList.txt");

           
        }

        void clearLists(List<Item>[] lists)
        {
            for (int i = 0; i < lists.Length; i++)
            {
                lists[i].Clear();
            }
        }

        public void assingLists(List<Item>[] lists)
        {
            _inventoryList = lists[0];
            _clothingList = lists[1];
            _handItemList = lists[2];
            _spellList = lists[3];
        }

        public void LoadState(Character _char, Shop shop)
        {
            StreamReader fileReader = new StreamReader("User.txt");

            List<int> characterData = getCharacterData(fileReader);

            List<Item>[] listsToClear = { _inventoryList, _handItemList, _clothingList, _spellList };

            clearLists(listsToClear);


            addItemsToList("InventoryList.txt", _inventoryList);
            addItemsToList("HandItemList.txt", _handItemList);
            addItemsToList("ClothingList.txt", _clothingList);
            addItemsToList("SpellList.txt", _spellList);

            _char.updateLoadedStats(characterData);
        }

        public void addItemsToList(string fileToReadFrom,List<Item> list)
        {
            StreamReader reader = new StreamReader(fileToReadFrom);
            string line = reader.ReadLine();


            while (line != null)
            {
                List<string> information = new List<string>();

                for (int i = 0; i < 7; i++)
                {
                    string info = line.Split('|')[i];

                    information.Add(info);
                }

                string itemType = information[0];
                string itemName = information[1];
                string itemDescription = information[2];
                int itemWeight = int.Parse(information[3]);
                int itemCost = int.Parse(information[4]);
                int cleaningMagic = int.Parse(information[5]);
                int protectiveMagic = int.Parse(information[6]);


                switch (itemType)
                {
                    case "C":
                        Clothing clothingItem = new Clothing(itemName, itemCost, itemWeight, cleaningMagic, protectiveMagic, itemDescription);
                        list.Add(clothingItem);
                        break;
                    case "S":
                        Spell spellItem = new Spell(itemName, itemCost, itemWeight, cleaningMagic, protectiveMagic, itemDescription);
                        list.Add(spellItem);
                        break;
                    case "H1L":
                        Left_hand leftHandedItem = new Left_hand(itemName, itemCost, itemWeight, cleaningMagic, protectiveMagic, itemDescription);
                        list.Add(leftHandedItem);
                        break;
                    case "H1R":
                        Right_hand rightHandedItem = new Right_hand(itemName, itemCost, itemWeight, cleaningMagic, protectiveMagic, itemDescription);
                        list.Add(rightHandedItem);
                        break;
                    case "H2":
                        Two_hand twoHandedItem = new Two_hand(itemName, itemCost, itemWeight, cleaningMagic, protectiveMagic, itemDescription);
                        list.Add(twoHandedItem);
                        break;

                }
                line= reader.ReadLine();
            }

            reader.Close();
        }

        public void addItemsToShop(StreamReader fileReader, string lineReadFromFile, Shop _theShop)
        {
            while (lineReadFromFile != null)
            {
                Console.WriteLine(lineReadFromFile);
                List<string> information = new List<string>();

                for (int i = 0; i < 7; i++)
                {
                    string info = lineReadFromFile.Split('|')[i];

                    information.Add(info);
                }

                string itemType = information[0];
                string itemName = information[1];
                string itemDescription = information[2];
                int itemWeight = int.Parse(information[3]);
                int itemCost = int.Parse(information[4]);
                int cleaningMagic = int.Parse(information[5]);
                int protectiveMagic = int.Parse(information[6]);


                switch (itemType)
                {
                    case "C":
                        Clothing clothingItem = new Clothing(itemName, itemCost, itemWeight, cleaningMagic, protectiveMagic, itemDescription);
                        _theShop.AddItem(clothingItem);
                        break;
                    case "S":
                        Spell spellItem = new Spell(itemName, itemCost, itemWeight, cleaningMagic, protectiveMagic, itemDescription);
                        _theShop.AddItem(spellItem);
                        break;
                    case "H1L":
                        Left_hand leftHandedItem = new Left_hand(itemName, itemCost, itemWeight, cleaningMagic, protectiveMagic, itemDescription);
                        _theShop.AddItem(leftHandedItem);
                        break;
                    case "H1R":
                        Right_hand rightHandedItem = new Right_hand(itemName, itemCost, itemWeight, cleaningMagic, protectiveMagic, itemDescription);
                        _theShop.AddItem(rightHandedItem);
                        break;
                    case "H2":
                        Two_hand twoHandedItem = new Two_hand(itemName, itemCost, itemWeight, cleaningMagic, protectiveMagic, itemDescription);
                        _theShop.AddItem(twoHandedItem);
                        break;

                }
                lineReadFromFile = fileReader.ReadLine();
            }

            fileReader.Close();
        }


        

        List<int> getCharacterData(StreamReader reader)
        {
            string lineReadFromFile = reader.ReadLine();
            List<int> charData = new List<int>();

            for (int i = 0; i < 9; i++)
            {
                string info = lineReadFromFile.Split('|')[i];
                int parsedInfo = int.Parse(info);

                charData.Add(parsedInfo);
            }

            reader.Close();
            
            return charData;
        }

        void writeItemToFile(List<Item> list, string fileName)
        {

            StreamWriter writer = new StreamWriter(fileName);
            foreach (Item i in list)
            {
                string itemTypeShort = "";

                if (i.TypeOfItem == ItemType.Clothing)
                {
                    itemTypeShort = "C";
                }
                else if (i.TypeOfItem == ItemType.Spell)
                {
                    itemTypeShort = "S";
                }
                else if (i.TypeOfItem == ItemType.LeftHand)
                {
                    itemTypeShort = "H1L";
                }
                else if (i.TypeOfItem == ItemType.RightHand)
                {
                    itemTypeShort = "H1R";
                }
                else if (i.TypeOfItem == ItemType.TwoHand)
                {
                    itemTypeShort = "H2";
                }

                string itemData = itemTypeShort + "|" + i.Name + "|" + i.Description + "|" + i.Weight + "|" + i.Cost + "|" + i.CleaningMagic + "|" + i.ProtectiveMagic;
                writer.WriteLine(itemData);
            }
            writer.Close();
        }


    }
}
