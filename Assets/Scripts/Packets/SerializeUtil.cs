using System;

public class SerializeUtil
{
    static private int BufferCopy(ref byte[] tgtBuffer, int offset, byte[] srcBuffer, int typeSize)
    {
        if (BitConverter.IsLittleEndian && typeSize > 1)
        {
            for (int i = 0; i < typeSize; i++)
            { // �l�b�g���[�N�o�C�g�I�[�_�[�Ŋi�[
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
}