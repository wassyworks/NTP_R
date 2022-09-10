using System;
using System.Collections;
using System.Collections.Generic;

public class Dispatcher
{
    private Dictionary<PacketTag, Action<byte[], int, int>> _dispatchTable;

    public Dispatcher()
    {
        _dispatchTable = new Dictionary<PacketTag, Action<byte[], int, int>>();
    }

    public void Dispatch(PacketTag tag, byte[] byteArray, int length, int offset) {
        _dispatchTable[tag](byteArray, length, offset);

    }

    public void AddFunc<Res>(PacketTag tag, Action<Res> callBack) where Res : IPacket, new()
    {
        // TODO: オフセット対応
        Action<byte[], int, int> func = (byte[] buffer, int length, int offset) => {
            Res r = new Res();
            var newOffset = r.Desrialize(buffer, offset);
            callBack(r);
        };
        _dispatchTable.Add(tag, func);

    }

}