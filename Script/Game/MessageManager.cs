using RockScissorPaperServer.PacketSerializer.Model;
using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using SimulFactory.Core;
using SimulFactory.Core.Util;
using SimulFactory.Game.Event;

namespace SimulFactory.Game
{
    public class MessageManager
    {
        public static void ProcessData(PcInstance pc, PacketData packet)                      // 클라이언트로부터 들어온 정보를 처리하기 위한 함수
        {
            byte eventCode = packet.EvCode;
            Dictionary<byte, object> param = packet.Data;

            // 데이터가 널이 아니라면 넣어줌
            if (param == null)
            {
                return;
            }
            try
            {
                switch (eventCode)
                {
                    case (byte)Define.EVENT_CODE.LoginC:
                        C_Login.LoginC(pc, param);
                        break;
                    case (byte)Define.EVENT_CODE.StartMatchingC:
                        C_StartMatching.StartMatchingC(pc, param);
                        break;
                    case (byte)Define.EVENT_CODE.MatchingResponseC:
                        C_MatchingResponse.MatchingResponseC(pc, param);
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
                    case (byte)Define.EVENT_CODE.LoginCompleteC:
                        C_LoginComplete.LoginCompleteC(pc, param);
                        break;
                    case (byte)Define.EVENT_CODE.FriendRequestC:
                        C_FriendRequest.FriendRequestC(pc, param);
                        break;
                    case (byte)Define.EVENT_CODE.FriendRemoveC:
                        C_FriendRemove.FriendRemoveC(pc, param);
                        break;
                    case (byte)Define.EVENT_CODE.FriendReceiveC:
                        C_FriendReceive.FriendReceiveC(pc, param);
                        break;
                    case (byte)Define.EVENT_CODE.ChatC:
                        C_Chat.ChatC(pc, param);
                        break;
                    case (byte)Define.EVENT_CODE.InviteUserC:
                        C_InviteUser.InviteUserC(pc, param);
                        break;
                    case (byte)Define.EVENT_CODE.InviteReceiveC:
                        C_InviteReceive.InviteReceiveC(pc, param);
                        break;
                    case (byte)Define.EVENT_CODE.SetProfileC:
                        C_SetProfile.SetProfileC(pc, param);
                        break;
                    default:
                        break;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }
    }
}
