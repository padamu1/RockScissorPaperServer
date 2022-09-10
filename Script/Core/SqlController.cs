using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System;
using MySql.Data.MySqlClient;

namespace SimulFactory.Core
{
    /// <summary>
    /// 사전에 Sql 과 연결을 유지하고 확인하는 작업 후 util 에서 끌어다 작업함.
    /// </summary>
    public class SqlController
    {
        static readonly Lazy<SqlController> instanceHolder = new Lazy<SqlController>(() => new SqlController());
        public static SqlController GetInstance()
        {
            return instanceHolder.Value;
        }
        /// <summary>
        /// SqlController 생성자
        /// </summary>
        public SqlController()
        {
            string strConn = "Server=127.0.0.1;Port=3001;Database=rspuserdb; Uid=root;Pwd=#Qudtk#20050;";
            MySqlConnection connection = new MySqlConnection(strConn);
            Console.WriteLine(connection.DataSource);
            Console.WriteLine(connection.Database);

            connection.Open();
            connection.Close();
        }
    }
}
