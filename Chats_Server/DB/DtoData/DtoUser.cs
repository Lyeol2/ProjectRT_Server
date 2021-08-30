namespace MMORPG.DataBase
{ 
    [System.Serializable]
    public class DtoUser
    {
        public DtoAccount account;
        public DtoCharacter character;
        public DtoStage stage;
    }
}