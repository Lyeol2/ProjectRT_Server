
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
        public override void SetActorInfo(DtoActor actor)
        {
            base.SetActorInfo(actor);

            if(actor is DtoMonster)
            {
                this.actor = actor as DtoMonster;
            }
        }
        public override void ActorUpdate(DtoActor actor = null)
        {
            base.ActorUpdate(actor);

            // 타겟 프로세스가 작동중이 아니라면 리턴
            if(!WaitingTargetProcess(actor as DtoMonster)) return;

            // 널값이면 세팅할 필요가 없다.
            if (actor == null) return;
            SetActorInfo(actor);

        }
        public bool WaitingTargetProcess(DtoMonster dto = null)
        {

            if ((actor as DtoMonster).targetName != null)
            {

                if (ObjectManager.Instance.users.Exists(_ => _.character.nickName == (actor as DtoMonster).targetName))
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("감지 해제");
                    (actor as DtoMonster).targetName = null;
                    actor.direct = new DtoVector() { x = 0, y = 0, z = 0, };
                    actor.state = Define.Actor.ActorState.Idle;
                }
            }
            var user = FindInRangeUser();

            var packet = PacketProcessHelper.CreatePacket(Define.Network.PacketType.Monster);

            // 어그로가 해제된 상태
            if (user == null)
            {
                if(dto != null)
                {
                    actor = dto;
                }
                packet.data = SerializeHelper.ToJson(actor as DtoMonster);
                
                ServerManager.Instance.SendAllClient(SerializeHelper.DataToByte(packet));
                return false;
            }

            // 타게팅을 켜줌
            (actor as DtoMonster).targetName = user.character.nickName;


            Console.WriteLine(user.character.nickName + ": 3칸거리 내로 접근");

            Client client = ServerManager.Instance.clients.Find(_ => _.account.id == user.account.id);

            packet = PacketProcessHelper.CreatePacket(Define.Network.PacketType.Monster);

            packet.data = SerializeHelper.ToJson(actor as DtoMonster);

            ServerManager.Instance.recvQueue.Enqueue(new KeyValuePair<Client, DB.Packet>(new Client(), packet));

            return false;
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