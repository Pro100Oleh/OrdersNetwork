using OrdersNetwork.Core.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core
{
    public class NetworkNode : INode
    {
        public NetworkNode(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            Name = name;
        }

        public string Name { get; }


        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public bool Equals(INode user)
        {
            return string.Equals(Name, user.Name, StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is INode))
            {
                return false;
            }

            return Equals((INode)obj);
        }

        public override string ToString()
        {
            return Name;
        }


        public static implicit operator NetworkNode(string value)
        {
            return new NetworkNode(value);
        }
    }
}
