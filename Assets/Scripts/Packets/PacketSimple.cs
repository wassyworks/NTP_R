using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SimpleEntity : IPacket
{
    public SimpleEntity() {}
    public ulong playerId = 0;
    public float x = 0.0f;
    public float y = 0.0f;
    public string name = "";
    public List<int> itemIds = new List<int>();
    public uint hp = 0;

    public PacketTag GetTag() {
        return PacketTag.Simple;
    }

    public int Serialize(byte[] byteArray, int offset)
    {

        return 0;
    }
    public int Desrialize(byte[] byteArray, int offset)
    {
        (playerId, offset) = DeserializeUtil.ToU64(byteArray, offset);
        (x, offset) = DeserializeUtil.ToFloat(byteArray, offset);
        (y, offset) = DeserializeUtil.ToFloat(byteArray, offset);
        (name, offset) = DeserializeUtil.ToString(byteArray, offset);
        (hp, offset) = DeserializeUtil.ToU32(byteArray, offset);
        return offset;
    }
}
