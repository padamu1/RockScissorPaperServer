using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Common.Instance
{
    public class UserData
    {
        public static UserData? Instance = null;
        public static UserData GetInstance()
        {
            if(Instance == null)
            {
                Instance = new UserData();
            }
            return Instance;
        }

        public string UserName { get; set; }
        public string UserId { get; set; }
    }
}
