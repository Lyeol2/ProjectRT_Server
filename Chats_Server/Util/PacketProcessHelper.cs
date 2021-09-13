using System;
using System.Collections.Generic;
using System.Text;
using ProjectRT.DataBase;
using ProjectRT.DB;
using ProjectRT.Define;
using ProjectRT.Network;
using ProjectRT.Object;
using ProjectRT.Stage;

namespace ProjectRT.Util
{

    using PacketType = Define.Network.PacketType;

    public static class PacketProcessHelper
    {

        public static Packet CreatePacket(PacketType type, int errorCode = 0, string log = null)
        {
            var packet = new Packet();

            packet.type = type;
            packet.errorCode = errorCode;
            packet.log = log;

            return packet;
        }
        public static void PacketProcess(Client client, Packet packet)
        {
            int errorCode;

            try
            {
                errorCode = packet.errorCode;
            }
            catch(Exception ex)
            {
                throw ex;
            }

            if (errorCode > 0)
            {
                OnFailed(client.ep, errorCode);
                return;
            }

            switch (packet.type)
            {
                case PacketType.Register:
                    Register(client, SerializeHelper.FromJson<DtoUser>(packet.data));
                    return;
                case PacketType.Login:
                    Login(client, SerializeHelper.FromJson<DtoAccount>(packet.data));
                    return;
                case PacketType.Logout:
                    return;
                case PacketType.Join:
                    DtoUser user = DBManager.Instance.FindUserInfo(client.account);
                    ObjectManager.Instance.users.Add(user);
                    client.IsLogin = true;
                    return;
                case PacketType.Message:
                    return;
                case PacketType.Log:
                    return;
                case PacketType.Actor:
                    ServerManager.Instance.SendAllClient(SerializeHelper.DataToByte(packet));
                    return;
                case PacketType.Character:
                    //ObjectManager.Instance.FindUserInfo(client.account).character = SerializeHelper.FromJson<DtoCharacter>(packet.data);
                    ServerManager.Instance.SendAllClient(SerializeHelper.DataToByte(packet));
                    return;
                case PacketType.Monster:
                    //ObjectManager.Instance.SetMonsterInfo(dtoMonster);
                    var dtoMonster = SerializeHelper.FromJson<DtoMonster>(packet.data);
                    return;
                default:
                    return;
            }
        }
        public static void OnFailed(string ep, int errorCode)
        {
            Console.WriteLine($"{ep} :: {errorCode} :: 처리실패");
        }
        public static void Register(Client client, DtoUser dto)
        {
            var dblist = DBManager.Instance.ReadFile<DtoUser>(DBPath.DBUserInfo);

            if(dblist == null)
            {
                dblist = new List<DtoUser>();
            }
            if (dblist.Exists(_ => dto.account.id == _.account.id))
            {
                // 에러 데이터 만들기
                var errorPacket = CreatePacket(PacketType.Register, 1, "이미 있는 아이디입니다.");

                ServerManager.Instance.SendClient(SerializeHelper.DataToByte(errorPacket), client.ep);

                return;
            }


            dto.character.guid = Guid.NewGuid().ToString();
            dto.character.level = 0;
            dto.character.exp = 0;

            dto.stage = Define.Stage.LobbyStage;

            dblist.Add(dto);

            DBManager.Instance.WriteFile(dblist, DBPath.DBUserInfo);

            var Packet = CreatePacket(PacketType.Register);

            Packet.data = SerializeHelper.ToJson(dto);
            dto.character.resourcePath = "Prefabs/Character/knight";

            client.account = dto.account;

            ServerManager.Instance.SendClient(SerializeHelper.DataToByte(Packet), client.ep);


            return; 
        }
        public static void Login(Client client, DtoAccount dto)
        {
            var dblist = DBManager.Instance.ReadFile<DtoUser>(DBPath.DBUserInfo);

            if (!dblist.Exists(_ => $"{dto.id}:{dto.password}" == $"{_.account.id}:{_.account.password}"))
            {
                // 에러 데이터 만들기
                var errorPacket = CreatePacket(PacketType.Register, 1, "존재하지 않는 아이디입니다.");

                ServerManager.Instance.SendClient(SerializeHelper.DataToByte(errorPacket), client.ep);

                return;
            }

            client.account = dto;
            

            var Packet = CreatePacket(PacketType.Login);
            Packet.data = SerializeHelper.ToJson(dblist.Find(_ => _.account.id == dto.id));
            ServerManager.Instance.SendClient(
                SerializeHelper.DataToByte(Packet), client.ep);

            return;
        }
    }
}