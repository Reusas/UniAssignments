using System;
namespace Assignment
{
    // Enum - Type of item
    public enum ItemType
    {
        Clothing,
        Spell,
        LeftHand,
        RightHand,
        TwoHand,
    }
    public abstract class Item: IComparable<Item>
    {
        // Type of item
        private ItemType _typeOfItem;
        // Name of item
        private string _name;
        // Description of item
        private string _description;
        // Weight of item
        private int _weight;
        // Cost of item
        private int _cost;
        // Cleaning magic of item
        private int _cleaningMagic;
        // Protective magic of item
        private int _protectiveMagic;
        // Icon name, this will use a given icon in the GUI
        // there are three other icons you can set this to, either "clothing-icon", "spell-icon" or "hand-icon"
        protected string _iconName = "default-icon";
        
        // Constructor for an Item
        public Item(ItemType typeOfItem, string name, int cost, int weight, int cleaningMagic,
            int protectiveMagic, string description)
        {
            _typeOfItem = typeOfItem;
            _name = name;
            _cost = cost;
            _weight = weight;
            _cleaningMagic = cleaningMagic;
            _protectiveMagic = protectiveMagic;
            _description = description;
        }


        public ItemType TypeOfItem { get { return _typeOfItem; } }
        public string Name { get { return _name; } }
        public string Description { get { return _description; } }
        public int Weight { get { return _weight; } }
        public int Cost { get { return _cost; } }
        public int CleaningMagic { get { return _cleaningMagic; } }
        public int ProtectiveMagic { get { return _protectiveMagic; } }
        public string IconName { get { return _iconName; } }

        public int CompareTo(Item i)
        {
            int cmpVal = Name.CompareTo(i.Name);
            if(cmpVal == 0)
            {
                
                return 0;
            }
            else if(cmpVal == 1)
            {
                
                return 1;
            }
            else
            {
                
                return -1;
            }

        }

    }
    
}
