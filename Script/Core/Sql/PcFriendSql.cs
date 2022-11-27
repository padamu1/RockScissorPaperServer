using MySql.Data.MySqlClient;
using SimulFactory.Common.Dto;
using SimulFactory.Common.Instance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Core.Sql
{
    public class PcFriendSql
    {
        /// <summary>
        /// 친구 추가시 사용
        /// </summary>
        /// <param name="userNo"></param>
        /// <param name="friendPC"></param>
        /// <returns></returns>
        public static bool InsertOrUpdateFriendData(long receiveUserNo, long requestUserNo, string receiveUserName, string requestUserName)
        {
            int result = 0;
            StringBuilder sb = new StringBuilder();
            // 사용할 커넥션 가져오기
            using (MySqlConnection connection = SqlController.GetMySqlConnection())
            {
                string insertQuery = "INSERT INTO user_friend (user_no, friend_user_no, friend_name) VALUES {0} ON DUPLICATE KEY UPDATE friend_name = VALUES(friend_name); ";

                sb.AppendFormat("({0},{1},{2}),",
                    receiveUserNo,
                    requestUserNo,
                    requestUserName);
                sb.AppendFormat("({0},{1},{2}),",
                    requestUserNo,
                    receiveUserNo,
                    receiveUserName);

                string commandText = string.Format(insertQuery, sb.ToString().TrimEnd(','));

                try //예외 처리
                {
                    // 커넥션 연결
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(commandText, connection);
                    Console.WriteLine("실행 쿼리 : " + command.CommandText);
                    result = command.ExecuteNonQuery(); // 성공시 1 들어감

                    Console.WriteLine(command.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("실패");
                    Console.WriteLine(ex.ToString());
                }
            }

            return result >= 1;
        }
        /// <summary>
        /// 친구 요청시 사용
        /// </summary>
        /// <param name="userNo"></param>
        /// <param name="friendPC"></param>
        /// <returns></returns>
        public static bool InsertOrUpdateFriendRequestData(long userNo, PcInstance friendPC)
        {
            int result = 0;
            StringBuilder sb = new StringBuilder();
            // 사용할 커넥션 가져오기
            using (MySqlConnection connection = SqlController.GetMySqlConnection())
            {
                string insertQuery = "INSERT INTO user_friend_request (user_no, friend_no, friend_name) VALUES {0} ON DUPLICATE KEY UPDATE friend_name = VALUES(friend_name); ";

                sb.AppendFormat("({0},{1},{2})",
                    userNo,
                    friendPC.GetUserData().UserNo,
                    friendPC.GetUserData().UserName);
                string commandText = string.Format(insertQuery, sb.ToString());

                try //예외 처리
                {
                    // 커넥션 연결
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(commandText, connection);
                    Console.WriteLine("실행 쿼리 : " + command.CommandText);
                    result = command.ExecuteNonQuery(); // 성공시 1 들어감

                    Console.WriteLine(command.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("실패");
                    Console.WriteLine(ex.ToString());
                }
            }

            return result >= 1;
        }
        /// <summary>
        /// 친구 요청시 사용
        /// </summary>
        /// <param name="userNo"></param>
        /// <param name="friendPC"></param>
        /// <returns></returns>
        public static bool DeleteFriendRequestData(PcInstance pc, string friendName)
        {
            int result = 0;
            StringBuilder sb = new StringBuilder();
            // 사용할 커넥션 가져오기
            using (MySqlConnection connection = SqlController.GetMySqlConnection())
            {
                string commandText = "DELETE FROM user_friend_request WHERE user_no = VALUES(@userNo) AND friend_name = VALUES(@friendName); ";

                try //예외 처리
                {
                    // 커넥션 연결
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(commandText, connection);
                    command.Parameters.AddWithValue("@userNo", pc.GetUserData().UserNo);
                    command.Parameters.AddWithValue("@friendName", friendName);
                    Console.WriteLine("실행 쿼리 : " + command.CommandText);
                    result = command.ExecuteNonQuery(); // 성공시 1 들어감

                    Console.WriteLine(command.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("실패");
                    Console.WriteLine(ex.ToString());
                }
            }

            return result >= 1;
        }
    }
}
