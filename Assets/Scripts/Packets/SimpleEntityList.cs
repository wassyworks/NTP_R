using System.Collections.Generic;

public class SimpleEntityList : IPacket
{
    public SimpleEntityList() {}
    public List<SimpleEntity> entities = new List<SimpleEntity>();

    public PacketTag GetTag() {
        return PacketTag.SimpleEntityList;
    }

    public int Serialize(ref byte[] byteArray, int offset)
    {
        offset = SerializeUtil.ToBytes(ref byteArray, offset, entities);
        return offset;
    }
    public int Desrialize(byte[] byteArray, int offset)
    {
        (entities, offset) = DeserializeUtil.ToVecClass<SimpleEntity>(byteArray, offset);
        return offset;
    }
}
