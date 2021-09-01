
using System.IO;

namespace MMORPG.Define
{
    public class Actor
    {
        public enum ActorType { Character, Monster, Building }

        public enum ActorState { Idle = 0,Walk, Sit, Rise, Attack, Dead }

        public enum ActorAnim { isWalk, isSit, isRise, isAttack, isDead }

        public enum AttackType { Normal, Projectile }
    }
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
        // 집 학원
        public const string curFile = "C:/Users/wjs/Documents/GitHub/ProjectRT_Server/Chats_Server/";
        //public const string curFile = "C:/Users/Administrator/Documents/GitHub/ProjectRT_Server/Chats_Server/";

        public const string DBLog = curFile + "DB/StaticData/DB/Log.txt";
        public const string DBUserInfo = curFile + "DB/StaticData/DB/UserInfo.json";
    }
}