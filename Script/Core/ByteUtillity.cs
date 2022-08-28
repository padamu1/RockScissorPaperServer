using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace SimulFactory.Core
{
    public class DataFormat
    {
        public byte evCode = 0;
        public Dictionary<byte, object> data = new Dictionary<byte, object>();
    }
    public class ByteUtillity
    {
        public static DataFormat ByteToObject(byte[] buffer)
        {
            try
            {
                DataFormat dataFormat = new DataFormat();
                string data = Encoding.UTF8.GetString(buffer);
                if (data == null)
                {
                    return null;
                }
                data = "";
                dataFormat = JsonConvert.DeserializeObject<DataFormat>(data);
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
    }
}
