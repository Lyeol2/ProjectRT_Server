using System;
using System.Collections.Generic;
using System.Text;

namespace MMORPG.DB
{
    using PacketType = Define.Network.PacketType;

    [System.Serializable]
    public class Packet
    {
        // 패킷 사이즈
        public int size;
        // 패킷 타입
        public PacketType type;
        // 패킷 데이터
        public string data;
    }
}
