using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using SimulFactory.Core;
using SimulFactory.Game.Event;

namespace SimulFactory.Game
{
    public class MessageManager
    {
        public static void ProcessData(PcInstance pc, DataFormat dataFormat)                      // 클라이언트로부터 들어온 정보를 처리하기 위한 함수
        {
            byte evCode = dataFormat.evCode;
            Dictionary<byte, object> param = dataFormat.data;
            switch (evCode)
            {
                case (byte)Define.EVENT_CODE.LoginC:
                    C_Login.LoginC(pc,param);
                    break;
                case (byte)Define.EVENT_CODE.StartMatchingC:
                    break;
                case (byte)Define.EVENT_CODE.MatchingResponseC:
                    break;
                case (byte)Define.EVENT_CODE.MatchingCancelC:
                    break;
            }
        }
    }
}
