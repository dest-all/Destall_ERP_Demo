using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol.Models.Filters;

public partial class StringFilterModel
{
    public static implicit operator StringFilterModel(string pattern) 
        => new StringFilterModel(pattern);

}

