
namespace MMORPG.Define
{
    public class Network
    {
        public enum PacketType
        {
            Register,
            Login,
            Logout,
            Message,
            Log,
            Actor,
        }
    }


    public class DBPath
    {
        public const string curFile = "C:/Users/Administrator/Documents/GitHub/ProjectRT_Server/Chats_Server/";
        public const string DBLog = curFile + "DB/StaticData/DB/Log.txt";
        public const string DBUserInfo = curFile + "DB/StaticData/DB/UserInfo.json";
    }
}