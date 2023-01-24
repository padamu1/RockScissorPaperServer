namespace RockScissorPaperServer.PacketSerializer.Model
{
    public class PacketData
    {
        public byte EvCode { get; set; }
        public Dictionary<byte, object> Data { get; set; }
        public PacketData(byte evCode, Dictionary<byte, object> data)
        {
            EvCode = evCode;
            Data = data;
        }
    }
}
