using ProjectRT.Util;
using System.Collections.Generic;
using System.IO;
using ProjectRT.Define;

namespace ProjectRT.DataBase
{
    public class DBManager : Singleton<DBManager>
    {
        public void Initialize()
        {

        }
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
        public DtoUser FindUserInfo(DtoAccount account)
        {
            return ReadFile<DtoUser>(DBPath.DBUserInfo).Find(_ => _.account == account);
        }
        public void ClearFile(string path)
        {
            File.WriteAllText(path, "");
        }
    }
}