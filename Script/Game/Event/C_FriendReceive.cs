using SimulFactory.Common.Dto;
using SimulFactory.Common.Instance;
using SimulFactory.Core.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Event
{
    public class C_FriendReceive
    {
        public static void FriendReceiveC(PcInstance pc, Dictionary<byte,object> param)
        {
            string friendUserName = (string)param[1];
            bool result = (bool)param[0];
            FriendRequestDto friendRequestDto = pc.GetPcFriend().GetFriendRequestDto(friendUserName);
            PcInstance friendPc = PcListInstance.GetInstance().GetPcInstance(friendRequestDto.FriendNo);

            if (result)
            {
                if(friendRequestDto != null)
                {
                    PcFriendSql.InsertOrUpdateFriendData(pc.GetUserData().UserNo, friendRequestDto.FriendNo, pc.GetUserData().UserName, friendRequestDto.FriendName);
                    pc.GetPcFriend().AddFriend(new FriendDto()
                    {
                        FriendNo = friendRequestDto.FriendNo,
                        FriendName = friendRequestDto.FriendName,
                    });

                    if(friendPc != null)
                    {
                        friendPc.GetPcFriend().AddFriend(new FriendDto()
                        {
                            FriendNo = pc.GetUserData().UserNo,
                            FriendName = pc.GetUserData().UserName,
                        });
                    }
                }
                else
                {
                    // 수락했는데 데이터가 정상적이지 않은 경우

                    pc.SendPacket(S_FriendReceive.Data(Common.Bean.Define.RECEIVE_DATA_TYPE.Me, false));
                    return;
                }
            }
            else
            {
                // 거절
            }
            pc.GetPcFriend().RemoveFriendRequest(friendUserName);

            pc.SendPacket(S_FriendReceive.Data(Common.Bean.Define.RECEIVE_DATA_TYPE.Me, true));
            if (friendPc != null)
            {
                pc.SendPacket(S_FriendReceive.Data(Common.Bean.Define.RECEIVE_DATA_TYPE.Other, pc.GetUserData().UserName, result));
            }
        }
    }
}
