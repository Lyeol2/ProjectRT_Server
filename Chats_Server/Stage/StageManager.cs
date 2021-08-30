using MMORPG.DataBase;
using MMORPG.Define;
using MMORPG.Util;
using System.Collections.Generic;

namespace MMORPG.Stage
{
    public class StageManager : Singleton<StageManager>
    {
        public List<Stage> stages = new List<Stage>();

        public void Initialize()
        {
            stages = DBManager.Instance.ReadFile<Stage>(DBPath.curFile + "DB/StaticData/Json/Stages.json");
        }




    }



}