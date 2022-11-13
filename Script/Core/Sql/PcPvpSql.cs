using MySql.Data.MySqlClient;
using SimulFactory.Common.Instance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Core.Sql
{
    public class PcPvpSql
    {
        public static bool InsertUserPvpSql(PcInstance pc)
        {
            int result = 0; // 실패로 정의

            // 사용할 커넥션 가져오기
            using (MySqlConnection connection = SqlController.GetMySqlConnection())
            {
                string insertQuery = "INSERT INTO user_pvp_db(user_no, rating, win_count, defeat_count) VALUES(@user_no, @rating, @win_count, @defeat_count)";
                try //예외 처리
                {
                    // 커넥션 연결
                    connection.Open();

                    // 커맨드 설정
                    MySqlCommand command = new MySqlCommand(insertQuery, connection);

                    // 파라메터 정의
                    command.Parameters.AddWithValue("@user_no", pc.GetUserData().UserNo);
                    command.Parameters.AddWithValue("@rating", pc.GetPcPvp().GetRating());
                    command.Parameters.AddWithValue("@win_count", pc.GetPcPvp().GetWinCount());
                    command.Parameters.AddWithValue("@defeat_count", pc.GetPcPvp().GetDefeatCount());

                    result = command.ExecuteNonQuery(); // 성공시 1 들어감
                }
                catch (Exception ex)
                {
                    Console.WriteLine("실패");
                    Console.WriteLine(ex.ToString());
                }
            }
            if (result >= 1)
            {
                Console.WriteLine("인서트 성공");
            }
            else
            {
                Console.WriteLine("인서트 실패");
            }

            return result >= 1;
        }
        public static bool GetUserPvp(PcInstance pc)
        {
            bool result = false; // 실패로 선언
            // 사용할 커넥션 가져오기
            using (MySqlConnection connection = SqlController.GetMySqlConnection())
            {
                string insertQuery = "Select * From user_pvp_db Where user_no=@userNo";
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
                        result = true;
                        pc.GetPcPvp().SetRating(dr.GetInt32("rating"));
                        pc.GetPcPvp().SetWinCount(dr.GetInt32("win_count"));
                        pc.GetPcPvp().SetDefeatCount(dr.GetInt32("defeat_count"));
                    }
                    else
                    {
                        Console.WriteLine("UnKnown UserNo");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("실패");
                    Console.WriteLine(ex.ToString());
                }
            }
            return result;
        }
        public static bool UpdateUserPvpSql(PcInstance pc)
        {
            // UPDATE[테이블] SET[열] = '변경할값' WHERE[조건]
            int result = 0;

            // 사용할 커넥션 가져오기
            using (MySqlConnection connection = SqlController.GetMySqlConnection())
            {
                string insertQuery = "Update user_pvp_db Set rating=@rating, win_count=@winCount, defeat_count=@defeatCount Where user_no=@userNo";
                try //예외 처리
                {
                    // 커넥션 연결
                    connection.Open();

                    // 커맨드 설정
                    MySqlCommand command = new MySqlCommand(insertQuery, connection);

                    // 파라메터 정의
                    command.Parameters.AddWithValue("@userNo", pc.GetUserData().UserNo);
                    command.Parameters.AddWithValue("@winCount", pc.GetPcPvp().GetWinCount());
                    command.Parameters.AddWithValue("@defeatCount", pc.GetPcPvp().GetDefeatCount());
                    command.Parameters.AddWithValue("@rating", pc.GetPcPvp().GetRating());

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

        public static bool UpdateUserPvpSql(PcInstance[] pcInstances)
        {
            int result = 0;
            StringBuilder sb = new StringBuilder();
            // 사용할 커넥션 가져오기
            using (MySqlConnection connection = SqlController.GetMySqlConnection())
            {
                string insertQuery = "INSERT INTO user_pvp_db (user_no,rating,win_count,defeat_count) VALUES {0} ON DUPLICATE KEY UPDATE rating = VALUES(rating),win_count = VALUES(win_count),defeat_count = VALUES(defeat_count); ";

                // 커맨드 설정
                foreach (PcInstance pc in pcInstances)
                {
                    sb.AppendFormat("({0},{1},{2},{3}),", pc.GetUserData().UserNo
                        , pc.GetPcPvp().GetRating()
                        , pc.GetPcPvp().GetWinCount()
                        , pc.GetPcPvp().GetDefeatCount());
                }
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
    }
}
