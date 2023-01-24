using RockScissorPaperServer.PacketSerializer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PacketSerializer
{
    public class Config
    {
        public static int LENGTH_SIZE = 4;
        public static int TYPE_SIZE = 2;
        public static int DATA_START_INDEX = 6;
        public static byte[] EMPTY_BYTES = new byte[0];
    }
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
        public const short Dictionary = 9;
        public const short PacketData = 10;
    }
    public static class Serializer
    {
        private static Dictionary<Type, short> TYPE_DICT = new Dictionary<Type, short>()
        {
            {typeof(int), TypeCode.Int },
            {typeof(long), TypeCode.Long },
            {typeof(short), TypeCode.Short },
            {typeof(bool), TypeCode.Bool },
            {typeof(string), TypeCode.String },
            {typeof(char), TypeCode.Char },
            {typeof(double), TypeCode.Double },
            {typeof(List<object>), TypeCode.List },
            {typeof(Dictionary<byte,object>), TypeCode.Dictionary },
            {typeof(PacketData), TypeCode.PacketData },
        };
        private static Dictionary<Type, byte[]> TYPE_BYTE_DICT = new Dictionary<Type, byte[]>()
        {
            {typeof(int),  new byte[] { 0, 0 } },
            {typeof(long), new byte[] { 1, 0 } },
            {typeof(short), new byte[] { 2, 0 } },
            {typeof(bool), new byte[] { 3, 0 } },
            {typeof(string), new byte[] { 4, 0 } },
            {typeof(char), new byte[] { 5, 0 } },
            {typeof(double), new byte[] { 6, 0 } },
            {typeof(List<object>), new byte[] { 8, 0 } },
            {typeof(Dictionary<byte,object>), new byte[] { 9, 0 } },
            {typeof(PacketData), new byte[] { 10, 0 } },
        };

        public static byte[] Serialize(object value)
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
                    data = DoubleToBytes((double)value);
                    break;
                case TypeCode.List:
                    data = ListToBytes((List<object>)value);
                    break;
                case TypeCode.Dictionary:
                    data = DictionaryToBytes((Dictionary<byte, object>)value);
                    break;
                case TypeCode.PacketData:
                    data = PacketDataToBytes((PacketData)value);
                    break;
                default:
                    data = Config.EMPTY_BYTES;
                    return data;
            }
            byte[] length = BitConverter.GetBytes(data.Length);
            byte[] type = TYPE_BYTE_DICT[objectType];
            byte[] result = new byte[Config.LENGTH_SIZE + Config.TYPE_SIZE + data.Length];
            Array.Copy(length, 0, result, 0, Config.LENGTH_SIZE);
            Array.Copy(type, 0, result, Config.LENGTH_SIZE, Config.TYPE_SIZE);
            Array.Copy(data, 0, result, Config.DATA_START_INDEX, data.Length);
            return result;
        }
        public static object Deserialize(byte[] value)
        {
            byte[] lengthBytes = new byte[Config.LENGTH_SIZE];
            Array.Copy(value, 0, lengthBytes, 0, Config.LENGTH_SIZE);
            int length = BytesToInt(lengthBytes);

            byte[] typeBytes = new byte[Config.TYPE_SIZE];
            Array.Copy(value, Config.LENGTH_SIZE, typeBytes, 0, Config.TYPE_SIZE);
            short type = BytesToShort(typeBytes);

            byte[] dataBytes = new byte[length];
            Array.Copy(value, Config.DATA_START_INDEX, dataBytes, 0, length);

            return TypeToValue(type, dataBytes);
        }
        private static object TypeToValue(int type, byte[] dataBytes)
        {
            object result = null;
            switch (type)
            {
                case TypeCode.Int:
                    result = BytesToInt(dataBytes);
                    break;
                case TypeCode.Long:
                    result = BytesToLong(dataBytes);
                    break;
                case TypeCode.Short:
                    result = BytesToShort(dataBytes);
                    break;
                case TypeCode.Bool:
                    result = BytesToBool(dataBytes);
                    break;
                case TypeCode.String:
                    result = BytesToString(dataBytes);
                    break;
                case TypeCode.Char:
                    result = BytesToChar(dataBytes);
                    break;
                case TypeCode.Double:
                    result = BytesToDouble(dataBytes);
                    break;
                case TypeCode.List:
                    result = BytesToList(dataBytes);
                    break;
                case TypeCode.Dictionary:
                    result = BytesToDictionary(dataBytes);
                    break;
                case TypeCode.PacketData:
                    result = BytesToPacketData(dataBytes);
                    break;
                default:
                    break;
            }
            return result;
        }
        #region Default Type <-> Bytes 
        private static byte[] IntToBytes(int value) => BitConverter.GetBytes(value);
        private static int BytesToInt(byte[] value) => BitConverter.ToInt32(value, 0);
        private static byte[] LongToBytes(long value) => BitConverter.GetBytes(value);
        private static long BytesToLong(byte[] value) => BitConverter.ToInt64(value, 0);
        private static byte[] ShortToBytes(short value) => BitConverter.GetBytes(value);
        private static short BytesToShort(byte[] value) => BitConverter.ToInt16(value, 0);
        private static byte[] BoolToBytes(bool value) => BitConverter.GetBytes(value);
        private static bool BytesToBool(byte[] value) => BitConverter.ToBoolean(value, 0);
        private static byte[] StringToBytes(string value) => Encoding.UTF8.GetBytes((value));
        private static string BytesToString(byte[] value) => Encoding.UTF8.GetString(value);
        private static byte[] CharToBytes(char value) => BitConverter.GetBytes((value));
        private static char BytesToChar(byte[] value) => BitConverter.ToChar(value, 0);
        private static byte[] DoubleToBytes(double value) => BitConverter.GetBytes(value);
        private static double BytesToDouble(byte[] value) => BitConverter.ToDouble(value, 0);
        #endregion
        private static byte[] ListToBytes(List<object> value)
        {
            byte[] bytes = Config.EMPTY_BYTES;
            for (int count = 0; count < value.Count; count++)
            {
                byte[] currentBytes = bytes;
                byte[] newResult = Serialize(value[count]);
                bytes = new byte[currentBytes.Length + newResult.Length];
                Array.Copy(currentBytes, 0, bytes, 0, currentBytes.Length);
                Array.Copy(newResult, 0, bytes, currentBytes.Length, newResult.Length);
            }
            return bytes;
        }
        private static List<object> BytesToList(byte[] value)
        {
            List<object> result = new List<object>();
            int nextIdx = 0;
            byte[] lengthBytes = new byte[Config.LENGTH_SIZE];
            byte[] typeBytes = new byte[Config.TYPE_SIZE];
            while (nextIdx < value.Length)
            {
                result.Add(ByteToObjectByIndex(value, ref lengthBytes, ref typeBytes, ref nextIdx));
            }
            return result;
        }
        private static byte[] DictionaryToBytes(Dictionary<byte, object> value)
        {
            byte[] bytes = Config.EMPTY_BYTES;
            foreach (KeyValuePair<byte, object> item in value)
            {
                byte[] currentBytes = bytes;
                byte[] newResultValue = Serialize(item.Value);
                bytes = new byte[currentBytes.Length + 1 + newResultValue.Length];
                Array.Copy(currentBytes, 0, bytes, 0, currentBytes.Length);
                bytes[currentBytes.Length] = item.Key;
                Array.Copy(newResultValue, 0, bytes, currentBytes.Length + 1, newResultValue.Length);
            }
            return bytes;
        }
        private static object BytesToDictionary(byte[] value)
        {
            Dictionary<byte, object> result = new Dictionary<byte, object>();
            int nextIdx = 0;
            byte[] lengthBytes = new byte[Config.LENGTH_SIZE];
            byte[] typeBytes = new byte[Config.TYPE_SIZE];
            while (nextIdx < value.Length)
            {
                byte resultKey = value[nextIdx++];
                result.Add(resultKey, ByteToObjectByIndex(value, ref lengthBytes, ref typeBytes, ref nextIdx));
            }
            return result;
        }
        private static byte[] PacketDataToBytes(PacketData value)
        {
            byte[] bytes = Config.EMPTY_BYTES;
            byte evCode = value.EvCode;
            byte[] data = Serialize(value.Data);
            bytes = new byte[1 + data.Length];
            bytes[0] = evCode;
            Array.Copy(data, 0, bytes, 1, data.Length);
            return bytes;
        }
        private static object BytesToPacketData(byte[] value)
        {
            byte[] dataBytes = new byte[value.Length - 1];
            Array.Copy(value, 1, dataBytes, 0, dataBytes.Length);
            return new PacketData(value[0], (Dictionary<byte, object>)Deserialize(dataBytes));
        }
        private static object ByteToObjectByIndex(byte[] value, ref byte[] lengthBytes, ref byte[] typeBytes, ref int nextIdx)
        {
            Array.Copy(value, nextIdx, lengthBytes, 0, Config.LENGTH_SIZE);
            int length = BytesToInt(lengthBytes);
            nextIdx += Config.LENGTH_SIZE;
            Array.Copy(value, nextIdx, typeBytes, 0, Config.TYPE_SIZE);
            short type = BytesToShort(typeBytes);
            nextIdx += Config.TYPE_SIZE;
            byte[] data = new byte[length];
            Array.Copy(value, nextIdx, data, 0, length);
            nextIdx += length;
            return TypeToValue(type, data);
        }
    }
}
