namespace ProjectRT.DataBase
{ 
    [System.Serializable]
    public class DtoUser
    {
        public DtoAccount account;
        public DtoCharacter character;
        public Define.Stage stage;
    }
}