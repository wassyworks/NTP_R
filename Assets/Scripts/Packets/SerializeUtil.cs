using System;
using System.Collections.Generic;

public class SerializeUtil
{
    static private int BufferCopy(ref byte[] tgtBuffer, int offset, byte[] srcBuffer, int typeSize)
    {
        if (BitConverter.IsLittleEndian && typeSize > 1)
        {
            for (int i = 0; i < typeSize; i++)
            { // ネットワークバイトオーダーで格納
                tgtBuffer[offset + i] = srcBuffer[typeSize - 1 - i];
            }
        }
        else
        {
            for (int i = 0; i < typeSize; i++)
            {
                tgtBuffer[offset + i] = srcBuffer[i];
            }
        }
        return offset + typeSize;
    }

    static public int ToBytes(ref byte[] byteArray, int offset, long value)
    {
        return BufferCopy(ref byteArray, offset, BitConverter.GetBytes(value), sizeof(long));
    }

    static public int ToBytes(ref byte[] byteArray, int offset, ulong value)
    {
        return BufferCopy(ref byteArray, offset, BitConverter.GetBytes(value), sizeof(ulong));
    }

    static public int ToBytes(ref byte[] byteArray, int offset, int value)
    {
        return BufferCopy(ref byteArray, offset, BitConverter.GetBytes(value), sizeof(int));
    }

    static public int ToBytes(ref byte[] byteArray, int offset, uint value)
    {
        return BufferCopy(ref byteArray, offset, BitConverter.GetBytes(value), sizeof(uint));
    }
    static public int ToBytes(ref byte[] byteArray, int offset, short value)
    {
        return BufferCopy(ref byteArray, offset, BitConverter.GetBytes(value), sizeof(short));
    }
    static public int ToBytes(ref byte[] byteArray, int offset, ushort value)
    {
        return BufferCopy(ref byteArray, offset, BitConverter.GetBytes(value), sizeof(ushort));
    }

    static public int ToBytes(ref byte[] byteArray, int offset, float value)
    {
        return BufferCopy(ref byteArray, offset, BitConverter.GetBytes(value), sizeof(float));
    }
    static public int ToBytes(ref byte[] byteArray, int offset, double value)
    {
        return BufferCopy(ref byteArray, offset, BitConverter.GetBytes(value), sizeof(double));
    }
    static public int ToBytes(ref byte[] byteArray, int offset, sbyte value)
    {
        return BufferCopy(ref byteArray, offset, BitConverter.GetBytes(value), sizeof(sbyte));
    }
    static public int ToBytes(ref byte[] byteArray, int offset, byte value)
    {
        return BufferCopy(ref byteArray, offset, BitConverter.GetBytes(value), sizeof(byte));
    }

    static public int ToBytes(ref byte[] byteArray, int offset, string value)
    {
        var strbuff = System.Text.Encoding.UTF8.GetBytes(value);
        offset = ToBytes(ref byteArray, offset, (ulong)strbuff.Length);
        for (int i = 0; i < strbuff.Length; i++)
        {
            byteArray[offset + i] = strbuff[i];
        }

        return offset + strbuff.Length;
    }

    static public int ToBytes(ref byte[] byteArray, int offset, List<long> list)
    {
        offset = ToBytes(ref byteArray, offset, (ulong)list.Count);
        foreach(var item in list)
        {
            offset = ToBytes(ref byteArray, offset, item);
        }
        return offset;
    }

    static public int ToBytes(ref byte[] byteArray, int offset, List<ulong> list)
    {
        offset = ToBytes(ref byteArray, offset, (ulong)list.Count);
        foreach(var item in list)
        {
            offset = ToBytes(ref byteArray, offset, item);
        }
        return offset;
    }

    static public int ToBytes(ref byte[] byteArray, int offset, List<int> list)
    {
        offset = ToBytes(ref byteArray, offset, (ulong)list.Count);
        foreach(var item in list)
        {
            offset = ToBytes(ref byteArray, offset, item);
        }
        return offset;
    }

    static public int ToBytes(ref byte[] byteArray, int offset, List<uint> list)
    {
        offset = ToBytes(ref byteArray, offset, (ulong)list.Count);
        foreach(var item in list)
        {
            offset = ToBytes(ref byteArray, offset, item);
        }
        return offset;
    }
    static public int ToBytes(ref byte[] byteArray, int offset, List<short> list)
    {
        offset = ToBytes(ref byteArray, offset, (ulong)list.Count);
        foreach(var item in list)
        {
            offset = ToBytes(ref byteArray, offset, item);
        }
        return offset;
    }

    static public int ToBytes(ref byte[] byteArray, int offset, List<ushort> list)
    {
        offset = ToBytes(ref byteArray, offset, (ulong)list.Count);
        foreach(var item in list)
        {
            offset = ToBytes(ref byteArray, offset, item);
        }
        return offset;
    }
    static public int ToBytes(ref byte[] byteArray, int offset, List<sbyte> list)
    {
        offset = ToBytes(ref byteArray, offset, (ulong)list.Count);
        foreach(var item in list)
        {
            offset = ToBytes(ref byteArray, offset, item);
        }
        return offset;
    }
    static public int ToBytes(ref byte[] byteArray, int offset, List<byte> list)
    {
        offset = ToBytes(ref byteArray, offset, (ulong)list.Count);
        foreach(var item in list)
        {
            offset = ToBytes(ref byteArray, offset, item);
        }
        return offset;
    }
    static public int ToBytes(ref byte[] byteArray, int offset, List<float> list)
    {
        offset = ToBytes(ref byteArray, offset, (ulong)list.Count);
        foreach(var item in list)
        {
            offset = ToBytes(ref byteArray, offset, item);
        }
        return offset;
    }
    static public int ToBytes(ref byte[] byteArray, int offset, List<double> list)
    {
        offset = ToBytes(ref byteArray, offset, (ulong)list.Count);
        foreach(var item in list)
        {
            offset = ToBytes(ref byteArray, offset, item);
        }
        return offset;
    }

    static public int ToBytes<T>(ref byte[] byteArray, int offset, IList<T> list) where T : IPacket
    {
        offset = ToBytes(ref byteArray, offset, (ulong)list.Count);
        foreach(var item in list)
        {
            offset = item.Desrialize(byteArray, offset);
        }
        return offset;
    }
}
