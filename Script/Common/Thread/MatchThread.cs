using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using SimulFactory.Core.Base;
using SimulFactory.Game.Matching;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Common.Thread
{
    using SimulFactory.Game.Event;
    using System.Threading;
    public class MatchThread : Match
    {
        private bool threadRun;
        public MatchThread():base()
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
        protected virtual void GameRun()
        {
            if (matchRound >= Define.MAX_ROUND_COUNT)
            {
                EndGame();
            }
            else
            {
                if (matchUserWaitTime++ > Define.MATCH_USER_WAIT_TIME)
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
        protected virtual int CheckRoundResult()
        {
            return 0;
        }
        /// <summary>
        /// 라운드 종료시 동작 메서드 - winTeamNo 가 0이 들어온 경우 다음 라운드로 진행 안함, -1이 들어온 경우 플레이어 누군가가 제출을 안함
        /// </summary>
        /// <param name="winTeamNo"></param>
        protected virtual void EndRound(int winTeamNo = 0)
        {
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
