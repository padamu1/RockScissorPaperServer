﻿using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SimulFactory.Core.Util
{
    [Serializable]
    public class EventData
    {
        public byte eventCode;
        public Dictionary<byte, object> data;

        public EventData(byte eventCode, Dictionary<byte, object> data)
        {
            this.eventCode = eventCode;
            this.data = data;
        }
    }
    public class ByteUtillity
    {
        public static EventData ByteToObject(byte[] buffer)
        {
            try
            {
                string data = Encoding.UTF8.GetString(buffer);
                if (data == null)
                {
                    return null;
                }
                EventData? dataFormat = JsonConvert.DeserializeObject<EventData>(data);

                //Console.WriteLine(data);
                //Console.WriteLine(dataFormat?.eventCode);
                return dataFormat;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            return null;
        }

        public static byte[] ObjectToByte(object obj)
        {
            try
            {
                string data = JsonConvert.SerializeObject(obj);
                byte[] byteData = Encoding.UTF8.GetBytes(data);
                return byteData;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            return null;
        }
        public static long BytesToLong(byte[] bytes)
        {
            return BitConverter.ToInt64(bytes,0);
        }
    }
}
