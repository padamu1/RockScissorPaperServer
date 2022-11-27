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
    public class C_FriendRemove
    {
        public static void FriendRemoveC(PcInstance pc, Dictionary<byte,object> param)
        {
            string friendname = (string)param[0];
            FriendDto friendDto = pc.GetPcFriend().GetFriend(friendname);
            if(friendDto == null)
            {
                S_FriendRemove.FriendRemoveS(pc, Common.Bean.Define.FRIEND_RECEIVE_DATA_TYPE.Me, false);
                return;
            }
            PcFriendSql.DeleteFriendData(pc.GetUserData().UserNo, friendname);
            PcFriendSql.DeleteFriendData(friendDto.FriendNo, pc.GetUserData().UserName);
            PcInstance friendPc = PcListInstance.GetInstance().GetPcInstance(friendDto.FriendNo);
            if (friendPc != null)
            {
                friendPc.GetPcFriend().RemoveFriend(pc.GetUserData().UserName);
                S_FriendRemove.FriendRemoveS(friendPc, Common.Bean.Define.FRIEND_RECEIVE_DATA_TYPE.Other, pc.GetUserData().UserName);
            }
            pc.GetPcFriend().RemoveFriend(pc.GetUserData().UserName);

            S_FriendRemove.FriendRemoveS(pc, Common.Bean.Define.FRIEND_RECEIVE_DATA_TYPE.Me, true);
        }
    }
}
