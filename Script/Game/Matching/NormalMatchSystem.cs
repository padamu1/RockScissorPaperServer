﻿using RockScissorPaperServer.AutoBattle;
using RockScissorPaperServer.AutoBattle.Common.Base;
using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using SimulFactory.Game.Matching.Mode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Matching
{
    public class NormalMatchSystem : MatchSystem
    {
        static readonly Lazy<NormalMatchSystem> instanceHolder = new Lazy<NormalMatchSystem>(() => new NormalMatchSystem());
        public static NormalMatchSystem GetInstance()
        {
            return instanceHolder.Value;
        }
        public NormalMatchSystem()
        {
            this.defaultSearchCount = Define.NORMAL_MODE_SEARCH_USER_COUNT;
            this.minSearchCount = Define.NORMAL_MODE_SEARCH_USER_MIN_COUNT;
        }
        protected override void CheckSearchUser(int searchCount)
        {
            // 매칭 전 정렬
            matchSearchList.OrderBy(x => x.GetPcPvp().GetNormalPvpDto().Rating);

            if(matchSearchList.Count == 1)
            {
                NoSearchUser(matchSearchList[0]);
            }
            else
            {
                // 실제 로직 처리
                for (int count = 0; count < matchSearchList.Count - (searchCount - 1);)
                {
                    //bool matchSuccess = false;
                    if (matchSearchList[count + (searchCount - 1)].GetPcPvp().GetNormalPvpDto().Rating - matchSearchList[count].GetPcPvp().GetNormalPvpDto().Rating <= Define.DEFAULT_SEARCH_RATING + matchSearchList[count].GetPcPvp().GetWaitCount() * Define.INCREASE_SEARCH_RATING)
                    {
                        NormalMatch match = new NormalMatch(this);
                        for (int index = count; index < count + searchCount; index++)
                        {
                            match.AddPcInstance(matchSearchList[index]);
                            RemovePcInstance(matchSearchList[index]);
                        }
                        match.CalculateEloRating();
                        count += searchCount;
                    }
                    else
                    {
                        NoSearchUser(matchSearchList[count]);
                        count++;
                    }
                }
            }

        }
        protected override void NoSearchUser(PcInstance pc)
        {
            base.NoSearchUser(pc);
            Console.WriteLine("{0} user Wait Count : {1}", pc.GetUserData().UserName, pc.GetPcPvp().GetWaitCount());
            if (pc.GetPcPvp().GetWaitCount() > 15)
            {
                AIModule ai = AutoBattleManager.GetInstance().SpawnAI();
                Console.WriteLine("AI Module Spawn");
                ai.SetRating(pc.GetPcPvp().GetNormalPvpDto().Rating);
                AddPcInsatnce(ai);
            }
        }
    }
}
