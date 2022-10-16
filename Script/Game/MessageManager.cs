using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using SimulFactory.Core;
using SimulFactory.Core.Util;
using SimulFactory.Game.Event;

namespace SimulFactory.Game
{
    public class MessageManager
    {
        public static void ProcessData(PcInstance pc, DataFormat dataFormat)                      // 클라이언트로부터 들어온 정보를 처리하기 위한 함수
        {
            byte eventCode = dataFormat.eventCode;
            Dictionary<byte, object> param = new Dictionary<byte, object>();

            // 데이터가 널이 아니라면 넣어줌
            if (dataFormat.data != null)
            {
                param = dataFormat.data;
            }
            switch (eventCode)
            {
                case (byte)Define.EVENT_CODE.LoginC:
                    C_Login.LoginC(pc,param);
                    break;
                case (byte)Define.EVENT_CODE.StartMatchingC:
                    C_StartMatching.StartMatchingC(pc);
                    break;
                case (byte)Define.EVENT_CODE.MatchingResponseC:
                    C_MatchingResponse.MatchingResponseC(pc,param);
                    break;
                case (byte)Define.EVENT_CODE.MatchingCancelC:
                    C_MatchingCancel.MatchingCancel(pc);
                    break;
                case (byte)Define.EVENT_CODE.PingC:

                    break;
                case (byte)Define.EVENT_CODE.UserBattleButtonClickedC:
                    C_UserBattleButtonClicked.UserBattleButtonClickedC(pc, param);
                    break;
                case (byte)Define.EVENT_CODE.UserNameC:
                    C_UserName.UserNameC(pc, param);
                    break;
                default:
                    break;
            }
        }
    }
}
