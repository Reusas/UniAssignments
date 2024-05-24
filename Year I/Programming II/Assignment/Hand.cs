using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    abstract class Hand : Item
    {

        public Hand(ItemType typeOfItem,string name, int cost, int weight, int cleaningMagic, int protectiveMagic, string description) : base(typeOfItem, name, cost, weight, cleaningMagic, protectiveMagic, description)
        {
            _iconName = "hand-icon";
        }

    }
}
