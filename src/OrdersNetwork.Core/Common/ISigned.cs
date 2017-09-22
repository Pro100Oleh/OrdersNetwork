using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core
{
    /// <summary>
    /// Any message that signed with hash
    /// </summary>
    public interface ISigned : IEquatable<ISigned>
    {
        HashValue Signature { get; }
    }
}
