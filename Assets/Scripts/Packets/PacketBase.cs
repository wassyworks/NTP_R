public interface IPacket {
    PacketTag GetTag() ;
    public int Serialize(ref byte[] byteArray, int offset);
    public int Desrialize(byte[] byteArray, int offset);
}