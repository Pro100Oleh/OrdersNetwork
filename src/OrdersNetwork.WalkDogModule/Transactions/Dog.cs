using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.WalkDogModule.Transactions
{
    public class Dog : IDog
    {
        public Dog(string name, bool small = true, bool aggresive = false)
        {
            Name = name;
            Small = small;
            Aggresive = aggresive;
        }

        public string Name { get; }
        public bool Small { get; }
        public bool Aggresive { get; }

    }
}
