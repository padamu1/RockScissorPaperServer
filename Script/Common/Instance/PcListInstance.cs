using SimulFactory.Core.Util;
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
        private Dictionary<long, PcInstance> pcDic;
        public PcListInstance()
        {
            pcDic = new Dictionary<long,PcInstance>();
        }
        public void AddInstance(PcInstance pc)
        {
            lock(pcDic)
            {
                pcDic.Add(pc.GetUserData().UserNo, pc);
                Console.WriteLine(pc.GetUserData().UserName + " Client Connected");
            }
        }
        public void RemoveInstance(long userNo)
        {
            lock (pcDic)
            {
                if (pcDic.ContainsKey(userNo))
                {
                    pcDic[userNo].Dispose();
                    pcDic.Remove(userNo);
                }
            }
        }
        /// <summary>
        /// 현재 접속중인 유저와 비교해 접속 가능한지 확인 - 임시
        /// </summary>
        /// <returns></returns>
        public bool CheckUser(long userNo)
        {
            lock(pcDic)
            {
                foreach(KeyValuePair<long,PcInstance> pcData  in pcDic)
                {
                    if(pcData.Key == userNo)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        public PcInstance GetPcInstance(long userNo)
        {
            lock(pcDic)
            {
                if(pcDic.ContainsKey(userNo))
                {
                    return pcDic[userNo];
                }
                return null;
            }
        }
        public void SendPacket(EventData eventData)
        {
            lock(pcDic)
            {
                foreach(PcInstance pc in pcDic.Values)
                {
                    pc.SendPacket(eventData);
                }
            }
        }
    }
}
