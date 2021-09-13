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


        public void Update()
        {
            foreach(var item in monster)
            {
                item.ActorUpdate();
            }
        }
        public DtoUser FindUserInfo(DtoAccount account)
        {
            return users.Find(_ => _.account == account);
        }
        public void SetMonsterInfo(DtoMonster dto)
        {
            monster.Find(_ => _.actor.guid == dto.guid).actor = dto;
        }
        public DtoMonster SpawnMonster(int index)
        {
            var dto = new DtoMonster()
            {
                isTargeting = false,
                monsterIndex = index,
                position = new DtoVector() { x = 0, y = 0, z = 0 },
                guid = Guid.NewGuid().ToString(),
                lookDirect = new DtoVector() { x = 1, y = 1, z = 1 },
            };


            return dto;
        }
    }

}