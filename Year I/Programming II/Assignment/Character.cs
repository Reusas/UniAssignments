using System;
using System.Collections.Generic;

namespace Assignment
{
    public class Character
    {
        // This is the list that manages the items in the characters inventory
        private List<Item> _inventoryList = new List<Item>();
        // This is the list that manages the hand items that are equipped to the character        
        private List<Item> _handItemList = new List<Item>();
        // This is the list that manages the clothing items that are equipped to the character
        private List<Item> _clothingList = new List<Item>();
        // This is the list that manages the spell items that are equipped to the character
        private List<Item> _spellList = new List<Item>();
        // Keeps track of the number of items equipped to each area of the character
        private int _numberLeftHandEquipped = 0;
        private int _numberRightHandEquipped = 0;
        private int _numberTwoHandEquipped = 0;
        private int _numberSpellsEquipped = 0;
        private int _numberClothingEquipped = 0;
        // Keeps track of the characters attributes
        private int _gold;
        private int _currentWeight;
        private int _cleaningMagic;
        private int _protectiveMagic;
        private int _maxWeight;


        public Character(int gold, int maxWeight)
        {
            // TODO - you should add the ability to set the gold and weight when creating the character
            _gold = gold;
            _maxWeight = maxWeight;

            

        }

        public List<Item> InventoryList { get { return _inventoryList; } }
        public List<Item> HandItemList { get { return _handItemList; } }
        public List<Item> ClothingList { get { return _clothingList; } }
        public List<Item> SpellList { get { return _spellList; } }
        public int Gold { get { return _gold; } }
        public int CurrentWeight { get { return _currentWeight; } }
        public int CleaningMagic { get { return _cleaningMagic; } }
        public int ProtectiveMagic { get { return _protectiveMagic; } }
        public int MaxWeight { get { return _maxWeight; } }

        public int NumberLeftHandEquipped { get { return _numberLeftHandEquipped;  } }
        public int NumberRightHandEquipped { get { return _numberRightHandEquipped; } }
        public int NumberTwoHandEquipped { get { return _numberTwoHandEquipped; } }
        public int NumberSpellsEquipped { get { return _numberSpellsEquipped; } }
        public int NumberClothingEquipped { get { return _numberClothingEquipped; } }

        // TODO: Check if the character has enough weight left to carry the given item.
        // If it is successful it should return true otherwise false
        public bool EnoughWeight(Item item)
        {
            if ((_maxWeight - item.Weight) >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // TODO: Check if the character has enough gold left to buy the item.
        // If it is successful it should return true otherwise false
        public bool EnoughGold(Item item)
        {
            if (_gold >= item.Cost)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // TODO: Equip the character with a given item based on its type.
        // Either a left-hand, right-hand, two-hand, clothing or spell item.
        // This should add an item from the correct character’s list and remove it from the inventory.
        // If it is successful it should return true otherwise false.
        public bool EquipItem(Item item)
        {
           
            if (EnoughWeight(item))
            {
                _maxWeight -= item.Weight;
            }
            else
            {
                return false;
            }

            switch (item.TypeOfItem)
            {
                case ItemType.Clothing:
                    if (_numberClothingEquipped < 2)
                    {
                        _clothingList.Add(item);
                        _clothingList.Sort();
                        _numberClothingEquipped++;
                    }
                    else
                    {
                        return false;
                    }
                    
                    break;
                case ItemType.Spell:
                    if (_numberSpellsEquipped<2)
                    {
                        _spellList.Add(item);
                        _spellList.Sort();
                        _numberSpellsEquipped++;
                    }
                    else
                    {
                        return false;
                    }

                    break;
                case ItemType.LeftHand:
                    

                    if(_numberLeftHandEquipped<1 && _numberTwoHandEquipped < 1)
                    {
                        _handItemList.Add(item);
                        _spellList.Sort();
                        _numberLeftHandEquipped++;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case ItemType.RightHand:
                    if (_numberRightHandEquipped < 1 && _numberTwoHandEquipped < 1)
                    {
                        _handItemList.Add(item);
                        _spellList.Sort();
                        _numberRightHandEquipped++;
                    }
                    else
                    {
                        return false;
                    }
                    
                    break;
                case ItemType.TwoHand:
                    if(_numberLeftHandEquipped<1 && _numberRightHandEquipped < 1)
                    {
                        _handItemList.Add(item);
                        _handItemList.Sort();
                        _numberTwoHandEquipped++;
                    }
                    else
                    {
                        return false;
                    }
                    
                    break;
                default:
                    break;
            }
            _inventoryList.Remove(item);
            _cleaningMagic += item.CleaningMagic;
            _protectiveMagic += item.ProtectiveMagic;

            _inventoryList.Sort();

            return true;
        }

        // TODO: Unequip an item from the character.
        // This should remove an item from the correct character’s list and add it to the inventory.
        // If it is successful it should return true otherwise false.
        public bool UnequipItem(Item item)
        {
            switch (item.TypeOfItem)
            {
                case ItemType.Clothing:
                    _clothingList.Remove(item);
                    _numberClothingEquipped--;
                    break;
                case ItemType.Spell:
                    _spellList.Remove(item);
                    _numberSpellsEquipped--;
                    break;
                case ItemType.LeftHand:
                    _handItemList.Remove(item);
                    _numberLeftHandEquipped--;
                    break;
                case ItemType.RightHand:
                    _handItemList.Remove(item);
                    _numberRightHandEquipped--;
                    break;
                case ItemType.TwoHand:
                    _handItemList.Remove(item);
                    _numberTwoHandEquipped--;
                    break;
                default:
                    break;
            }
            _cleaningMagic -= item.CleaningMagic;
            _protectiveMagic -= item.ProtectiveMagic;
            _maxWeight += item.Weight;
            _inventoryList.Add(item);
            _inventoryList.Sort();
            return true;
        }

        // TODO: This item should buy an item if the character has enough gold. 
        // It should then be added to the characters inventory list.
        // return true if successful, false otherwise.
        public bool BuyItem(Item item)
        {
            if (EnoughGold(item))
            {
                _gold -= item.Cost;
                _inventoryList.Add(item);
                return true;
            }
            else
            {
                return false;
            }

 
            
        }

        // TODO: This should remove an item from the characters inventory list
        // If it is successful it should return true otherwise false
        public bool SellItem(Item item)
        {
            _inventoryList.Remove(item);

            _gold += item.Cost;


            return true;
        }

        public void updateLoadedStats(List<int> updatesStats)
        {
            _gold = updatesStats[0];
            _cleaningMagic = updatesStats[1];
            _protectiveMagic = updatesStats[2];
            _maxWeight = updatesStats[3];
            _numberClothingEquipped = updatesStats[4];
            _numberSpellsEquipped = updatesStats[5];
            _numberLeftHandEquipped = updatesStats[6];
            _numberRightHandEquipped = updatesStats[7];
            _numberTwoHandEquipped = updatesStats[8];
        }
    }
}
