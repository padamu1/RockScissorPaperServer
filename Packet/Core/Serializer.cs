using System.Text;
using System.Text.Json;

namespace RockScissorPaperServer.Packet.Core
{
    public static class Serializer
    {
        public static byte[] EMPTY_BYTES = new byte[0];
        public enum TypeCode
        {
            Int = 0,
            Long = 1,
            Short = 2,
            Bool = 3,
            String = 4,
            Char = 5,
            Double = 6,
            List = 8,
            Objects = 9,
            Dictionary = 10
        }
        public static Dictionary<Type, TypeCode> TYPE_DICT = new Dictionary<Type, TypeCode>()
        {
            {typeof(int), TypeCode.Int },
            {typeof(long), TypeCode.Long },
            {typeof(short), TypeCode.Short },
            {typeof(bool), TypeCode.Bool },
            {typeof(string), TypeCode.String },
            {typeof(string), TypeCode.Char },
            {typeof(double), TypeCode.Double },
            {typeof(List<object>), TypeCode.List },
            {typeof(object[]), TypeCode.Objects },
            {typeof(Dictionary<byte,object>), TypeCode.Dictionary },
        };

        public static byte[] GetBytes(object value)
        {
            switch (TYPE_DICT[value.GetType()])
            {
                case TypeCode.Int:
                    return IntToBytes((int)value);
                case TypeCode.Long:
                    return LongToBytes((long)value);
                case TypeCode.Short:
                    return ShortToBytes((short)value);
                case TypeCode.Bool:
                    return BoolToBytes((bool)value);
                case TypeCode.String:
                    return StringToBytes((string)value);
                case TypeCode.Char:
                    return CharToBytes((char)value);
                case TypeCode.Double:
                    return DoubleToBytes((long)value);
                case TypeCode.List:
                    return ListToBytes((List<object>)value);
                case TypeCode.Objects:
                    return ObjectToBytes((object[])value);
                case TypeCode.Dictionary:
                    return DictionaryToBytes((Dictionary<byte, object>)value);
                default:
                    return EMPTY_BYTES;
            }
        }
        #region Default Type <-> Bytes 
        public static byte[] IntToBytes(int value) => BitConverter.GetBytes(value);
        public static int BytesToInt(byte[] value) => BitConverter.ToInt32(value);
        public static byte[] LongToBytes(long value) => BitConverter.GetBytes(value);
        public static long BytesToLong(byte[] value) => BitConverter.ToInt64(value);
        public static byte[] ShortToBytes(short value) => BitConverter.GetBytes(value);
        public static short BytesToShort(byte[] value) => BitConverter.ToInt16(value);
        public static byte[] BoolToBytes(bool value) => BitConverter.GetBytes(value);
        public static bool BytesToBool(byte[] value) => BitConverter.ToBoolean(value);
        public static byte[] StringToBytes(string value) => Encoding.UTF8.GetBytes((value));
        public static string BytesToString(byte[] value) => Encoding.UTF8.GetString(value);
        public static byte[] CharToBytes(char value) => BitConverter.GetBytes((value));
        public static char BytesToChar(byte[] value) => BitConverter.ToChar((value));
        public static byte[] DoubleToBytes(double value) => BitConverter.GetBytes(value);
        public static double BytesToDouble(byte[] value) => BitConverter.ToDouble((value)); 
        #endregion
        public static byte[] ListToBytes(List<object> value)
        {

            return EMPTY_BYTES;
        }
        public static byte[] ObjectToBytes(object[] value)
        {
            return EMPTY_BYTES;
        }
        public static byte[] DictionaryToBytes(Dictionary<byte, object> value)
        {
            return EMPTY_BYTES;
        }
    }
}
