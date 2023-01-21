using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Packet.Model
{
    public class PacketData
    {
        public int PacketId { get; set; }
        public Dictionary<byte,object> Data { get; set; }
    }
}
