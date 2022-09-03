using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Matching
{
    public class Match
    {
        private List<PcInstance> pcList;    // 현재 매칭된 유저가 들어가 있는 객체
        private int matchUserWaitTime;      // 유저가 매칭을 수락하는 것을 기다린 시간
        public Match()
        {
            matchUserWaitTime = 0;
            pcList = new List<PcInstance>();
        }
        /// <summary>
        /// 매칭에 참여할 유저 추가
        /// </summary>
        public void AddPcInstance(PcInstance pcInstance)
        {
            pcList.Add(pcInstance);
        }
        /// <summary>
        /// 매칭이 성사 되었음을 클라이언트에 보냄
        /// </summary>
        public void SendMatchSuccess()
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            foreach (PcInstance pc in pcList)
            {
                pc.SendPacket((byte)Define.EVENT_CODE.MatchingSuccessS, param);
            }
        }
        /// <summary>
        /// 매칭 시작에 대한 결과를 클라이언트에 보냄
        /// </summary>
        public void SendMatchStartResult(Define.MATCH_STATE result)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            if(result == Define.MATCH_STATE.MATCH_START_SUCCESS)
            {
                param.Add(0, 0);    // 성공인 경우
                foreach (PcInstance pc in pcList)
                {
                    pc.SendPacket((byte)Define.EVENT_CODE.MatchingResponseS,param);
                }
            }
            else
            {
                param.Add(0, 1);    // 실패인 경우
                foreach (PcInstance pc in pcList)
                {
                    pc.SendPacket((byte)Define.EVENT_CODE.MatchingResponseS, param);
                    if (pc.pcPvp.MatchAccept)
                    {
                        // 수락을 한 유저는 실패를 했지만 다시 매칭을 이어갈 수 있도록 넣어줌
                        MatchSystem.GetInstance().AddPcInsatnce(pc);
                    }
                }    
            }
        }
        /// <summary>
        /// 매칭 대기중인 유저의 상태 확인
        /// </summary>
        public Define.MATCH_STATE CheckUserWaitState()
        {
            int acceptCount = 0;    // 매칭 수락을 한 유저 수
            foreach (PcInstance pc in pcList)
            {
                if (pc.pcPvp.MatchAccept)
                {
                    acceptCount++;
                }
            }

            if (acceptCount == pcList.Count) // 유저 리스트 수와 매칭 수락한 유저 수가 같은 경우
            {
                return Define.MATCH_STATE.MATCH_START_SUCCESS;
            }
            else
            {
                if(CheckWaitTime()) // 대기 시간이 남아있는지 확인
                {
                    return Define.MATCH_STATE.MATCH_START_WAIT;
                }
                else
                {
                    return Define.MATCH_STATE.MATCH_START_FAILED;
                }
            }
        }
        /// <summary>
        /// 현재 매칭 대기 중 기다린 시간 확인
        /// </summary>
        /// <returns></returns>
        private bool CheckWaitTime()
        {
            if (matchUserWaitTime > Define.MATCH_USER_WAIT_TIME)
            {
                return false;   // 매칭 대기 시간이 경과된 경우
            }
            matchUserWaitTime += Define.MATCH_SYSTEM_DELAY_TIME;    // 유저를 대기하는 시간을 증가시킴
            return true;    // 매칭 대기 시간이 남은 경우
        }
    }
}
