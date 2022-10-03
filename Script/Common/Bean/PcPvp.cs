using SimulFactory.Common.Instance;
using SimulFactory.Game.Matching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Common.Bean
{
    public class PcPvp
    {
        private int rating;
        private int winCount;
        private int defeatCount;
        private int matchId;
        private int cardNo;
        private bool matchAccept;
        private int teamNo;
        private PcInstance pc;
        private Match? match;
        public PcPvp(PcInstance pc)
        {
            this.pc = pc;
            rating = Define.INIT_RATING;
        }
        public void SetRating(int rating)
        {
            this.rating = rating;
        }
        public int GetRating()
        {
            return rating;
        }
        public void SetWinCount(int winCount)
        {
            this.winCount = winCount;
        }
        public int GetWinCount()
        {
            return winCount;
        }
        public void SetDefeatCount(int defeatCount)
        {
            this.defeatCount = defeatCount;
        }
        public int GetDefeatCount()
        {
            return defeatCount;
        }
        public void SetMatchId(int matchId)
        {
            this.matchId = matchId;
        }
        public int GetMatchId()
        {
            return matchId;
        }
        public void SetCardNo(int cardNo)
        {
            this.cardNo = cardNo;
        }
        public int GetCardNo()
        {
            return cardNo;
        }
        public void SetMatchAccept(bool matchAccept)
        {
            this.matchAccept = matchAccept;
        }
        public bool GetMatchAccept()
        {
            return matchAccept;
        }
        public void SetTeamNo(int teamNo)
        {
            this.teamNo = teamNo;
        }
        public int GetTeamNo()
        {
            return teamNo;
        }
        public void UpdateMatchResult(bool isWin, int rating)
        {
            if(isWin)
            {
                winCount++;
            }
            else
            {
                defeatCount++;
            }
            this.rating += rating;
        }
        public void SetMatch(Match? match)
        {
            if(match == null)
            {
                this.match = null;
            }
            else
            {
                this.match = match;
            }
        }
        public Match GetMatch()
        {
            return match;
        }
    }
}
