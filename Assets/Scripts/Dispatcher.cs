using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ResCallBackFunc = System.Func<byte[], int, int, int>;

public class Dispatcher
{
    private Dictionary<PacketTag, ResCallBackFunc> _dispatchTable;

    public Dispatcher()
    {
        _dispatchTable = new Dictionary<PacketTag, ResCallBackFunc>();
    }

    public int Dispatch(PacketTag tag, byte[] byteArray, int length, int offset) {
        if (_dispatchTable.ContainsKey(tag))
        {
            return _dispatchTable[tag](byteArray, length, offset);
        }
        else
        {
            Debug.LogErrorFormat("failed to dispatch tag:{0}",  tag.ToString());
            return 0;
        }
    }

    public void AddFunc<Res>(PacketTag tag, Action<Res> callBack) where Res : IPacket, new()
    {
        // TODO: オフセット対応
        ResCallBackFunc func = (byte[] buffer, int length, int offset) => {
            Res r = new Res();
            var newOffset = r.Desrialize(buffer, offset);
            callBack(r);
            return newOffset;
        };
        _dispatchTable.Add(tag, func);

    }

}