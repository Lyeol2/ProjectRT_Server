using ProjectRT.Util.STL;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectRT.DataBase
{
    using ActorState = Define.Actor.ActorState;


    public class DtoActor
    {
        public string guid;

        public ActorState state;

        public DtoVector direct = new DtoVector();

        public DtoVector position = new DtoVector();

        public DtoVector lookDirect = new DtoVector();

        public float speed;

    }
}
