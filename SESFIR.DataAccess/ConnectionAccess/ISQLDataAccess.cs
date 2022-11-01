using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESFIR.DataAccess.ConnectionAccess
{
    public interface ISQLDataAccess
    {
        string Connection { get; }
    }
}
