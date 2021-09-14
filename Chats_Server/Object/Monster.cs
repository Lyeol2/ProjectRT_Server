
using ProjectRT.DataBase;
using ProjectRT.Network;
using ProjectRT.Util;
using System;
using System.Collections.Generic;

namespace ProjectRT.Object
{
    public class Monster : Actor
    {

        public override void Initialize(DtoActor actor)
        {
            base.Initialize(actor);

            if(actor is DtoMonster)
            {
                this.actor = actor as DtoMonster;
            }
        }
        public override void ActorUpdate()
        {
            base.ActorUpdate();

            // 타게팅 중이라면 실행하지 않음.


            if ((actor as DtoMonster).targetName != null)
            {
                if (ObjectManager.Instance.users.Exists(_ => _.character.nickName == (actor as DtoMonster).targetName))
                {
                    return;
                }
                else
                {
                    (actor as DtoMonster).targetName = null;
                }
            }
            var user = FindInRangeUser();

            var packet = PacketProcessHelper.CreatePacket(Define.Network.PacketType.Monster);

            if (user == null)
            {
                packet.data = SerializeHelper.ToJson(actor as DtoMonster);

                ServerManager.Instance.recvQueue.Enqueue(new KeyValuePair<Client, DB.Packet>(new Client(), packet));

                return;
            }

            // 타게팅을 켜줌
            (actor as DtoMonster).targetName = user.character.nickName;

            Console.WriteLine(user.character.nickName + ": 3칸거리 내로 접근");
            Client client = ServerManager.Instance.clients.Find(_ => _.account.id == user.account.id);

            packet = PacketProcessHelper.CreatePacket(Define.Network.PacketType.Monster);

            packet.data = SerializeHelper.ToJson(actor as DtoMonster);

            ServerManager.Instance.recvQueue.Enqueue(new KeyValuePair<Client, DB.Packet>(new Client(), packet));
        }
        public bool CheckingInRangeUser(DtoVector A, DtoVector B, float range)
        {
            return DtoVector.Distance(A, B) < range;
        }
        public DtoUser FindInRangeUser()
        {
            return ObjectManager.Instance.users.Find(_ => CheckingInRangeUser(_.character.position, actor.position, 3));
        }



    }
}