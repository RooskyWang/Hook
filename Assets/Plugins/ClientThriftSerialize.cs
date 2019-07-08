using System.IO;
using Thrift.Transport;
using Thrift.Protocol;

public class ClientThriftSerialize
{
    #region Sington
    private static ClientThriftSerialize instance = null;

    public static ClientThriftSerialize Instance
    {
        get
        {
            if (instance == null)
                instance = new ClientThriftSerialize();
            return instance;
        }
    }

    private ClientThriftSerialize()
    {
        Init();
    }
    #endregion

    private Stream outputStream = new MemoryStream(64);

    private Stream inputStream = new MemoryStream(64);

    private TProtocol serializeProtocol = null;

    private TProtocol deSerializeProtocol = null;

    private void Init()
    {
        // output is serialize
        TTransport serializeTransport = new TStreamTransport(null, outputStream);
        serializeProtocol = new TCompactProtocol(serializeTransport);

        // input is deSerialize
        TTransport deSerializeTransport = new TStreamTransport(inputStream, null);
        deSerializeProtocol = new TCompactProtocol(deSerializeTransport);
    }

    public byte[] Serialize(TBase tbase)
    {
        if (tbase == null) return null;

        // init output stream
        outputStream.Seek(0, SeekOrigin.Begin);
        outputStream.SetLength(0);

        // serialize object by protocol
        tbase.Write(serializeProtocol);

        // read serialize data
        byte[] bytes = new byte[outputStream.Length];
        outputStream.Position = 0;
        outputStream.Read(bytes, 0, bytes.Length);

        return bytes;
    }

    public void DeSerialize(TBase tbase, byte[] bytes)
    {
        if (tbase == null || bytes == null) return;
        DeSerialize(tbase, bytes, 0, bytes.Length);
    }

    public void DeSerialize(TBase tbase, byte[] bytes,int startPos,int length)
    {
        if (tbase == null || bytes == null) return;

        // init input stream
        inputStream.Seek(0, SeekOrigin.Begin);
        inputStream.SetLength(0);

        // write data to input stream
        inputStream.Write(bytes, startPos, length);
        inputStream.Seek(0, SeekOrigin.Begin);

        // deSerialize object by protocol
        tbase.Read(deSerializeProtocol);
    }

    public void CopyClass(TBase src, TBase tar)
    {
        if (src == null || tar == null) return;

        byte[] data = Serialize(src);
        DeSerialize(tar, data);
    }
}