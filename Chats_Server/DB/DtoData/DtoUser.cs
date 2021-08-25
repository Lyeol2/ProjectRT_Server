namespace MMORPG.DataBase
{ 
    [System.Serializable]
    public class DtoUser : DtoBase
    {
        public DtoAccount account;
        public DtoCharacter character;
    }
}