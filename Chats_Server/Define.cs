
using System.IO;

namespace MMORPG.Define
{
    public class Network
    {
        public enum PacketType
        {
            Register,
            Login,
            Logout,
            Join, // 서버에 입장을 알리는 패에에킷
            Message,
            Log,
            Actor,
        }
    }


    public class DBPath
    {
        public const string curFile = "C:/Users/wjs/Documents/GitHub/ProjectRT_Server/Chats_Server/";

        public const string DBLog = curFile + "DB/StaticData/DB/Log.txt";
        public const string DBUserInfo = curFile + "DB/StaticData/DB/UserInfo.json";
    }
}