using System;
using System.Collections.Generic;
using System.Text;
using MMORPG.DataBase;
using MMORPG.DB;
using MMORPG.Define;
using MMORPG.Network;
using MMORPG.Stage;
using MMORPG.Util;

using PacketType = MMORPG.Define.Network.PacketType;

namespace MMORPG
{
    public class Child : Parent
    {
        int child;
    }
    public class Parent
    {
        int parent;
    }
    class MainThread
    {

               
        static void Main()
        {


            DBManager.Instance.Initialize();
            StageManager.Instance.Initialize();
            //새로운 서버객체 생성

            if (ServerManager.Instance.CreateServer("127.0.0.1", 4826))
            {
                Console.WriteLine("서버 생성에 성공했습니다.");
            }
            else
            {
                Console.WriteLine("서버 생성에 실패했습니다.");
            }

            while (true)
            {
                // 콘솔에 대한 명령.
                string a = Console.ReadLine();

                if (!ServerManager.Instance.clients.Exists(_ => _.account.id == a)) continue;

                var client = ServerManager.Instance.clients.Find(_ => _.account.id == a);

                DtoActor dto = new DtoActor();
                dto.guid = "sample";

                Packet pack = new Packet() { data = SerializeHelper.ToJson(dto), type = PacketType.Actor };

                Console.WriteLine("Send!");
                ServerManager.Instance.SendClient(SerializeHelper.DataToByte(pack), client.ep);

            }
        }

    }
}
