using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESFIR.DataAccess.ConnectionAccess
{
    public class SQLDataAccess : ISQLDataAccess
    {
        public string Connection { get; private set; }
        public SQLDataAccess(string connection)
        {
            Connection = connection;
        }
    }
}
