using RockScissorPaperServer.AutoBattle.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaperServer.AutoBattle
{
    public class AutoBattleManager
    {
        static readonly Lazy<AutoBattleManager> instanceHolder = new Lazy<AutoBattleManager>(() => new AutoBattleManager());
        public static AutoBattleManager GetInstance()
        {
            return instanceHolder.Value;
        }

        private Queue<AIModule> aIModules;
        
        public AutoBattleManager()
        {
            aIModules = new Queue<AIModule>();
        }
        /// <summary>
        /// AI 개수가 모자랄 경우 설정
        /// </summary>
        private void AddAI()
        {
            AIModule aIModule = new AIModule(null);
            aIModule.SetDefaultData();
            aIModules.Enqueue(aIModule);
        }
        public AIModule SpawnAI()
        {
            if(aIModules.Count == 0)
            {
                AddAI();
            }
            return aIModules.Dequeue();
        }
        public void ReturnAIModule(AIModule aIModule)
        {
            aIModules.Enqueue(aIModule);
        }
    }
}
