using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using MMORPG.DataBase;
using MMORPG.DB;
using MMORPG.Define;
using MMORPG.Network;
using MMORPG.Stage;
using MMORPG.Util;

using PacketType = MMORPG.Define.Network.PacketType;

namespace MMORPG
{
    class MainThread
    {
        static void Main()
        {

            DBManager.Instance.Initialize();
            //새로운 서버객체 생성

            if (ServerManager.Instance.CreateServer("127.0.0.1", 4826))
            {
                Console.WriteLine("서버 생성에 성공했습니다.");
            }
            else
            {
                Console.WriteLine("서버 생성에 실패했습니다.");
            }

            Thread gameUpdate = new Thread(() => GameUpdate());
            gameUpdate.Start();

            while (Command()) { };

        }
        static bool Command()
        {
            // 콘솔에 대한 명령.
            string a = Console.ReadLine();

            if (!ServerManager.Instance.clients.Exists(_ => _.account.id == a)) return true;

            var client = ServerManager.Instance.clients.Find(_ => _.account.id == a);

            DtoActor dto = new DtoActor();
            dto.guid = "sample";

            Packet pack = new Packet() { data = SerializeHelper.ToJson(dto), type = PacketType.Actor };

            Console.WriteLine("Send!");
            ServerManager.Instance.SendClient(SerializeHelper.DataToByte(pack), client.ep);

            return true;
        }
        static void GameUpdate()
        {
            while (true)
            {

            }
        }

    }
}
