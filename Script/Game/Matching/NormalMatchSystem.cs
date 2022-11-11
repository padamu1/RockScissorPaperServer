using SimulFactory.Common.Bean;
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
            base.CheckSearchUser(searchCount);
        }
    }
}
