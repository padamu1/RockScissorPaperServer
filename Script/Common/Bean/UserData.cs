using SimulFactory.Common.Instance;
using SimulFactory.Core.Sql;
using SimulFactory.Game.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            if (changeName.Length > 10)
            {
                pc.SendPacket(S_UserName.Data(pc, 3));
                return;
            }
            changeName = changeName.ToUpper();
            if (UserDBSql.GetUserNoByName(changeName) == -1)
            {
                string idChecker = Regex.Replace(changeName, @"[^a-zA-Z0-9가-힣\.*,]", "", RegexOptions.Singleline);
                if(changeName == idChecker)
                {
                    if (UserDBSql.UpdateUserSql(pc, changeName))
                    {
                        this.UserName = changeName;
                        pc.SendPacket(S_UserName.Data(pc, 0));
                        return;
                    }
                }
                else
                {
                    pc.SendPacket(S_UserName.Data(pc, 2));
                    return;
                }
            }
            else
            {
                // 이미 존재하는 이름
                pc.SendPacket(S_UserName.Data(pc, 1));
            }
        }
    }
}
