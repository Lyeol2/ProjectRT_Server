using ProjectRT.DataBase;

namespace ProjectRT.Object
{
    public class Actor
    {
        public virtual DtoActor actor { get; set; }
        public virtual void Initialize(DtoActor actor)
        {
            this.actor = actor;
        }
        public virtual void ActorUpdate()
        {

        }
        public virtual void Destory()
        {

        }
    }
}