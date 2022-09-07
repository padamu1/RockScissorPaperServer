using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Core.Util
{
    public class SqlUtil
    {
        public static bool UpdateSql()
        {
            SqlConnection connection = SqlController.GetInstance().GetSqlConnection();
            connection.Open();

            return false;
        }
    }
}
