using SimulFactory.Common.Instance;
using SimulFactory.Core.Sql;
using SimulFactory.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulFactory.Common.Bean;
using SimulFactory.Game.Matching;

namespace SimulFactory.Game.Event
{
    public class C_InviteUser
    {
        public static void InviteUserC(PcInstance pc, Dictionary<byte,object> param)
        {
            string userName = (string)param[0];
            long userNo = UserDBSql.GetUserNoByName(userName);
            if(userNo > 0)
            {
                PcInstance otherUser = PcListInstance.GetInstance().GetPcInstance(userNo);
                if(otherUser == null)
                {
                    // 유저가 접속중이지 않음
                    pc.SendPacket(S_InviteUser.Data(Define.RECEIVE_DATA_TYPE.Me, 2));
                }
                else
                {
                    Match match = otherUser.GetPcPvp().GetMatch();
                    if(match == null)
                    {
                        // 상대방 유저에게 초대 메시지 보냄
                        pc.SendPacket(S_InviteUser.Data(Define.RECEIVE_DATA_TYPE.Other, pc));

                        // 유저 초대에 성공
                        pc.SendPacket(S_InviteUser.Data(Define.RECEIVE_DATA_TYPE.Me, 0));
                    }
                    else
                    {
                        // 상대방 유저 게임중임
                        pc.SendPacket(S_InviteUser.Data(Define.RECEIVE_DATA_TYPE.Me, 3));
                    }
                }
            }
            else
            {
                // 유저가 없음
                pc.SendPacket(S_InviteUser.Data(Define.RECEIVE_DATA_TYPE.Me,1));
            }
        }
    }
}
