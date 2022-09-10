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
        // playerId = BitConverter.ToUInt64(byteArray, 0);
        // x = BitConverter.ToSingle(byteArray, 8);
        // y = BitConverter.ToSingle(byteArray, 12);
        // var len = BitConverter.ToUInt64(byteArray, 16);
        // var strBytes = new byte[len];
        // Array.Copy(byteArray, 24, strBytes, 0, (int)len);
        // name = System.Text.Encoding.UTF8.GetString(strBytes);
        // hp = BitConverter.ToUInt32(byteArray, 24 + (int)len);
        return 0;
    }
}
