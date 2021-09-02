using System.Collections.Generic;

namespace MMORPG.DataBase
{
    [System.Serializable]
    public class DtoStage
    {
        public int index;

        public string stageName;

        public List<DtoActor> ActorList = new List<DtoActor>();
        public List<DtoUser> UserList = new List<DtoUser>();
    }




}