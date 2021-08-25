using MMORPG.Util;
using System.Collections.Generic;
using System.IO;

namespace MMORPG.DataBase
{
    public class DBManager : Singleton<DBManager>
    {
        public List<T> ReadFile<T>(string path)
        {
            string data = File.ReadAllText(path);

            return SerializeHelper.JsonToList<T>(data);
        }
        public void WriteFile<T>(T data, string path)
        {
            string json = SerializeHelper.ToJson(data);

            File.WriteAllText(path, json);

            return;
        }
        public void ClearFile(string path)
        {
            File.WriteAllText(path, "");
        }
    }
}