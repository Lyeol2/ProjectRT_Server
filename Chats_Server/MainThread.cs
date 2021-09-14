using ProjectRT.DataBase;
using ProjectRT.DB;
using ProjectRT.Network;
using ProjectRT.Object;
using ProjectRT.Util;
using System;
using System.Threading;
using PacketType = ProjectRT.Define.Network.PacketType;

namespace ProjectRT
{
    class MainThread
    {
        static void Main()
        {

            DBManager.Instance.Initialize();
            ObjectManager.Instance.Initialize();
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
            string input = Console.ReadLine();

            string[] command = input.Split(' ');

            if(command[0] == "spawn")
            {
                var monster = ObjectManager.Instance.SpawnMonster(int.Parse(command[1]));

                var mob = new Monster();
                mob.Initialize(monster);
                ObjectManager.Instance.monster.Add(mob);


            }

            return true;
        }
        static void GameUpdate()
        {
            while (true)
            {
                Thread.Sleep(200);
                ObjectManager.Instance.Update();
            }
        }

    }
}
