using ProjectRT.DataBase;
using ProjectRT.Util;
using System;
using System.Collections.Generic;

namespace ProjectRT.Object
{
    
    public class ObjectManager : Singleton<ObjectManager>
    {
        public Queue<object> actorQueue = new Queue<object>();

        public List<DtoUser> users = new List<DtoUser>();
        public List<Monster> monster = new List<Monster>();

        public void Initialize()
        {
            Instance.users = new List<DtoUser>();
            Instance.monster = new List<Monster>();
        }
        public void ActorUpdate()
        {
            DtoActor actor = null;
            if (actorQueue.Count > 0) actor = actorQueue.Dequeue() as DtoActor;

            foreach (var item in monster)
            {
                if (actor?.guid == item.actor.guid)
                {
                    item.ActorUpdate(actor as DtoMonster);
                }
                else
                {
                    item.ActorUpdate();
                }
            }
        }
        public void Update()
        {
            ActorUpdate();

        }
        public void SetCharacterInfo(DtoCharacter character)
        {
            if (!users.Exists(_ => _.character.guid == character.guid)) return;
            var data = users.Find(_ => _.character.guid == character.guid);
            data.character = character;
        }
        public DtoMonster SpawnMonster(int index)
        {
            var dto = new DtoMonster()
            {
                targetName = null,
                monsterIndex = index,
                position = new DtoVector() { x = 0, y = 0, z = 0 },
                guid = Guid.NewGuid().ToString(),
                lookDirect = new DtoVector() { x = 1, y = 1, z = 1 },
                direct = new DtoVector(),
                speed = 3f,
                state = Define.Actor.ActorState.Idle,
     
            };


            return dto;
        }
    }

}