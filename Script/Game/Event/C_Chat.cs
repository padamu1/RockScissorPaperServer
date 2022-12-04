using SimulFactory.Common.Bean;

namespace SimulFactory.Game.Event
{
    public class C_Chat
    {
        public static void ChatC(Dictionary<byte,object> param)
        {
            Define.CHAT_TYPE chatType = (Define.CHAT_TYPE)(long)param[0];
            string sendUser = (string)param[1];
            string chatText = (string)param[2];
            string targetName = (string)param[3];   // 타입이 whisper가 아닌 경우 "" 로 기입되어 옴
            S_Chat.ChatS(chatType, sendUser, chatText, targetName);
        }
    }
}
