public interface IPacket {
    PacketTag GetTag() ;
    public int Serialize(byte[] byteArray, int offset);
    public int Desrialize(byte[] byteArray, int offset);
}