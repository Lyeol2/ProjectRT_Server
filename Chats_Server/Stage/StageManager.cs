using MMORPG.DataBase;
using MMORPG.Define;
using MMORPG.Util;
using System.Collections.Generic;

namespace MMORPG.Stage
{
    public class StageManager : Singleton<StageManager>
    {
        public List<DtoStage> stages = new List<DtoStage>();

        public void Initialize()
        {
            stages = DBManager.Instance.ReadFile<DtoStage>(DBPath.curFile + "DB/StaticData/Json/Stages.json");
        }
        public void EnterStage(DtoUser user, int stage)
        {

        }




    }



}