using Newtonsoft.Json;
using System;
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
        public static T FromJson<T>(string data)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(data);
            }
            catch(Exception ex)
            {
                Console.WriteLine(data);
                throw new Exception("비정상적 데이터가 들어왔습니다.");
            }
        }
        public static List<T> JsonToList<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<T>>(json);
            }
            catch(Exception ex)
            {
                Console.WriteLine(json);
                return null;
            }
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
        public static T ByteToData<T>(byte[] data)
        {
            string json = ByteToString(data);

            return FromJson<T>(json);
        }
    }
}