using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core
{
    public interface IUser : IEquatable<IUser>
    {
        string Name { get; }
    }
}
