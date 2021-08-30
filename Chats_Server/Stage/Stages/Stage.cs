using MMORPG.DataBase;
using MMORPG.Util.STL;
using System.Collections.Generic;

namespace MMORPG.Stage
{
    [System.Serializable]
    public class Stage
    {
        public int index;

        public string stageName;

        public List<DtoCharacter> characterList = new List<DtoCharacter>();

    }
}