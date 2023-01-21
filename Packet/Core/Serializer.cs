using System.Text;
using System.Text.Json;

namespace SimulFactory.Packet.Core
{
    public class TypeCode
    {
        public const short Int = 0;
        public const short Long = 1;
        public const short Short = 2;
        public const short Bool = 3;
        public const short String = 4;
        public const short Char = 5;
        public const short Double = 6;
        public const short List = 8;
        public const short Objects = 9;
        public const short Dictionary = 10;
    }
    public static class Serializer
    {
        public static Dictionary<Type, short> TYPE_DICT = new Dictionary<Type, short>()
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
        public static Dictionary<Type, byte[]> TYPE_BYTE_DICT = new Dictionary<Type, byte[]>()
        {
            {typeof(int),  new byte[] { 0, 0 } },
            {typeof(long), new byte[] { 1, 0 } },
            {typeof(short), new byte[] { 2, 0 } },
            {typeof(bool), new byte[] { 3, 0 } },
            {typeof(string), new byte[] { 4, 0 } },
            {typeof(string), new byte[] { 5, 0 } },
            {typeof(double), new byte[] { 6, 0 } },
            {typeof(List<object>), new byte[] { 7, 0 } },
            {typeof(object[]), new byte[] { 8, 0 } },
            {typeof(Dictionary<byte,object>), new byte[] { 9, 0 } },
        };

        public static byte[] GetBytes(object value)
        {
            Type objectType = value.GetType();
            byte[] data;
            switch (TYPE_DICT[objectType])
            {
                case TypeCode.Int:
                    data = IntToBytes((int)value);
                    break;
                case TypeCode.Long:
                    data = LongToBytes((long)value);
                    break;
                case TypeCode.Short:
                    data = ShortToBytes((short)value);
                    break;
                case TypeCode.Bool:
                    data = BoolToBytes((bool)value);
                    break;
                case TypeCode.String:
                    data = StringToBytes((string)value);
                    break;
                case TypeCode.Char:
                    data = CharToBytes((char)value);
                    break;
                case TypeCode.Double:
                    data = DoubleToBytes((long)value);
                    break;
                case TypeCode.List:
                    data = ListToBytes((List<object>)value);
                    break;
                case TypeCode.Objects:
                    data = ObjectToBytes((object[])value);
                    break;
                case TypeCode.Dictionary:
                    data = DictionaryToBytes((Dictionary<byte, object>)value);
                    break;
                default:
                    data = Config.EMPTY_BYTES;
                    return data;
            }
            byte[] type = TYPE_BYTE_DICT[objectType];
            byte[] length = BitConverter.GetBytes(data.Length);
            byte[] result = new byte[Config.LENGTH_SIZE + Config.TYPE_SIZE + data.Length];
            Array.Copy(length, 0, result, 0, Config.LENGTH_SIZE);
            Array.Copy(type, 0, result, Config.LENGTH_SIZE, Config.TYPE_SIZE);
            Array.Copy(data, 0, result, Config.DATA_START_INDEX, data.Length);
            return result;
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
            byte[] bytes = new byte[0];
            for (int count = 0; count < value.Count; count++)
            {
                byte[] currentBytes = bytes;
                byte[] newResult = GetBytes(value[count]);
                bytes = new byte[currentBytes.Length + newResult.Length];
                Array.Copy(currentBytes, 0, bytes, 0, currentBytes.Length);
                Array.Copy(newResult, 0, bytes, currentBytes.Length, newResult.Length);
            }
            return bytes;
        }
        public static List<object> BytesToList(byte[] value)
        {
            List<object> result = new List<object>();
            int nextIdx = 0;
            byte[] lengthBytes = new byte[Config.LENGTH_SIZE];
            byte[] typeBytes = new byte[Config.TYPE_SIZE];
            while (nextIdx < value.Length)
            {
                Array.Copy(value, nextIdx, lengthBytes, 0, Config.LENGTH_SIZE);
                int length = BytesToInt(lengthBytes);
                nextIdx += Config.LENGTH_SIZE;
                Array.Copy(value, nextIdx, typeBytes, 0, Config.TYPE_SIZE);
                short type = BytesToShort(typeBytes);
                nextIdx += Config.TYPE_SIZE;
                byte[] data = new byte[length];
                Array.Copy(value, nextIdx, data, 0, length);
                //result.Add(GetValue(type, data));
                nextIdx += length;
            }
            return result;
        }
        public static byte[] ObjectToBytes(object[] value)
        {
            return Config.EMPTY_BYTES;
        }
        public static byte[] DictionaryToBytes(Dictionary<byte, object> value)
        {
            return Config.EMPTY_BYTES;
        }
    }
}
