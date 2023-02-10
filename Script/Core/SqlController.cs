using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System;
using MySql.Data.MySqlClient;
using SimulFactory.Core.Sql;

namespace SimulFactory.Core
{
    /// <summary>
    /// 사전에 Sql 과 연결을 유지하고 확인하는 작업 후 util 에서 끌어다 작업함.
    /// </summary>
    public class SqlController
    {
        private static string strConn = "Server=rspdb.mysql.database.azure.com;Port=3306;Database=rsp_db; Uid=padamu1;Pwd=#Qudtk#20050;";
        public static void CheckSqlConnection()
        {
            MySqlConnection connection = new MySqlConnection(strConn);
            connection.Open();
            connection.Close();
        }
        /// <summary>
        /// 연결에 따른 커넥션 반환 - 파라메터를 넣지 않을 경우 rsp_db
        /// </summary>
        /// <param name="sqlName"></param>
        /// <returns>커넥션</returns>
        public static MySqlConnection GetMySqlConnection(string sqlName = "")
        {
            // sql 이름이 설정이 안되어있을 겨웅 기존에 설정된 db를 반환
            MySqlConnection connection = new MySqlConnection(strConn);
            if("".Equals(sqlName))
            {
            }
            else
            {
                connection.ChangeDatabase(sqlName);
            }
            return connection;
        }
    }
}
