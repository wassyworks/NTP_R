using System.Collections.Generic;

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

    public int Serialize(ref byte[] byteArray, int offset)
    {
        offset = SerializeUtil.ToBytes(ref byteArray, offset, playerId);
        offset = SerializeUtil.ToBytes(ref byteArray, offset, x);
        offset = SerializeUtil.ToBytes(ref byteArray, offset, y);
        offset = SerializeUtil.ToBytes(ref byteArray, offset, name);
        offset = SerializeUtil.ToBytes(ref byteArray, offset, itemIds);
        offset = SerializeUtil.ToBytes(ref byteArray, offset, hp);
        return offset;
    }
    public int Desrialize(byte[] byteArray, int offset)
    {
        (playerId, offset) = DeserializeUtil.ToU64(byteArray, offset);
        (x, offset) = DeserializeUtil.ToFloat(byteArray, offset);
        (y, offset) = DeserializeUtil.ToFloat(byteArray, offset);
        (name, offset) = DeserializeUtil.ToString(byteArray, offset);
        (itemIds, offset) = DeserializeUtil.ToVecI32(byteArray, offset);
        (hp, offset) = DeserializeUtil.ToU32(byteArray, offset);
        return offset;
    }
}
