using SimulFactory.Common.Instance;
using SimulFactory.Core.Sql;
using SimulFactory.Game.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Common.Bean
{
    public class UserData
    {
        public int LoginType { get; set; }
        public string GoogleToken { get; set; }
        public long UserNo { get; set; }
        public string UserName { get; set; }
        public long PingTime { get; set; }
        private PcInstance pc;
        public UserData(PcInstance pc)
        {
            this.pc = pc;
        }
        public void ChangeUserName(string changeName)
        {
            if(UserDBSql.UpdateUserSql(pc, changeName))
            {
                this.UserName = changeName;
                S_UserName.UserNameS(pc, true);
                return;
            }
            S_UserName.UserNameS(pc, false);
        }
    }
}
