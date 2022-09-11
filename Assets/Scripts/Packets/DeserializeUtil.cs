using System;
using System.Collections.Generic;

public class DeserializeUtil
{
    static private byte[] _convertBuffer = new byte[sizeof(long)];
    static byte[] ConvertByteOrder(byte[] byreArray, int offset, int typeSize)
    {
        if (BitConverter.IsLittleEndian && typeSize > 0)
        {
            for(int i = 0; i< typeSize; i++)
            {
                _convertBuffer[i] = byreArray[offset + typeSize - 1 - i];
            }

        }
        else
        {
            for(int i = 0; i< typeSize; i++)
            {
                _convertBuffer[i] = byreArray[offset + i];
            }
        }
        return _convertBuffer;
    }

    static public (long, int) ToI64(byte[] byteArray, int offset)
    {
        return (BitConverter.ToInt32(ConvertByteOrder(byteArray, offset, sizeof(long)), 0), offset + sizeof(long));
    }
    static public (ulong, int) ToU64(byte[] byteArray, int offset)
    {
        return (BitConverter.ToUInt32(ConvertByteOrder(byteArray, offset, sizeof(ulong)), 0), offset + sizeof(ulong));
    }
    static public (int, int) ToI32(byte[] byteArray, int offset)
    {
        return (BitConverter.ToInt32(ConvertByteOrder(byteArray, offset, sizeof(int)), 0), offset + sizeof(int));
    }
    static public (uint, int) ToU32(byte[] byteArray, int offset)
    {
        return (BitConverter.ToUInt32(ConvertByteOrder(byteArray, offset, sizeof(uint)), 0), offset + sizeof(uint));
    }

    static public (short, int) ToI16(byte[] byteArray, int offset)
    {
        return (BitConverter.ToInt16(ConvertByteOrder(byteArray, offset, sizeof(short)), 0), offset + sizeof(short));
    }
    static public (ushort, int) ToU16(byte[] byteArray, int offset)
    {
        return (BitConverter.ToUInt16(ConvertByteOrder(byteArray, offset, sizeof(ushort)), 0), offset + sizeof(ushort));
    }

    static public (sbyte, int) ToI8(byte[] byteArray, int offset)
    {
        return ((sbyte)byteArray[offset], offset + sizeof(sbyte));
    }
    static public (byte, int) ToU8(byte[] byteArray, int offset)
    {
        return (byteArray[offset], offset + sizeof(byte));
    }

    static public (float, int) ToFloat(byte[] byteArray, int offset)
    {
        return (BitConverter.ToSingle(ConvertByteOrder(byteArray, offset, sizeof(float)), 0), offset + sizeof(float));
    }
    static public (double, int) ToDouble(byte[] byteArray, int offset)
    {
        return (BitConverter.ToDouble(ConvertByteOrder(byteArray, offset, sizeof(double)), 0), offset + sizeof(double));
    }

    static public (string, int) ToString(byte[] byteArray, int offset)
    {
        (var l, _) = ToU64(byteArray, offset); 
        var len = (int)Math.Min(l, Int32.MaxValue);
        var strBytes = new byte[len];
        Array.Copy(byteArray, offset + sizeof(long), strBytes, 0, len);
        return (System.Text.Encoding.UTF8.GetString(strBytes), offset + sizeof(long) + len);
    }

    static public (List<long>, int) ToVecI64(byte[] byteArray, int offset)
    {
        (var l, var o) = ToU64(byteArray, offset);
        var len = (int)Math.Min(l, Int32.MaxValue);
        offset = o;
        var list = new List<long>(len);
        for(int i = 0; i < len; i++)
        {
            list.Add(BitConverter.ToInt64(ConvertByteOrder(byteArray, offset, sizeof(long)), 0));
            offset += sizeof(long);
        }
        return (list, offset);
    }

    static public (List<ulong>, int) ToVecU64(byte[] byteArray, int offset)
    {
        (var l, var o) = ToU64(byteArray, offset);
        var len = (int)Math.Min(l, Int32.MaxValue);
        offset = o;
        var list = new List<ulong>(len);
        for(int i = 0; i < len; i++)
        {
            list.Add(BitConverter.ToUInt64(ConvertByteOrder(byteArray, offset, sizeof(ulong)), 0));
            offset += sizeof(ulong);
        }
        return (list, offset);
    }
    static public (List<int>, int) ToVecI32(byte[] byteArray, int offset)
    {
        (var l, var o) = ToU64(byteArray, offset);
        var len = (int)Math.Min(l, Int32.MaxValue);
        offset = o;
        var list = new List<int>(len);
        for(int i = 0; i < len; i++)
        {
            list.Add(BitConverter.ToInt32(ConvertByteOrder(byteArray, offset, sizeof(int)), 0));
            offset += sizeof(int);
        }
        return (list, offset);
    }
    static public (List<uint>, int) ToVecU32(byte[] byteArray, int offset)
    {
        (var l, var o) = ToU64(byteArray, offset);
        var len = (int)Math.Min(l, Int32.MaxValue);
        offset = o;
        var list = new List<uint>(len);
        for(int i = 0; i < len; i++)
        {
            list.Add(BitConverter.ToUInt32(ConvertByteOrder(byteArray, offset, sizeof(uint)), 0));
            offset += sizeof(uint);
        }
        return (list, offset);
    }
    static public (List<short>, int) ToVecI16(byte[] byteArray, int offset)
    {
        (var l, var o) = ToU64(byteArray, offset);
        var len = (int)Math.Min(l, Int32.MaxValue);
        offset = o;
        var list = new List<short>(len);
        for(int i = 0; i < len; i++)
        {
            list.Add(BitConverter.ToInt16(ConvertByteOrder(byteArray, offset, sizeof(short)), 0));
            offset += sizeof(short);
        }
        return (list, offset);
    }
    static public (List<ushort>, int) ToVecU16(byte[] byteArray, int offset)
    {
        (var l, var o) = ToU64(byteArray, offset);
        var len = (int)Math.Min(l, Int32.MaxValue);
        offset = o;
        var list = new List<ushort>(len);
        for(int i = 0; i < len; i++)
        {
            list.Add(BitConverter.ToUInt16(ConvertByteOrder(byteArray, offset, sizeof(ushort)), 0));
            offset += sizeof(ushort);
        }
        return (list, offset);
    }
    static public (List<sbyte>, int) ToVecI8(byte[] byteArray, int offset)
    {
        (var l, var o) = ToU64(byteArray, offset);
        var len = (int)Math.Min(l, Int32.MaxValue);
        offset = o;
        var list = new List<sbyte>(len);
        for(int i = 0; i < len; i++)
        {
            list.Add((sbyte)byteArray[offset]);
            offset += sizeof(sbyte);
        }
        return (list, offset);
    }
    static public (List<byte>, int) ToVecU8(byte[] byteArray, int offset)
    {
        (var l, var o) = ToU64(byteArray, offset);
        var len = (int)Math.Min(l, Int32.MaxValue);
        offset = o;
        var list = new List<byte>(len);
        for(int i = 0; i < len; i++)
        {
            list.Add(byteArray[offset]);
            offset += sizeof(byte);
        }
        return (list, offset);
    }
    static public (List<float>, int) ToVecFloat(byte[] byteArray, int offset)
    {
        (var l, var o) = ToU64(byteArray, offset);
        var len = (int)Math.Min(l, Int32.MaxValue);
        offset = o;
        var list = new List<float>(len);
        for(int i = 0; i < len; i++)
        {
            list.Add(BitConverter.ToSingle(ConvertByteOrder(byteArray, offset, sizeof(float)), 0));
            offset += sizeof(float);
        }
        return (list, offset);
    }
    static public (List<double>, int) ToVecDouble(byte[] byteArray, int offset)
    {
        (var l, var o) = ToU64(byteArray, offset);
        var len = (int)Math.Min(l, Int32.MaxValue);
        offset = o;
        var list = new List<double>(len);
        for(int i = 0; i < len; i++)
        {
            list.Add(BitConverter.ToDouble(ConvertByteOrder(byteArray, offset, sizeof(double)), 0));
            offset += sizeof(double);
        }
        return (list, offset);
    }
    static public (List<T>, int) ToVec<T>(byte[] byteArray, int offset) where T :IPacket, new()
    {
        (var l, var o) = ToU64(byteArray, offset);
        var len = (int)Math.Min(l, Int32.MaxValue);
        offset = o;
        var list = new List<T>(len);
        for(int i = 0; i < len; i++)
        {
            var obj = new T();
            offset = obj.Desrialize(byteArray, offset);
            list.Add(obj);
        }
        return (list, offset);
    }
}
