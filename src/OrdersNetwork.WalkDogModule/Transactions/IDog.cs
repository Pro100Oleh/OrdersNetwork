using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.WalkDogModule.Transactions
{
    public interface IDog
    {
        string Name { get; }
        bool Small { get; }
        bool Aggresive { get; }
    }
}
