using SimulFactory.Common.Bean;
using SimulFactory.Common.Thread;
using SimulFactory.Game.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Matching
{
    public class NormalMatch : MatchThread
    {
        public NormalMatch() : base()
        { 
        
        }

        protected override int CheckRoundResult()
        {
            if (roundResponseDic.Count == 2)
            {
                int winTeamNo = 1;

                switch ((Define.ROCK_SCISSOR_PAPER)roundResponseDic[1])
                {
                    case Define.ROCK_SCISSOR_PAPER.Rock:
                        switch ((Define.ROCK_SCISSOR_PAPER)roundResponseDic[2])
                        {
                            case Define.ROCK_SCISSOR_PAPER.Rock:
                                winTeamNo = 3;
                                break;
                            case Define.ROCK_SCISSOR_PAPER.Scissor:
                                winTeamNo = 1;
                                break;
                            case Define.ROCK_SCISSOR_PAPER.Paper:
                                winTeamNo = 2;
                                break;
                        }
                        break;
                    case Define.ROCK_SCISSOR_PAPER.Scissor:
                        switch ((Define.ROCK_SCISSOR_PAPER)roundResponseDic[2])
                        {
                            case Define.ROCK_SCISSOR_PAPER.Rock:
                                winTeamNo = 2;
                                break;
                            case Define.ROCK_SCISSOR_PAPER.Scissor:
                                winTeamNo = 3;
                                break;
                            case Define.ROCK_SCISSOR_PAPER.Paper:
                                winTeamNo = 1;
                                break;
                        }
                        break;
                    case Define.ROCK_SCISSOR_PAPER.Paper:
                        switch ((Define.ROCK_SCISSOR_PAPER)roundResponseDic[2])
                        {
                            case Define.ROCK_SCISSOR_PAPER.Rock:
                                winTeamNo = 1;
                                break;
                            case Define.ROCK_SCISSOR_PAPER.Scissor:
                                winTeamNo = 2;
                                break;
                            case Define.ROCK_SCISSOR_PAPER.Paper:
                                winTeamNo = 3;
                                break;
                        }
                        break;
                }
                // 데미지 비교
                if (roundResponseDic[1] < roundResponseDic[2])
                {
                }
                return winTeamNo;
            }
            return 0;
        }
        protected override void EndRound(int winTeamNo = 0)
        {
            base.EndRound(winTeamNo);
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
                S_UserBattleResponse.UserBattleResponseS(pcList[0], roundResponseDic[pcList[1].GetPcPvp().GetTeamNo()]);
                S_UserBattleResponse.UserBattleResponseS(pcList[1], roundResponseDic[pcList[0].GetPcPvp().GetTeamNo()]);
            }
            else
            {
                Console.WriteLine("한명 승리");
                userWinCountDic[winTeamNo]++;
                S_UserBattleResponse.UserBattleResponseS(pcList[0], roundResponseDic[pcList[1].GetPcPvp().GetTeamNo()]);
                S_UserBattleResponse.UserBattleResponseS(pcList[1], roundResponseDic[pcList[0].GetPcPvp().GetTeamNo()]);
            }
            // 매치 라운드 증가
            matchRound++;
            // 다음 계산을 위해 초기화
            roundResponseDic.Clear();
            SendRoundResult(winTeamNo);
        }
    }
}
