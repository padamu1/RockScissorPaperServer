using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaperServer.Packet.Core
{
    public class Serializer
    {
        public byte[] GetBytes(int type, object value)
        {
            switch(type)
            {
                case 0:
                    return LongToBytes((long)value);
                default:
                    return new byte[0];
            }
        }

        public byte[] LongToBytes(long value) => BitConverter.GetBytes(value);
    }
}
