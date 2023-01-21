using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Packet.Core
{
    public class Config
    {
        public static int LENGTH_SIZE = 4;
        public static int TYPE_SIZE = 2;
        public static int DATA_START_INDEX = 6;
        public static byte[] EMPTY_BYTES = new byte[0];
    }
}
