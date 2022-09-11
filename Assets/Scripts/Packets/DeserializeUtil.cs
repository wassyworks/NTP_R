using System;

public class DeserializeUtil
{
    static public (long, int)  ToI64(byte[] byteArray, int offset){
        return (BitConverter.ToInt32(byteArray, offset), offset + sizeof(long)); 
    }
    static public (ulong, int) ToU64(byte[] byteArray, int offset){
        return (BitConverter.ToUInt32(byteArray, offset), offset + sizeof(ulong)); 
    }
    static public (int, int) ToI32(byte[] byteArray, int offset){
        return (BitConverter.ToInt32(byteArray, offset), offset + sizeof(int)); 
    }
    static public (uint, int) ToU32(byte[] byteArray, int offset){
        return (BitConverter.ToUInt32(byteArray, offset), offset + sizeof(uint)); 
    }

    static public (short, int) ToI16(byte[] byteArray, int offset){
        return (BitConverter.ToInt16(byteArray, offset), offset + sizeof(short)); 
    }
    static public (ushort, int) ToU16(byte[] byteArray, int offset){
        return (BitConverter.ToUInt16(byteArray, offset), offset + sizeof(ushort)); 
    }

    static public (sbyte, int) ToI8(byte[] byteArray, int offset){
        return ((sbyte)byteArray[offset], offset + sizeof(sbyte)); 
    }
    static public (byte, int) ToU8(byte[] byteArray, int offset){
        return (byteArray[offset], offset + sizeof(byte)); 
    }

    static public (float, int) ToFloat(byte[] byteArray, int offset){
        return (BitConverter.ToSingle(byteArray, offset), offset + sizeof(float)); 
    }
    static public (double, int) ToDouble(byte[] byteArray, int offset){
        return (BitConverter.ToDouble(byteArray, offset), offset + sizeof(double)); 
    }

    static public (string, int) ToString(byte[] byteArray, int offset){
        var len = (int)Math.Min(BitConverter.ToUInt64(byteArray, offset), Int32.MaxValue);
        var strBytes = new byte[len];
        Array.Copy(byteArray, offset + sizeof(long), strBytes, 0, len);
        return (System.Text.Encoding.UTF8.GetString(strBytes), offset + sizeof(long) + len);
    }

}
