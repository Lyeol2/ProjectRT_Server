using ProjectRT.Util;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Newtonsoft.Json;
using System.IO;
using ProjectRT.Define;
using ProjectRT.DB;
using ProjectRT.DataBase;
using ProjectRT.Object;

namespace ProjectRT.Network
{

    public class Client
    {
        // 서버에서 클라이언트의 개별 쓰레드
        public Thread thread;
        // 클라이언트의 소켓
        public Socket socket;
        // 클라이언트의 ep
        public string ep;

        public bool IsLogin = false;

        public DtoAccount account;
    }

    public class ServerManager : Singleton<ServerManager>
    {
        public List<Client> clients = new List<Client>();

        public Queue<KeyValuePair<Client, Packet>> recvQueue = new Queue<KeyValuePair<Client, Packet>>();

        public static Socket serverSocket;

        public bool CreateServer(string ip, int port)
        {
            serverSocket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);

            IPEndPoint ep = new IPEndPoint(IPAddress.Any, port);

            serverSocket.NoDelay = true;

            serverSocket.Bind(ep);

            serverSocket.Listen(10);

            WriteLog("서버를 시작합니다");
            Console.WriteLine("서버 접속을 받는 쓰레드를 생성합니다");
            Thread AccpectThread = new Thread(() => TryConnectServer());
            AccpectThread.Start(); 
            
            Thread ProcessThread = new Thread(() => ReceiveProcess());
            ProcessThread.Start();

            return true;
        }
        public void TryConnectServer()
        {
            while (true)
            {

                Socket sock = serverSocket.Accept();


                Console.WriteLine("접속을 받았습니다.");

                if (!sock.Connected)
                {
                    continue;
                }

                sock.NoDelay = true;

                Client client = new Client();
                client.socket = sock;
                client.ep = sock.RemoteEndPoint.ToString();
                // 클라이언트를 받는 쓰레드를 생성
                client.thread = new Thread(() => ReceiveClient(client));
                client.thread.Start();


                WriteLog($"[+] : {client.ep}", true);
                clients.Add(client);
                //쓰레드 시작.



            }
        }
        public void ReceiveProcess()
        {
            while (true)
            {
                while (recvQueue.Count > 0)
                {
                    var que = recvQueue.Dequeue();

                    try
                    {
                        PacketProcessHelper.PacketProcess(que.Key, que.Value);
                    }
                    catch
                    {
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 클라이언트로부터 데이터를 받습니다.
        /// </summary>
        /// <param name="client"></param> 데이터를 받을 클라이언트
        public void ReceiveClient(Client client)
        {
            while (client.socket.Connected)
            {
                byte[] buffer = new byte[4096];

                int n;
                try
                {
                    n = client.socket.Receive(buffer, SocketFlags.None);
                }
                catch
                {
                    break;
                }

                if (n == 0) continue;

                var packet = SerializeHelper.ByteToString(buffer);
                WriteLog($"{client.ep} => {packet}");

                if (packet.Contains("disconnect")) break;

                Packet pack;

                try
                {
                     pack = SerializeHelper.FromJson<Packet>(packet);
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"{client.ep} : {ex}");
                    continue;
                }
                // 버퍼가 비어있지않다면

                recvQueue.Enqueue(new KeyValuePair<Client, Packet>(client, pack));



            }


            WriteLog($"[-] : {client.ep}", true);

            ObjectManager.Instance.users.RemoveAll(_ => _.account == client.account);


            client.socket.Close();
            clients.Remove(client);

        }
        /// <summary>
        /// 클라이언트로 데이터를 전송합니다
        /// </summary>
        /// <param name="data"></param> 데이터
        /// <param name="ep"></param> 클라이언트의 아이디
        public void SendClient(byte[] data, string ep)
        {
            // 아이디를 통해 등록된 클라이언트 검색
            Client client = clients.Find(_ => (_.ep == ep));
            WriteLog($"{client.ep} <= {SerializeHelper.ByteToString(data)}");


            try
            {
                client.socket.Send(data, SocketFlags.None);
            }
            catch
            {
                return;
            }
        }
        /// <summary>
        /// 모든 클라이언트에 데이터를 전송합니다.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="exClient"</parma> 메세지를 보내지않을 클라이언트를 설정합니다.
        public void SendAllClient(byte[] data, List<string> exClient)
        {
            if (exClient == null)
            {
                foreach (var client in clients)
                {
                    if (client.IsLogin)
                    {
                        SendClient(data, client.ep);
                    }
                }
            }
            if (exClient != null)
            {
                foreach (var client in clients)
                {
                    if (exClient.Exists(_ => (_ == client.ep))) continue;

                    if (client.IsLogin)
                    {
                        SendClient(data, client.ep);
                    }
                }
            }
        }

        public void SendAllClient(byte[] data, string exClient = null)
        {

            if (exClient == null)
            {
                foreach (var client in clients)
                {
                    if (client.IsLogin)
                    {
                        SendClient(data, client.ep);
                    }
                }
            }
            if (exClient != null)
            {
                foreach (var client in clients)
                {
                    if (exClient == client.ep) continue;

                    if (client.IsLogin)
                    {
                        SendClient(data, client.ep);
                    }
                }
            }
        }
        public void WriteLog(string log, bool isPrint = false)
        {
            if (isPrint) Console.WriteLine(log);

            //StreamWriter writer = File.AppendText(DBPath.DBLog);
            //writer.WriteLine(log);
            //writer.Close();


        }
    }
}