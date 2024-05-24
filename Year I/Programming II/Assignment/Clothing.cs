using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class Clothing : Item
    {

        public Clothing(string name, int cost, int weight, int cleaningMagic, int protectiveMagic, string description) : base(ItemType.Clothing,name,cost,weight,cleaningMagic,protectiveMagic,description)
        {
            _iconName = "clothing-icon";
        }


    }
}
