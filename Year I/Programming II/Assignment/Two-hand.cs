using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class Two_hand : Hand
    {
        public Two_hand(string name, int cost, int weight, int cleaningMagic, int protectiveMagic, string description): base(ItemType.TwoHand, name, cost, weight, cleaningMagic, protectiveMagic, description)
        {

        }

    }
}
