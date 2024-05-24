using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class Spell : Item
    {

        public Spell(string name, int cost, int weight, int cleaningMagic, int protectiveMagic, string description) : base(ItemType.Spell,name,cost,weight,cleaningMagic,protectiveMagic,description)
        {
            _iconName = "spell-icon";
        }




    }
}
