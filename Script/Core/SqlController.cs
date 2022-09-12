using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System;
using MySql.Data.MySqlClient;
using SimulFactory.Core.Util;

namespace SimulFactory.Core
{
    /// <summary>
    /// 사전에 Sql 과 연결을 유지하고 확인하는 작업 후 util 에서 끌어다 작업함.
    /// </summary>
    public class SqlController
    {
        private static string strConn = "Server=127.0.0.1;Port=3001;Database=rspuserdb; Uid=root;Pwd=#Qudtk#20050;";
        public static void CheckSqlConnection()
        {
            MySqlConnection connection = new MySqlConnection(strConn);
            connection.Open();
            connection.Close();
            UserDBSqlUtil.InsertSql();
        }
        public static MySqlConnection GetMySqlConnection()
        {
            return new MySqlConnection(strConn);
        }
    }
}
