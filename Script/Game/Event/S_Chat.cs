using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using SimulFactory.Core.Util;

namespace SimulFactory.Game.Event
{
    public class S_Chat
    {
        public static void ChatS(Define.CHAT_TYPE chatType, string sendUser, string chatText, string targetName)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            param.Add(0, (int)chatType);
            switch(chatType)
            {
                case Define.CHAT_TYPE.Toast:
                    param.Add(1, "System");
                    param.Add(2, chatText);
                    PcListInstance.GetInstance().SendPacket(new EventData((int)Define.EVENT_CODE.ChatS, param));
                    break;
                case Define.CHAT_TYPE.None:
                    param.Add(1, sendUser);
                    param.Add(2, chatText);
                    PcListInstance.GetInstance().SendPacket(new EventData((int)Define.EVENT_CODE.ChatS, param));
                    break;
                case Define.CHAT_TYPE.Whisper:
                    param.Add(1, sendUser);
                    param.Add(2, chatText);
                    param.Add(3, targetName);
                    PcListInstance.GetInstance().SendPacket(new EventData((int)Define.EVENT_CODE.ChatS, param));
                    break;
            }
        }
    }
}
