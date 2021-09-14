using ProjectRT.DataBase;
using ProjectRT.Util;
using System;
using System.Collections.Generic;

namespace ProjectRT.Object
{
    
    public class ObjectManager : Singleton<ObjectManager>
    {


        public List<DtoUser> users = new List<DtoUser>();
        public List<Monster> monster = new List<Monster>();

        public void Initialize()
        {
            Instance.users = new List<DtoUser>();
            Instance.monster = new List<Monster>();
        }
        public void Update()
        {
            if (monster.Count > 0)
            {
                foreach (var item in monster)
                {
                    item.ActorUpdate();
                }
            }
        }
        public void SetCharacterInfo(DtoCharacter character)
        {
            if (!users.Exists(_ => _.character.guid == character.guid)) return;
            var data = users.Find(_ => _.character.guid == character.guid);
            data.character = character;
        }
        public void SetMonsterInfo(DtoMonster dto)
        {
            if (!monster.Exists(_ => _.actor.guid == dto.guid)) return;
            var data = monster.Find(_ => _.actor.guid == dto.guid);
            data.actor = dto;
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
                speed = 0f,
                state = Define.Actor.ActorState.Idle,
     
            };


            return dto;
        }
    }

}