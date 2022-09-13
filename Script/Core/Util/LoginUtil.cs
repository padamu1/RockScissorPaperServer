using SimulFactory.Common.Instance;
using SimulFactory.Game.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Core.Util
{
    public class LoginUtil
    {
        /// <summary>
        /// 유저의 로그인 정보가 유효한지 확인하는 함수
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="userNo"></param>
        public static void CheckLogin(PcInstance pc)
        {
            if(pc.GetUserData().UserNo == 0)
            {
                // 유저 생성
                MakeUser(pc);
            }
            else
            {
                // 유저 정보 조회
                if(UserDBSqlUtil.CheckUserNo(pc))
                {

                }

                // 로그인 성공 메시지 보냄
                S_Login.LoginS(pc, true);
            }
        }
        /// <summary>
        /// 시간값과 임의로 정의된 값을 가지고 새로운 유저 넘버를 부여해주는 함수
        /// </summary>
        /// <param name="pc"></param>
        public static void MakeUser(PcInstance pc)
        {
            string userNo = (DateTimeOffset.Now.ToUnixTimeMilliseconds() * 12332131).ToString();
            byte[] byteData = Encoding.UTF8.GetBytes(userNo);
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                byte[] newUserNo = Util.RSAEncrypt(byteData, RSA.ExportParameters(false), false);
                CheckUserIdValid(pc, newUserNo);
            }
        }
        /// <summary>
        /// 생성된 유저 넘버를 넣어서 아이디를 생성하는 함수
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="userNo"></param>
        public static void CheckUserIdValid(PcInstance pc, byte[] userNo)
        {
            // DB 내용과 비교
            long newUserNo = ByteUtillity.BytesToLong(userNo);
            pc.GetUserData().UserNo = newUserNo;
            if (UserDBSqlUtil.InsertUserSql(pc))
            {
                S_Login.LoginS(pc,true);
            }
            else
            {
                MakeUser(pc);
            }
        }

    }
}
