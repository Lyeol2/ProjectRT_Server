using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace MMORPG.Util
{
    public static class SerializeHelper
    {
        public static string ToJson<T>(T data)
        {
            return JsonConvert.SerializeObject(data);
        }
        public static T FromJson<T>(string data) where T : class
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
        public static List<T> JsonToList<T>(string json)
        {
            return JsonConvert.DeserializeObject<List<T>>(json);
        }

        public static string ByteToString(byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }
        public static byte[] StringToByte(string data)
        {
            return Encoding.UTF8.GetBytes(data);
        }
        public static byte[] DataToByte<T>(T data)
        {
            string json = ToJson(data);

            return StringToByte(json);
        }
        public static T ByteToData<T>(byte[] data) where T : class
        {
            string json = ByteToString(data);

            return FromJson<T>(json);
        }
    }
}