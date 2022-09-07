using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System;

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
        private SqlConnection mssqlconn;
        /// <summary>
        /// SqlController 생성자
        /// </summary>
        public SqlController()
        {
            string strConn = "Data Source=192.168.0.1,1433:Initial Catalog=DataBase;User Id =user1;Password=1234";
            mssqlconn = new SqlConnection(strConn);
            mssqlconn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = mssqlconn;
        }
        /// <summary>
        /// SqlConnection 반환
        /// </summary>
        /// <returns></returns>
        public SqlConnection GetSqlConnection()
        {
            return mssqlconn;
        }
    }
}
