using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core
{
    public class NetworkUser : IUser
    {
        public NetworkUser(string name)
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

        public bool Equals(IUser user)
        {
            return string.Equals(Name, user.Name, StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is IUser))
            {
                return false;
            }

            return Equals((IUser)obj);
        }

        public override string ToString()
        {
            return Name;
        }


        public static implicit operator NetworkUser(string value)
        {
            return new NetworkUser(value);
        }
    }
}
