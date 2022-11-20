using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Common.Dto
{
    public class FriendDto
    {
        // 아래 두개 정보만 내려줌
        public string FirendName { get; set; }
        public long ConnectionTime { get; set; }
        // 친구 이름을 기준으로 알아낼 수 있는 정보
        public long FriendNo { get; set; }
    }
}
