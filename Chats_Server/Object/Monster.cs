
using ProjectRT.DataBase;
using System;

namespace ProjectRT.Object
{
    public class Monster : Actor
    {

        public override void Initialize(DtoActor actor)
        {
            base.Initialize(actor);
        }
        public override void ActorUpdate()
        {
            base.ActorUpdate();

            var user = FindInRangeUser();
            if (user == null) return;

            // 타게팅을 켜줌
            Console.WriteLine(user.character.nickName + ": 3칸거리 내로 접근");


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