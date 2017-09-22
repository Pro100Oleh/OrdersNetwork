using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain
{
    public class NodeSettings
    {
        /// <summary>
        /// Limits block signatures. Will be applied blocks with signature (0xFFFFFFFFFF) less or equal factor.
        /// </summary>
        public string NextHashFactor { get; set; }
    }
}
