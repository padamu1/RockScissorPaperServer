using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using SimulFactory.Common.Thread;
using SimulFactory.Core;
using SimulFactory.Game.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Matching
{
    public class Match
    {
        private List<PcInstance> pcList;            // 현재 매칭된 유저가 들어가 있는 객체
        private int matchUserWaitTime;              // 유저의 행동을 기다린 시간
        private MatchThread mt;                     // 매칭 쓰레드
        private Define.MATCH_STATE matchState;      // 현재 매치 상태
        private int matchRound;                     // 현재 진행중인 매치 라운드
        private long winUserNo;                     // 라운드에서 승리한 유저 넘버
        private Dictionary<int,int> userWinCountDic;// 각 유저가 승리한 카운트
        private Dictionary<int, double> dmgDic;     // 데미지 게산을 위한 임시 보관소
        public Match()
        {
            matchUserWaitTime = 0;
            matchRound = 0;
            pcList = new List<PcInstance>();
            userWinCountDic = new Dictionary<int, int>();
            mt = new MatchThread(this);
            matchState = Define.MATCH_STATE.MATCH_READY;
            dmgDic = new Dictionary<int, double>();
        }
        /// <summary>
        /// 매칭에 참여할 유저 추가
        /// </summary>
        public void AddPcInstance(PcInstance pcInstance)
        {
            pcList.Add(pcInstance);
            int teamNo = userWinCountDic.Count+1;
            pcInstance.GetPcPvp().SetTeamNo(teamNo);
            userWinCountDic.Add(teamNo,0);
        }

        #region 매칭 관련된 상태 Getter 메서드
        /// <summary>
        /// 현재 매칭의 상태 확인
        /// </summary>
        /// <returns></returns>
        public Define.MATCH_STATE GetMatchState()
        {
            return matchState;
        }
        /// <summary>
        /// 매칭 대기중인 유저의 상태 확인
        /// </summary>
        public Define.MATCH_READY_STATE CheckUserWaitState()
        {
            int acceptCount = 0;    // 매칭 수락을 한 유저 수
            foreach (PcInstance pc in pcList)
            {
                if (pc.GetPcPvp().GetMatchAccept())
                {
                    acceptCount++;
                }
            }

            if (acceptCount == pcList.Count) // 유저 리스트 수와 매칭 수락한 유저 수가 같은 경우
            {
                return Define.MATCH_READY_STATE.MATCH_START_SUCCESS;
            }
            else
            {
                if (CheckWaitTime()) // 대기 시간이 남아있는지 확인
                {
                    return Define.MATCH_READY_STATE.MATCH_START_WAIT;
                }
                else
                {
                    return Define.MATCH_READY_STATE.MATCH_START_FAILED;
                }
            }
        }
        /// <summary>
        /// 현재 진행중인 라운드 번호 반환하는 함수
        /// </summary>
        /// <returns></returns>
        public int GetMatchRound()
        {
            return matchRound;
        }
        #endregion

        #region 매칭 대기 ~ 시작
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
        public void SendMatchStartResult(Define.MATCH_READY_STATE result)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            if (result == Define.MATCH_READY_STATE.MATCH_START_SUCCESS)
            {
                param.Add(0, 0);    // 성공인 경우
                foreach (PcInstance pc in pcList)
                {
                    matchState = Define.MATCH_STATE.MATCH_START;
                    matchUserWaitTime = 0;
                    mt.ThreadStart(Define.MATCH_SYSTEM_DELAY_TIME);
                    pc.SendPacket((byte)Define.EVENT_CODE.MatchingResponseS, param);
                }
            }
            else
            {
                param.Add(0, 1);    // 실패인 경우
                foreach (PcInstance pc in pcList)
                {
                    pc.SendPacket((byte)Define.EVENT_CODE.MatchingResponseS, param);
                    if (pc.GetPcPvp().GetMatchAccept())
                    {
                        // 수락을 한 유저는 실패를 했지만 다시 매칭을 이어갈 수 있도록 넣어줌
                        MatchSystem.GetInstance().AddPcInsatnce(pc);
                    }
                }
            }
        }
        /// <summary>
        /// 유저가 매칭 수락하는 것을 기다림
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
        #endregion

        #region 매칭 진행 중 로직
        /// <summary>
        /// 진행중인 매치 라운드 확인
        /// </summary>
        public void MatchRoundChecker()
        {
            if(matchRound > Define.MAX_ROUND_COUNT)
            {
                EndGame();
            }
            else
            {
                if(matchUserWaitTime++ > Define.MATCH_USER_WAIT_TIME)
                {
                    EndRound(-1);
                    return;
                }
                else
                {
                    EndRound(CheckRoundResult());
                }
            }
        }
        /// <summary>
        /// 라운드의 결과를 확인 0 : 결과 제출이 안됨, 1 : 1p 승리, 2 : 2p 승리
        /// </summary>
        /// <returns>승리한 유저 넘버</returns>
        private int CheckRoundResult()
        {
            if(dmgDic.Count == 2)
            {
                int winUserNo = 1;
                // 데미지 비교
                if (dmgDic[1] < dmgDic[2])
                {
                    winUserNo = 2;
                }
                // 다음 계산을 위해 초기화
                dmgDic.Clear();
                return winUserNo;
            }
            return 0;
        }
        /// <summary>
        /// 라운드 종료시 동작 메서드 - winUserNo 가 0이 들어온 경우 다음 라운드로 진행 안함, -1이 들어온 경우 플레이어 누군가가 제출을 안함
        /// </summary>
        /// <param name="winUserNo"></param>
        private void EndRound(int winUserNo = 0)
        {
            if(winUserNo == 0)
            {
                return;
            }
            if(winUserNo == -1)
            {
                // 제출을 한 유저만 승리 처리 - 인터넷 상태가 안좋은 유저의 경우 자동 제출이 안됐을 것으로 가정
                if(!dmgDic.ContainsKey(1))
                {
                    userWinCountDic[2] = Define.MAX_ROUND_COUNT;
                }
                else
                {
                    userWinCountDic[1] = Define.MAX_ROUND_COUNT;
                }
                EndGame();
                return;
            }

            userWinCountDic[winUserNo]++;
            matchRound++;
        }
        /// <summary>
        /// 각 유저의 데미지 설정
        /// </summary>
        /// <param name="teamNo"></param>
        /// <param name="dmg"></param>
        public void SetDmg(int teamNo, double dmg)
        {
            dmgDic.Add(teamNo, dmg);
        }
        #endregion

        #region 매칭 종료 후 로직
        /// <summary>
        /// 게임 종료시 호출된 메서드
        /// </summary>
        private void EndGame()
        {
            // 승리한 유저 설정
            int winUserNo = 1;
            if (userWinCountDic[1] > userWinCountDic[2])
            {
                winUserNo = 1;
            }
            else
            {
                winUserNo = 2;
            }

            // 결과 각 유저에게 전송
            foreach(PcInstance pc in pcList)
            {
                if (winUserNo == pc.GetPcPvp().GetTeamNo())
                {
                    S_MatchingResult.MatchingResultS(pc, true);
                }
                else
                {
                    S_MatchingResult.MatchingResultS(pc, false);
                }
            }

            // 스레드 종료
            ThreadManager.GetInstance().RemoveWorker(mt);
        }
        #endregion
    }
}
