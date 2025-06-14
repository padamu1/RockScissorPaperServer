using SimulFactory.Common.Instance;
using SimulFactory.Common.Bean;
using SimulFactory.Game.Matching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulFactory.Game.Matching.Mode;

namespace SimulFactory.Game.Event
{
    public class C_InviteReceive
    {
        public static void InviteReceiveC(PcInstance pc, Dictionary<byte,object> param)
        {
            bool result = (bool)param[0];
            long InviteUserNo = (long)param[1];
            PcInstance inviteUser = PcListInstance.GetInstance().GetPcInstance(InviteUserNo);
            if(result)
            {
                if (inviteUser == null)
                {
                    // 접속중이지 않음
                    pc.SendPacket(S_InviteReceive.Data(Define.RECEIVE_DATA_TYPE.Me, 1));
                }
                else
                {
                    Match match = inviteUser.GetPcPvp().GetMatch();
                    MatchSystem matchSystem = inviteUser.GetMatchSystem();
                    if (match == null && matchSystem == null)
                    {
                        matchSystem = pc.GetMatchSystem();
                        match = pc.GetPcPvp().GetMatch();
                        if (match == null && matchSystem == null)
                        {
                            NormalMatch normalMatch = new NormalMatch(NormalMatchSystem.GetInstance());
                            normalMatch.AddPcInstance(pc);
                            normalMatch.AddPcInstance(inviteUser);
                            normalMatch.CalculateEloRating();
                        }
                        else
                        {
                            // 내가 초대를 받을 수 없는 상태임
                            pc.SendPacket(S_InviteReceive.Data(Define.RECEIVE_DATA_TYPE.Me, 3));
                        }
                    }
                    else
                    {
                        // 상대방이 초대를 받을 수 없는 상태임
                        pc.SendPacket(S_InviteReceive.Data(Define.RECEIVE_DATA_TYPE.Me, 2));
                    }
                }
            }
            else
            {
                if (inviteUser != null)
                {
                    // 초대 거절을 상대방에게 알림
                    inviteUser.SendPacket(S_InviteReceive.Data(Define.RECEIVE_DATA_TYPE.Other, 1, pc));
                }
            }
        }
    }
}
