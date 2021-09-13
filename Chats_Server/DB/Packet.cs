using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectRT.DB
{
    using PacketType = Define.Network.PacketType;

    [System.Serializable]
    public class Packet
    {
        public int errorCode;

        public string log;
        // 패킷 타입
        public PacketType type;
        // 패킷 데이터
        public string data;
    }
}
