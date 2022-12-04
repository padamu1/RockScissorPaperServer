using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using SimulFactory.Core.Sql;
using SimulFactory.Core.Util;

namespace SimulFactory.Game.Event
{
    public class C_Chat
    {
        public static void ChatC(PcInstance pc, Dictionary<byte,object> param)
        {
            Define.CHAT_TYPE chatType = (Define.CHAT_TYPE)(long)param[0];
            string chatText = (string)param[1];
            string targetName = (string)param[2];
            switch (chatType)
            {
                case Define.CHAT_TYPE.Toast:
                case Define.CHAT_TYPE.None:
                    {
                        S_Chat.ChatS(chatType, pc.GetUserData().UserName, chatText);
                    }
                    break;
                case Define.CHAT_TYPE.Whisper:
                    {
                        long targetUserNo = UserDBSql.GetUserNoByName(targetName);
                        if (targetUserNo > 0)
                        {
                            PcInstance targetUser = PcListInstance.GetInstance().GetPcInstance(targetUserNo);
                            if (targetUser == null)
                            {
                                pc.SendPacket(S_Chat.Data(Define.CHAT_TYPE.Toast, "System", "접속중이지 않은 유저입니다."));
                            }
                            else
                            {
                                targetUser.SendPacket(S_Chat.Data(chatType, pc.GetUserData().UserName, chatText));
                            }
                        }
                        pc.SendPacket(S_Chat.Data(Define.CHAT_TYPE.Toast, "System", "없는 유저입니다."));
                        break;
                    }
            }
        }
    }
}
