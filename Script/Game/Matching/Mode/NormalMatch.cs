using SimulFactory.Common.Thread;

namespace SimulFactory.Game.Matching.Mode
{
    public class NormalMatch : MatchThread
    {
        public NormalMatch(MatchSystem matchSystem) : base(matchSystem)
        {

        }
        /*
        protected override void EndRound(int winTeamNo = 0)
        {
            Console.WriteLine("매칭 결과 확인");
            if (winTeamNo == 0)
            {
                // 다음 라운드 진행 안함
                return;
            }
            else if (winTeamNo == -1)
            {
                Console.WriteLine("한 쪽 연결 끊김");
                // 제출을 한 유저만 승리 처리 - 인터넷 상태가 안좋은 유저의 경우 자동 제출이 안됐을 것으로 가정
                if (!roundResponseDic.ContainsKey(1))
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
            else if (winTeamNo == 3)
            {
                Console.WriteLine("무승부");
                // 두 플레이어 모두 승리 처리
                userWinCountDic[1]++;
                userWinCountDic[2]++;

                // 서로에게 상대방의 결과를 보냄
                S_UserBattleResponse.UserBattleResponseS(pcDic[1], roundResponseDic[pcDic[2].GetPcPvp().GetTeamNo()]);
                S_UserBattleResponse.UserBattleResponseS(pcDic[2], roundResponseDic[pcDic[1].GetPcPvp().GetTeamNo()]);
            }
            else
            {
                Console.WriteLine("한명 승리");
                userWinCountDic[winTeamNo]++;
                S_UserBattleResponse.UserBattleResponseS(pcDic[1], roundResponseDic[pcDic[2].GetPcPvp().GetTeamNo()]);
                S_UserBattleResponse.UserBattleResponseS(pcDic[2], roundResponseDic[pcDic[1].GetPcPvp().GetTeamNo()]);
            }
            // 다음 계산을 위해 초기화
            roundResponseDic.Clear();
            SendRoundResult(winTeamNo);

            base.EndRound(winTeamNo);
        }
        */
    }
}
