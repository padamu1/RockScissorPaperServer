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
        public static void LoadFriendData(PcInstance pc,ref Dictionary<string, FriendDto> friendDtoDic)
        {
            // 사용할 커넥션 가져오기
            using (MySqlConnection connection = SqlController.GetMySqlConnection())
            {
                string insertQuery = "Select * From user_friend Where user_no=@userNo";
                try //예외 처리
                {
                    // 커넥션 연결
                    connection.Open();

                    // 커맨드 설정
                    MySqlCommand command = new MySqlCommand(insertQuery, connection);

                    // 파라메터 정의
                    command.Parameters.AddWithValue("@userNo", pc.GetUserData().UserNo);

                    MySqlDataReader dr = command.ExecuteReader();
                    if (dr.Read())
                    {
                        long friendNo = dr.GetInt64("friend_no");
                        string friendName = dr.GetString("friend_name");
                        if (!friendDtoDic.ContainsKey(friendName))
                        {
                            friendDtoDic.Add(friendName, new FriendDto()
                            {
                                FriendName = friendName,
                                FriendNo = friendNo,
                            });
                        }
                    }
                    else
                    {
                        Console.WriteLine("{0} 유저의 친구가 없습니다.",pc.GetUserData().UserNo);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("실패");
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        public static void LoadFriendRequestData(PcInstance pc,ref Dictionary<string, FriendRequestDto> friendRequestDtoDic)
        {
            // 사용할 커넥션 가져오기
            using (MySqlConnection connection = SqlController.GetMySqlConnection())
            {
                string insertQuery = "Select * From user_friend_request Where user_no=@userNo";
                try //예외 처리
                {
                    // 커넥션 연결
                    connection.Open();

                    // 커맨드 설정
                    MySqlCommand command = new MySqlCommand(insertQuery, connection);

                    // 파라메터 정의
                    command.Parameters.AddWithValue("@userNo", pc.GetUserData().UserNo);

                    MySqlDataReader dr = command.ExecuteReader();
                    if (dr.Read())
                    {
                        long friendNo = dr.GetInt64("friend_no");
                        string friendName = dr.GetString("friend_name");
                        if (!friendRequestDtoDic.ContainsKey(friendName))
                        {
                            friendRequestDtoDic.Add(friendName, new FriendRequestDto()
                            {
                                FriendName = friendName,
                                FriendNo = friendNo,
                            });
                        }
                    }
                    else
                    {
                        Console.WriteLine("{0} 유저의 친구요청 데이터가 없습니다.", pc.GetUserData().UserNo);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("실패");
                    Console.WriteLine(ex.ToString());
                }
            }
        }
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
                string insertQuery = "INSERT INTO user_friend (user_no, friend_no, friend_name) VALUES {0} ON DUPLICATE KEY UPDATE friend_name = VALUES(friend_name); ";

                sb.AppendFormat("({0},{1},'{2}'),",
                    receiveUserNo,
                    requestUserNo,
                    requestUserName);
                sb.AppendFormat("({0},{1},'{2}'),",
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

                sb.AppendFormat("({0},{1},'{2}')",
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
        /// 친구 삭제시 사용
        /// </summary>
        /// <param name="userNo"></param>
        /// <param name="friendPC"></param>
        /// <returns></returns>
        public static bool DeleteFriendData(long userNo, string friendName)
        {
            int result = 0;
            StringBuilder sb = new StringBuilder();
            // 사용할 커넥션 가져오기
            using (MySqlConnection connection = SqlController.GetMySqlConnection())
            {
                string commandText = @"DELETE FROM user_friend WHERE user_no = @userNo AND friend_name = @friendName; ";

                try //예외 처리
                {
                    // 커넥션 연결
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(commandText, connection);
                    command.Parameters.AddWithValue("@userNo", userNo);
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
        /// <summary>
        /// 친구 요청 삭제 사용
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
                string commandText = @"DELETE FROM user_friend_request WHERE user_no = @userNo AND friend_name = @friendName; ";

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
