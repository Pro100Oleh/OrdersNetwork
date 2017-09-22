using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Streams
{
    /// <summary>
    /// Writes all values in one string
    /// </summary>
    public class StringStream : IStream
    {
        private StringBuilder _builder;

        public StringStream()
        {
            _builder = new StringBuilder(100);
        }

        private void EnsureAlive()
        {
            if (_builder == null)
            {
                throw new ObjectDisposedException("StringStream");
            }
        }

        public void Write(string value)
        {
            EnsureAlive();
            _builder.Append(value);
        }

        public void Dispose()
        {
            if (_builder != null)
            {
                _builder = null;
            }
        }

        public string GetContent()
        {
            EnsureAlive();
            return _builder.ToString();
        }
    }
}
