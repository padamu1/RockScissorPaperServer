using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using SimulFactory.Core.Sql;
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
            // 타입 구분 필요
            switch (pc.GetUserData().LoginType)
            {
                case 1:
                    bool result = GoogleUtil.CheckTokenAsync("").Result;
                    
                    // 구글 로그인
                    break;
                case 2:
                    // 임시
                    break;
                case 0:
                default:
                    // Guest 로그인
                    if (pc.GetUserData().UserNo == 0)
                    {
                        // 유저 생성
                        MakeUser(pc);
                        return;
                    }
                    CheckUser(pc);
                    break;
            }
        }
        /// <summary>
        /// 로그인 타입에 따라 UserNo 설정 후 호출되는 함수
        /// </summary>
        /// <param name="pc"></param>
        private static void CheckUser(PcInstance pc)
        {
            // 유저 정보 조회
            if (UserDBSql.GetUserNo(pc))
            {
                pc.LoadData();
                // 현재 접속중인 유저라면 이전 접속 상태를 끊고 새로 연결
                if (!PcListInstance.GetInstance().CheckUser(pc.GetUserData().UserNo))
                {
                    PcListInstance.GetInstance().RemoveInstance(pc.GetUserData().UserNo);
                }
                PcListInstance.GetInstance().AddInstance(pc);

                // 로그인 성공 메시지 보냄
                pc.SendPacket(S_Login.Data(pc, true));
                return;
            }

            // 로그인 실패시
            pc.SendPacket(S_Login.Data(pc, false));
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
        /// 임시 유저 닉네임 생성 함수
        /// </summary>
        /// <returns></returns>
        public static string MakeUserNickName()
        {
            int randomLength = Random.Shared.Next(Define.RANDOM_NICKNAME_START_LENGHT, Define.RANDOM_NICKNAME_END_LENGHT);
            StringBuilder sb = new StringBuilder();
            for (int count = 0; count < randomLength; count++)
            {
                int randomNum = Random.Shared.Next(0, Define.RANDOM_STRING.Length - 1);
                sb.Append(Define.RANDOM_STRING[randomNum]);
            }
            return sb.ToString().ToUpper();
        }
        /// <summary>
        /// 생성된 유저 넘버를 넣어서 아이디를 생성하는 함수
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="userNo"></param>
        public static void CheckUserIdValid(PcInstance pc, byte[] userNo)
        {
            // DB 내용과 비교
            long newUserNo = Math.Abs(ByteUtillity.BytesToLong(userNo));
            string newUserName = MakeUserNickName();
            pc.GetUserData().UserNo = newUserNo;
            pc.GetUserData().UserName = newUserName;
            if (UserDBSql.InsertUserSql(pc))
            {
                PcPvpSql.InsertUserPvpSql(pc);
                pc.SendPacket(S_Login.Data(pc, true));

            }
            else
            {
                MakeUser(pc);
            }
        }

    }
}
