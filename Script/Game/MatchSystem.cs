using SimulFactory.Common.Instance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game
{
    public class MatchSystem
    {
        private static MatchSystem? Instance = null;
        public static MatchSystem GetInstance()
        {
            if(Instance == null)
            {
                Instance = new MatchSystem();
            }
            return Instance;
        }
        private List<PcInstance> pcInstances = new List<PcInstance>();
        private List<PcInstance> addPcInstances = new List<PcInstance>();
        private List<PcInstance> removedPcInstances = new List<PcInstance>();
        public void Matching()
        {
            while(true)
            {
                lock (addPcInstances)
                {
                    foreach(PcInstance instance in addPcInstances)
                    {
                        pcInstances.Add(instance);
                    }
                    addPcInstances.Clear();
                }
                lock (addPcInstances)
                {
                    foreach (PcInstance instance in removedPcInstances)
                    {
                        pcInstances.Remove(instance);
                    }
                    removedPcInstances.Clear();
                }
                // 실제 로직 처리
                foreach (PcInstance pcInstance in pcInstances)
                {
                    
                }
                Console.WriteLine(pcInstances.Count);
                Thread.Sleep(1000); // 1초간 멈춤
            }
        }
        public void AddPcInsatnce(PcInstance pcInstance)
        {
            lock(addPcInstances)
            {
                addPcInstances.Add(pcInstance);
            }
        }
        public void RemovePcInstance(PcInstance pcInstance)
        {
            lock(removedPcInstances)
            {
                removedPcInstances.Add(pcInstance);
            }
        }
    }
}
