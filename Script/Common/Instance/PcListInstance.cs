using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Common.Instance
{
    /// <summary>
    /// 전체 유저를 관리하기 위한 객체 - 아직 미사용
    /// </summary>
    public class PcListInstance
    {
        static readonly Lazy<PcListInstance> instanceHolder = new Lazy<PcListInstance>(() => new PcListInstance());
        public static PcListInstance GetInstance()
        {
            return instanceHolder.Value;
        }
        private Dictionary<string, PcInstance> pcDic;
        public PcListInstance()
        {
            pcDic = new Dictionary<string,PcInstance>();
        }
        public void AddInstance(PcInstance pc)
        {
            lock(pcDic)
            {
                if (pcDic.ContainsKey(pc.UserData.UserId))
                {
                }
                pcDic.Add(pc.UserData.UserId,pc);
            }
        }
        public void RemoveInstance(PcInstance pc)
        {
            lock (pcDic)
            {
                if (pcDic.ContainsKey(pc.UserData.UserId))
                {
                    pcDic.Remove(pc.UserData.UserId);
                }
            }
        }
        /// <summary>
        /// 현재 접속중인 유저와 비교해 접속 가능한지 확인 - 임시
        /// </summary>
        /// <returns></returns>
        public bool CheckUser(PcInstance pc)
        {
            lock(pcDic)
            {
                foreach(KeyValuePair<string,PcInstance> pcData  in pcDic)
                {
                    if(pcData.Key == pc.UserData.UserId)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
