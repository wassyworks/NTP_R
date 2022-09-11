using System;

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

}
