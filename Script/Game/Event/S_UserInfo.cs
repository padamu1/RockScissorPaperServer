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
    public class S_UserInfo
    {
        public static PacketData Data(PcInstance pc)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            // 유저 이름
            param.Add(0, pc.GetUserData().UserName);
            // 노말 매치
            NormalPvpDto normalPvpDto = pc.GetPcPvp().GetNormalPvpDto();
            param.Add(1, normalPvpDto.Rating);
            param.Add(2, normalPvpDto.WinCount);
            param.Add(3, normalPvpDto.DefeatCount);
            // 멀티 매치
            MultiPvpDto multiPvpDto = pc.GetPcPvp().GetMultiPvpDto();
            param.Add(4, multiPvpDto.WinCount);
            param.Add(5, multiPvpDto.DefeatCount);
            return new PacketData((byte)Define.EVENT_CODE.UserInfoS, param);
        }
    }
}
