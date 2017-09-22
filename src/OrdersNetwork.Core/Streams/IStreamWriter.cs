using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Streams
{
    /// <summary>
    /// Objects-to-stream writer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IStreamWriter<T>
    {
        /// <summary>
        /// Write content of obj to stream (deserialize)
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="obj"></param>
        void Write(IStream stream, T obj);
    }
}
