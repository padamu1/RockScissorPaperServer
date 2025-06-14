﻿using RockScissorPaperServer.Script.Common.Dto;
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
        private NormalPvpDto normalPvpDto;
        private MultiPvpDto multiPvpDto;
        private int matchId;
        private int cardNo;
        private bool matchAccept;
        private int teamNo;
        private int waitCount;
        private PcInstance pc;
        private Match? match;
        public PcPvp(PcInstance pc)
        {
            this.pc = pc;
            normalPvpDto = new NormalPvpDto(Define.INIT_RATING);
            multiPvpDto = new MultiPvpDto();
        }
        public void SetWaitCount(int waitCount)
        {
            this.waitCount = waitCount;
        }
        public int GetWaitCount()
        {
            return waitCount;
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
        public NormalPvpDto GetNormalPvpDto()
        {
            return normalPvpDto;
        }
        public MultiPvpDto GetMultiPvpDto()
        {
            return multiPvpDto;
        }
    }
}
