using SimulFactory.Common.Instance;
using SimulFactory.Core.Sql;
using SimulFactory.Common.Bean;
using SimulFactory.Common.Dto;
namespace SimulFactory.Game.Event
{
    public class C_FriendRequest
    {
        public static void FriendRequestC(PcInstance pc, Dictionary<byte, object> param)
        {
            string userName = (string)param[0];
            long userNo = UserDBSql.GetUserNoByName(userName);
            if(userNo < 0)
            {
                Console.WriteLine("{0} 유저의 {1} 로의 친구 요청 실패 ",pc.GetUserData().UserName, userName);
                pc.SendPacket(S_FriendRequest.Data(pc, Define.RECEIVE_DATA_TYPE.Me, false));
            }
            else
            {
                if(PcFriendSql.InsertOrUpdateFriendRequestData(userNo, pc))
                {
                    Console.WriteLine("{0} 유저의 {1} 로의 친구 요청 성공 ",pc.GetUserData().UserName, userName);
                    PcInstance friendPc = PcListInstance.GetInstance().GetPcInstance(userNo);
                    if(friendPc != null)
                    {
                        friendPc.GetPcFriend().AddFriendRequest(new FriendRequestDto()
                        {
                            FriendNo = pc.GetUserData().UserNo,
                            FriendName = pc.GetUserData().UserName,
                        });
                    }
                    pc.SendPacket(S_FriendRequest.Data(pc, Define.RECEIVE_DATA_TYPE.Me, true));
                }
                else
                {
                    Console.WriteLine("{0} 유저의 {1} 로의 친구 요청 실패 ",pc.GetUserData().UserName, userName);
                    pc.SendPacket(S_FriendRequest.Data(pc, Define.RECEIVE_DATA_TYPE.Me, false));
                }
            }
        }
    }
}
