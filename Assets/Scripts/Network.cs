using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Net.Sockets;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
[Serializable]
class Entity
{
    public ulong playerId;
    public float x;
    public float y;
    public string name;
    public uint hp;
}

public class Network : MonoBehaviour
{

    [SerializeField]
    private Button SendButton = null;

    private TcpClient Client;
    private const int ClientCount = 0;

    private const int SendBufferSize = 1024;
    private const int ClientRecvBufferSize = 256;
    private byte[] RecvBuffer = new byte[ClientRecvBufferSize];
    private const int ConnectTimeout = 3000;
    private const string Address = "127.0.0.1"; // virtualbox
    private const int Port = 44000;
    // private Queue<Tuple<PacketId, ByteBuffer>> ReceivedQueue;
    // private Dictionary<PacketId, Action<ByteBuffer>> DispatchTable; 

    // Use this for initialization
    void Start()
    {
        // ReceivedQueue = new Queue<Tuple<Packet.Common.PacketId, ByteBuffer>>();
        // DispatchTable = new Dictionary<PacketId, Action<ByteBuffer>>();

        Client = new TcpClient();
        Debug.Log("TryConnection");
        Client.BeginConnect(Address, Port, new System.AsyncCallback(OnConnected), Client);

        var entity = new Entity();
        entity.playerId = 1000100;
        entity.x = 128.22f;
        entity.y = 54.129f;
        entity.name = "";
        entity.hp = 200000;

        BinaryFormatter bf = new BinaryFormatter();
        var memstream = new MemoryStream();
        bf.Serialize(memstream, entity);
        Debug.LogFormat("memstream len:{0}", memstream.ToArray().Length);
    }

    private void Connect()
    {
        try
        {
            Client.Connect(Address, Port);
            if (Client.Connected)
            {
                Debug.LogFormat("Connected");
                Recv();
                StartCoroutine("Recv");
            }
            else
            {
                Debug.LogFormat("Connect Failed");
                System.Threading.Thread.Sleep(Mathf.Max(0, ConnectTimeout - 1000));
                Client.BeginConnect(Address, Port, new System.AsyncCallback(OnConnected), Client);
            }
        }
        catch (Exception e)
        {
            Debug.LogErrorFormat("OnConnected Exception:{0}", e.ToString());
        }
    }

    private void OnConnected(System.IAsyncResult ar)
    {
        try
        {
            TcpClient result = (TcpClient)ar.AsyncState;
            if (result.Connected)
            {
                Debug.Log("OnConnected");
                Recv();
            }
            else
            {
                System.Threading.Thread.Sleep(Mathf.Max(0, ConnectTimeout - 1000));
                Client.BeginConnect(Address, Port, new System.AsyncCallback(OnConnected), Client);
            }
        }
        catch (Exception e)
        {
            // Debug.LogErrorFormat ("OnConnected Exception:{0}", e.ToString ());
        }
    }

    public void OnClickedSend()
    {
        if (!Client.Connected)
        {
            Debug.Log("NotConnected");
            return;
        }

        ushort pkid = 100;
        var header = BitConverter.GetBytes(pkid);
        // ??????????????????????????????????????????????????????????????????
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(header);
        }
        var str = "Hello World !! ??????????????????";

        var strbuff = System.Text.Encoding.UTF8.GetBytes(str);
        var fbuff = BitConverter.GetBytes(2.0f);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(fbuff);
        }

        var buffer = new byte[header.Length + fbuff.Length];
        Array.Copy(header, buffer, header.Length);
        // Array.Copy(strbuff, 0, buffer, header.Length, strbuff.Length);
        Array.Copy(fbuff, 0, buffer, header.Length, fbuff.Length);
        NetworkStream stream = Client.GetStream();
        stream.Write(buffer, 0, buffer.Length);
        // Debug.LogFormat("Send String. {0}", str);
        Debug.LogFormat("Send float. {0}", fbuff);
    }

    private void OnApplicationQuit()
    {
        if (Client != null)
        {
            Client.Close();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // while(ReceivedQueue.Count > 0)
        // {
        // 	var (packetId, data) = ReceivedQueue.Dequeue();
        // 	Action<ByteBuffer> callBack;
        // 	if(DispatchTable.TryGetValue(packetId, out callBack))
        // 	{
        // 		callBack(data);
        // 	}
        // }
    }

    public void FixedUpdate()
    {

    }

    public void Recv()
    {
        NetworkStream stream = Client.GetStream();
        stream.BeginRead(RecvBuffer, 0, ClientRecvBufferSize, new System.AsyncCallback(OnRecv), stream);
        int readBytes = stream.Read(RecvBuffer, 0, ClientRecvBufferSize);

        Debug.LogFormat("OnRecv readBytes: {0} ", readBytes);

        var headerBytes = RecvBuffer[0..2];
        var bodyBytes = RecvBuffer[2..readBytes];
        Array.Reverse(headerBytes);
        var packet_id = BitConverter.ToUInt16(headerBytes);
        Debug.LogFormat("packet_id:{0}", packet_id);

        // ??????????????????????????????
        var pid = BitConverter.ToUInt64(bodyBytes, 0);
        var x = BitConverter.ToSingle(bodyBytes, 8);
        var y = BitConverter.ToSingle(bodyBytes, 12);
        var len = BitConverter.ToUInt64(bodyBytes, 16);
        var strBytes = new byte[len];
        Array.Copy(bodyBytes, 24, strBytes, 0, (int)len);
        var name = System.Text.Encoding.UTF8.GetString(strBytes);
        var hp = BitConverter.ToUInt32(bodyBytes, 24 + (int)len);


        Debug.LogFormat("x:{0}, y:{1}, len:{2}, name:{3}, hp:{4}", x, y, len, name, hp);
        // var str = System.Text.Encoding.UTF8.GetString(bodyBytes);
        // Debug.LogFormat("string:{0}", str);


        // PacketHeader header = PacketHeader.HeaderDeserialize(RecvBuffer);
        // Debug.LogFormat("OnRecv:{0}", header.Head.ToString());
    }

    private void OnRecv(System.IAsyncResult ar)
    {
        NetworkStream stream = (NetworkStream)ar.AsyncState;
        int bytesRead = stream.EndRead(ar);
        if (bytesRead > 0)
        {
            Debug.LogFormat("bytesRead:{0}", bytesRead);
        }
        Recv();

    }
}