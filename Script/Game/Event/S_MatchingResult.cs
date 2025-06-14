using RockScissorPaperServer.PacketSerializer.Model;
using RockScissorPaperServer.Script.Common.Dto;
using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using SimulFactory.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Event
{
    public class S_MatchingResult
    {
        public static PacketData Data(bool isWin, PvpDtoBase pvpDto, int type)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            if(isWin)
            {
                param.Add(0, true);
            }
            else
            {
                param.Add(0, false);
            }
            param.Add(1,pvpDto.Rating);
            param.Add(2,pvpDto.WinCount);
            param.Add(3,pvpDto.DefeatCount);
            param.Add(4, type);

            return new PacketData((byte)Define.EVENT_CODE.MatchingResultS, param);
        }
    }
}
