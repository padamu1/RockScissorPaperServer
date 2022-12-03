using SimulFactory.Common.Bean;
using SimulFactory.Common.Dto;
using SimulFactory.Common.Instance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Event
{
    public class S_FriendData
    {
        public static void FriendDataS(PcInstance pc, List<FriendDto> friendDtoList)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            param.Add(0, friendDtoList);
            pc.SendPacket((byte)Define.EVENT_CODE.FriendDataS, param);
        }
    }
}
