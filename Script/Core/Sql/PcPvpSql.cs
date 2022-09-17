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
    }
}
