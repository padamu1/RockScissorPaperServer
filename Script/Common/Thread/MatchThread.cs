using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using SimulFactory.Core.Base;
using SimulFactory.Game.Matching;

namespace SimulFactory.Common.Thread
{
    using SimulFactory.Core.Util;
    using SimulFactory.Game;
    using SimulFactory.Game.Event;
    using System.Threading;
    public class MatchThread : Match
    {
        private bool threadRun;
        public MatchThread(MatchSystem matchSystem) :base(matchSystem)
        {

        }
        protected override void ThreadAction()
        {
            threadRun = true;
            while (threadRun)
            {
                GameRun();
                Thread.Sleep(delayTime);
            }
        }
        protected void GameRun()
        {
            switch (gameState)
            {
                case Define.GAME_STATE.USER_RESULT_RECEIVE:
                    EndRound(CheckRoundResult());
                    break;
                case Define.GAME_STATE.ROUNT_RESULT:
                    SetRoundResult();
                    break;
                case Define.GAME_STATE.EndGame:
                    EndGame();
                    break;
            }
            matchUserWaitTime += Define.MATCH_USER_RESULT_WAIT_DELAY;
            if (matchUserWaitTime > Define.MATCH_USER_RESULT_WAIT_COUNT)
            {
                EndRound(Define.ROCK_SCISSOR_PAPER.Break);
                return;
            }
        }

        /// <summary>
        /// 라운드의 결과를 확인 0 : 결과 제출이 안됨, 1 : 1p 승리, 2 : 2p 승리
        /// </summary>
        /// <returns>승리한 유저 넘버</returns>
        protected virtual Define.ROCK_SCISSOR_PAPER CheckRoundResult()
        {
            if (roundResponseDic.Count == pcDic.Count)
            {
                return MatchUtil.GetRSPResultV2(roundResponseDic);
            }
            return Define.ROCK_SCISSOR_PAPER.None;
        }
        /// <summary>
        /// 라운드 종료시 동작 메서드 - winTeamNo 가 0이 들어온 경우 다음 라운드로 진행 안함, -1이 들어온 경우 플레이어 누군가가 제출을 안함
        /// </summary>
        /// <param name="winTeamNo"></param>
        protected virtual void EndRound(Define.ROCK_SCISSOR_PAPER winUserResult)
        {
            Console.WriteLine("matching reponse check");
            if (winUserResult == Define.ROCK_SCISSOR_PAPER.None)
            {
                // 다음 라운드 진행 안함
                return;
            }
            else if (winUserResult == Define.ROCK_SCISSOR_PAPER.Break)
            {
                Console.WriteLine("some client connection close");

                foreach (KeyValuePair<int, PcInstance> pc in pcDic)
                {
                    int teamNo = pc.Value.GetPcPvp().GetTeamNo();
                    if (roundResponseDic.ContainsKey(teamNo))
                    {
                        userWinCountDic[teamNo] = Define.MAX_WIN_COUNT;
                    }
                }
                EndGame();
                return;
            }
            maxWinCount = 0;
            foreach (KeyValuePair<int, PcInstance> pc in pcDic)
            {
                int teamNo = pc.Value.GetPcPvp().GetTeamNo();
                if ((Define.ROCK_SCISSOR_PAPER)roundResponseDic[teamNo] == winUserResult)
                {
                    userWinCountDic[teamNo]++;
                    if (userWinCountDic[teamNo] > maxWinCount)
                    {
                        maxWinCount = userWinCountDic[teamNo];
                    }
                }
            }
            SendBattleResponse();
            this.winUserResult = (int)winUserResult;
            gameState = Define.GAME_STATE.ROUNT_RESULT;
        }
        public virtual void SetRoundResult()
        {
            Dictionary<int, int>? roundResponseDicCopied = new Dictionary<int, int>(roundResponseDic);
            // 다음 계산을 위해 초기화
            roundResponseDic.Clear(); 
            SendRoundResult(winUserResult, roundResponseDicCopied);

            if (maxWinCount == Define.MAX_WIN_COUNT)
            {
                gameState = Define.GAME_STATE.EndGame;
                return;
            }
            if (winUserResult != (int)Define.ROCK_SCISSOR_PAPER.None)
            {
                matchRound++;
                matchUserWaitTime = 0;
            }
            gameState = Define.GAME_STATE.USER_RESULT_RECEIVE;
        }

        public override void Dispose()
        {
            threadRun = false;
            base.Dispose();
        }
        public override ThreadBase Clone()
        {
            return this;
        }
        public override string GetThreadName()
        {
            return "MatchTread";
        }
    }
}
