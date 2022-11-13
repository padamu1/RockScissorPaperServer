using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using SimulFactory.Common.Thread;
using SimulFactory.Core;
using SimulFactory.Core.Base;
using SimulFactory.Core.Sql;
using SimulFactory.Core.Util;
using SimulFactory.Game.Event;

namespace SimulFactory.Game.Matching
{
    public class Match : ThreadBase
    {
        protected bool sendMatchSuccessMessage;       // 매칭 성공 메시지를 보냈는지 확인
        protected Dictionary<int, PcInstance> pcDic;            // 현재 매칭된 유저가 들어가 있는 객체
        protected int matchUserWaitTime;              // 유저의 행동을 기다린 시간
        protected Define.MATCH_STATE matchState;      // 현재 매치 상태
        protected int matchRound;                     // 현재 진행중인 매치 라운드
        protected Dictionary<int, int> userWinCountDic;// 각 유저가 승리한 카운트
        protected Dictionary<int, int> roundResponseDic;     // 데미지 게산을 위한 임시 보관소
        protected Dictionary<int,  float> eloDic;   // elo probability
        protected MatchSystem matchSystem;
        public Match(MatchSystem matchSystem)
        {
            this.matchSystem = matchSystem;
            sendMatchSuccessMessage = false;
            matchUserWaitTime = 0;
            matchRound = 0;
            pcDic = new Dictionary<int, PcInstance>();
            userWinCountDic = new Dictionary<int, int>();
            matchState = Define.MATCH_STATE.MATCH_READY;
            roundResponseDic = new Dictionary<int, int>();
            eloDic = new Dictionary<int, float>();
        }
        /// <summary>
        /// 매칭에 참여할 유저 추가
        /// </summary>
        public void AddPcInstance(PcInstance pcInstance)
        {
            int teamNo = userWinCountDic.Count + 1;
            pcInstance.GetPcPvp().SetTeamNo(teamNo);
            pcInstance.GetPcPvp().SetMatchAccept(false);
            pcInstance.GetPcPvp().SetMatch(this);
            userWinCountDic.Add(teamNo, 0);
            pcDic.Add(teamNo, pcInstance);
        }
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
            if (!sendMatchSuccessMessage)
            {
                sendMatchSuccessMessage = true;
                SendMatchSuccess();
                return Define.MATCH_READY_STATE.MATCH_START_BEFORE_WAIT;
            }
            int acceptCount = 0;    // 매칭 수락을 한 유저 수
            foreach (KeyValuePair<int, PcInstance> pc in pcDic)
            {
                if (pc.Value.GetPcPvp().GetMatchAccept())
                {
                    acceptCount++;
                }
            }

            if (acceptCount == pcDic.Count) // 유저 리스트 수와 매칭 수락한 유저 수가 같은 경우
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
        /// <summary>
        /// 매칭이 성사 되었음을 클라이언트에 보냄
        /// </summary>
        private void SendMatchSuccess()
        {
            foreach (KeyValuePair<int, PcInstance> pc in pcDic)
            {
                S_MatchingSuccess.MatchingSuccessS(pc.Value);
            }
        }
        /// <summary>
        /// 매칭 시작에 대한 결과를 클라이언트에 보냄
        /// </summary>
        public void SendMatchStartResult(Define.MATCH_READY_STATE result)
        {
            if (result == Define.MATCH_READY_STATE.MATCH_START_SUCCESS)
            {
                matchState = Define.MATCH_STATE.MATCH_START;
                matchUserWaitTime = 0;
                ThreadStart(Define.MATCH_SYSTEM_DELAY_TIME);
                foreach (KeyValuePair<int, PcInstance> pc in pcDic)
                {
                    S_MatchingResponse.MatchingReponseS(pc.Value, 0);
                }
            }
            else
            {
                foreach (KeyValuePair<int, PcInstance> pc in pcDic)
                {
                    S_MatchingResponse.MatchingReponseS(pc.Value, 1);
                    if (pc.Value.GetPcPvp().GetMatchAccept())
                    {
                        // 수락을 한 유저는 실패를 했지만 다시 매칭을 이어갈 수 있도록 넣어줌
                        matchSystem.AddPcInsatnce(pc.Value);
                    }
                    else
                    {
                        matchSystem.RemovePcInstance(pc.Value);
                    }
                }
                matchSystem.RemoveReadyMatchList(this);
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
        protected void SendRoundResult(Define.ROCK_SCISSOR_PAPER winUserResult)
        {
            foreach (KeyValuePair<int, PcInstance> pc in pcDic)
            {
                S_RoundResult.RoundResultS(pc.Value, winUserResult);
            }
        }
        protected void SendBattleResponse()
        {
            foreach (KeyValuePair<int, PcInstance> pc in pcDic)
            {
                int teamNo = pc.Value.GetPcPvp().GetTeamNo();
                foreach(KeyValuePair<int, int> response in roundResponseDic)
                {
                    if(teamNo != response.Key)
                    {
                        S_UserBattleResponse.UserBattleResponseS(pc.Value, response.Value);
                    }
                }
            }
        }
        /// <summary>
        /// 각 유저의 데미지 설정
        /// </summary>
        /// <param name="teamNo"></param>
        /// <param name="dmg"></param>
        public virtual void SetDmg(int teamNo, int response)
        {
            if (!roundResponseDic.ContainsKey(teamNo))
            {
                roundResponseDic.Add(teamNo, response);
            }
        }
        /// <summary>
        /// 유저 연결 끊겼을 경우 처리
        /// </summary>
        /// <param name="pc"></param>
        public void UserDisconnect(PcInstance pc)
        {
            foreach (KeyValuePair<int, int> teamNo in userWinCountDic)
            {
                if (pc.GetPcPvp().GetTeamNo() == teamNo.Key)
                {
                    userWinCountDic[teamNo.Key] = 0;
                }
                else
                {
                    userWinCountDic[teamNo.Key] = 10;
                }
            }
            EndGame();
        }
        /// <summary>
        /// 게임 종료시 호출된 메서드
        /// </summary>
        protected virtual void EndGame()
        {
            // 승리한 유저 설정
            List<int> winTeamNos = new List<int>();
            int winCount = -1;
            foreach(KeyValuePair<int, int> teamNo in userWinCountDic)
            {
                if(winCount < teamNo.Value)
                {
                    winCount = teamNo.Value;
                    winTeamNos.Clear();
                    winTeamNos.Add(teamNo.Key);
                }
                else if(winCount == teamNo.Value)
                {
                    winTeamNos.Add(teamNo.Key);
                }
            }

            // 결과 각 유저에게 전송
            foreach (KeyValuePair<int, PcInstance> pc in pcDic)
            {
                if (winTeamNos.Contains(pc.Value.GetPcPvp().GetTeamNo()))
                {
                    pc.Value.GetPcPvp().SetRating(pc.Value.GetPcPvp().GetRating() + (int)(Define.K_FACTOR * (1- (eloDic[pc.Key]))));
                    S_MatchingResult.MatchingResultS(pc.Value, true);
                }
                else
                {
                    pc.Value.GetPcPvp().SetRating(pc.Value.GetPcPvp().GetRating() + (int)(Define.K_FACTOR * (0 - (eloDic[pc.Key]))));
                    S_MatchingResult.MatchingResultS(pc.Value, false);
                }
                pc.Value.GetPcPvp().SetMatch(null);
            }
            PcPvpSql.UpdateUserPvpSql(pcDic.Values.ToArray());
            matchSystem.RemoveReadyMatchList(this);

            // 스레드 종료
            ThreadManager.GetInstance().RemoveWorker(this);
        }

        /// <summary>
        /// 등록된 모든 유저의 elo 평점 계산
        /// </summary>
        public void CalculateEloRating()
        {
            MatchUtil.SetPvpEloRating(pcDic, ref eloDic);
        }
    }
}
