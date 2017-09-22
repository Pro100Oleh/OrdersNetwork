﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain
{
    public interface INextBlockFinder
    {
        bool TryFindNext(NodeState state, out NodeState nextState);
    }
}
