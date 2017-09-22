using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core
{
    /// <summary>
    /// Hash value which describe signature of user transaction or server block
    /// </summary>
    public struct HashValue : IEquatable<HashValue>
    {
        public HashValue(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            Value = value;
        }

        public static HashValue Empty { get; } = new HashValue();

        public string Value { get; }

        public bool IsEmpty
        {
            get { return Value == null; }
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public bool Equals(HashValue hash)
        {
            return string.Equals(Value, hash.Value, StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is HashValue))
            {
                return false;
            }

            var hash = (HashValue)obj;
            return string.Equals(Value, hash.Value, StringComparison.Ordinal);
        }


        public static bool operator ==(HashValue a, HashValue b)
        {
            return string.Equals(a.Value, b.Value, StringComparison.Ordinal);
        }

        public static bool operator !=(HashValue a, HashValue b)
        {
            return !(a == b);
        }


        public override string ToString()
        {
            return Value;
        }


        public static implicit operator HashValue(string value)
        {
            return new HashValue(value);
        }
        
    }
}
