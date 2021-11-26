using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.GroupSpace.Entities
{
    interface IConcurrencyAware
    {
        string ConcurrencyStamp { get; set; }

    }
}
