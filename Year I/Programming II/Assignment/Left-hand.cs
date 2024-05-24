using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class Left_hand : Hand
    {
        public Left_hand(string name, int cost, int weight, int cleaningMagic, int protectiveMagic, string description): base(ItemType.LeftHand, name, cost, weight, cleaningMagic, protectiveMagic, description)
        {

        }

    }
}
