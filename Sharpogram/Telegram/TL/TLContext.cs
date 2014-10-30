using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace Telegram.TL
{
    /**
 * TypeLanguage context object. It performs deserialization of objects and vectors.
 * All known classes might be registered in context for deserialization.
 * Often this might be performed from inherited class in init() method call.
 * If TL-Object contains static int field CLASS_ID, then it might be used for registration,
 * but it uses reflection so it might be slow in some cases. It recommended to manually pass CLASS_ID
 * to registerClass method.
 *
 * Based on (@author Korshakov Stepan <me@ex3ndr.com> for Java)
 */
public abstract class TLContext {
    private  Dictionary<int, Type> registeredClasses = new Dictionary<int, Type>();
    private  Dictionary<int, Type> registeredCompatClasses = new Dictionary<int, Type>();

    public TLContext() {
        init();
    }

    protected void init() {

    }

    public Boolean isSupportedObject(TLObject obj) {
        return isSupportedObject(obj.getClassId());
    }

    public Boolean isSupportedObject(uint classId) {
        return registeredClasses.ContainsKey((int)classId);
    }

    public void registerClass<T>(Type tClass) {
        try {
            if(tClass == typeof(TLObject)) {
                uint classId = (uint)typeof(T).GetField("CLASS_ID").GetValue(null);
                registeredClasses.Add((int)classId, tClass);
            }

        } catch (Exception e) {
            System.Diagnostics.Debug.WriteLine(e.StackTrace);
        }
    }

    public  void registerClass<T>(int clazzId, Type tClass) {
        registeredClasses.Add(clazzId, tClass);
        
    }

    public void registerCompatClass<T>(Type tClass) where T : TLObject {
        try {
            int classId = (int)tClass.GetField("CLASS_ID").GetValue(null);
            registeredCompatClasses.Add(classId, tClass);
        } catch (Exception e) {
            System.Diagnostics.Debug.WriteLine(e.StackTrace);
        }
    }

    public  void registerCompatClass<T>(int clazzId, Type tClass) {
        registeredCompatClasses.Add(clazzId, tClass);
    }

    protected TLObject convertCompatClass(TLObject src) {
        return src;
    }

    public TLObject deserializeMessage(byte[] data) {
        return deserializeMessage(new BufferedStream(new MemoryStream(data)));
    }

    public TLObject deserializeMessage(int clazzId, /*InputStream*/BufferedStream stream) {
        try {
            if (clazzId == TLGzipObject.getClassId()) {
                TLGzipObject obj = new TLGzipObject();
                obj.deserializeBody(stream, this);
                BufferedStream gzipInputStream = new BufferedStream(new GZipStream(new MemoryStream(obj.getPackedData()), CompressionMode.Decompress));
                int innerClazzId = StreamingUtils.readInt(gzipInputStream);
                return deserializeMessage(innerClazzId, gzipInputStream);
            }

            if (clazzId == TLBoolTrue.getClassId()) {
                return new TLBoolTrue();
            }

            if (clazzId == TLBoolFalse.getClassId()) {
                return new TLBoolFalse();
            }

            if (registeredCompatClasses.ContainsKey(clazzId)) {
                try {
                    Type messageClass = (Type) registeredCompatClasses[clazzId];
                    //TLObject message = (TLObject) messageClass.getConstructor().newInstance();
                    TLObject message = (TLObject) Activator.CreateInstance(messageClass); 
                    message.deserializeBody(stream, this);
                    return convertCompatClass(message);
                } catch (Exception e) {
                    System.Diagnostics.Debug.WriteLine(e.StackTrace);
                    throw new IOException("Unable to deserialize data");
                }
            }

            try {
                Type messageClass = (Type) registeredClasses[clazzId];
                if (messageClass != null) {
                    TLObject message = (TLObject) Activator.CreateInstance(messageClass); 
                    message.deserializeBody(stream, this);
                    return message;
                } else {
                    throw new /*DeserializeException*/Exception("Unsupported class: #" + clazzId.ToString("X"));
                }
            } catch (Exception e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw new IOException("Unable to deserialize data");
            }
        } catch (Exception e) {
            System.Diagnostics.Debug.WriteLine(e.StackTrace);
            throw e;
        }
    }

    public TLObject deserializeMessage(/*InputStream*/BufferedStream stream) {
        try{
            int clazzId = StreamingUtils.readInt(stream);
            return deserializeMessage(clazzId, stream);
        } catch(IOException e) {
            System.Diagnostics.Debug.WriteLine(e.StackTrace);
            throw e;
        }
    }

    public /*TLVector*/TLObject deserializeVector<T>(/*InputStream*/BufferedStream stream) {
        try {
//            int clazzId = StreamingUtils.readInt(stream);
            byte[] byte_clazzId = new byte[] { };
            stream.Read(byte_clazzId, 0, (int)stream.Length);
            int clazzId = Convert.ToInt32(byte_clazzId);
            if (clazzId == TLVector<Type>.getClassId()) {
//                TLVector res = new TLVector();
                TLVector<T> res = new TLVector<T>();
                res.deserializeBody(stream, this);
                return res;
            } else if (clazzId == TLGzipObject.getClassId()) {
                TLGzipObject obj = new TLGzipObject();
                obj.deserializeBody(stream, this);
                BufferedStream gzipInputStream = new /*BufferedInputStream*/BufferedStream(new GZipStream(new /*ByteArrayInputStream*/MemoryStream(obj.getPackedData()), CompressionMode.Decompress));
                return deserializeVector<T>(gzipInputStream);
            } else {
                throw new IOException("Unable to deserialize vector");
            }
        } catch(IOException e) {
            System.Diagnostics.Debug.WriteLine(e.StackTrace);
            throw e;
        }
    }

    public TLIntVector deserializeIntVector(/*InputStream*/BufferedStream stream) {
        try {
//            int clazzId = StreamingUtils.readInt(stream);
            byte[] byte_clazzId = new byte[] { };
            stream.Read(byte_clazzId, 0, (int)stream.Length);
            int clazzId = Convert.ToInt32(byte_clazzId);

            if (clazzId == TLVector<int>.getClassId()) {
                TLIntVector res = new TLIntVector();
                res.deserializeBody(stream, this);
                return res;
            } else if (clazzId == TLGzipObject.getClassId()) {
                TLGzipObject obj = new TLGzipObject();
                obj.deserializeBody(stream, this);
                BufferedStream gzipInputStream = new /*BufferedInputStream*/BufferedStream(new GZipStream(new /*ByteArrayInputStream*/MemoryStream(obj.getPackedData()), CompressionMode.Decompress));
                return deserializeIntVector(gzipInputStream);
            } else {
                throw new IOException("Unable to deserialize vector");
            }
        } catch(IOException e) {
            System.Diagnostics.Debug.WriteLine(e.StackTrace);
            throw e;
        }
    }

    public TLLongVector deserializeLongVector(/*InputStream*/BufferedStream stream) {
        try {
            int clazzId = StreamingUtils.readInt(stream);
            if (clazzId == TLVector<long>.getClassId()) {
                TLLongVector res = new TLLongVector();
                res.deserializeBody(stream, this);
                return res;
            } else if (clazzId == TLGzipObject.getClassId()) {
                TLGzipObject obj = new TLGzipObject();
                obj.deserializeBody(stream, this);
                BufferedStream gzipInputStream = new /*BufferedInputStream*/BufferedStream(new GZipStream(new /*ByteArrayInputStream*/MemoryStream(obj.getPackedData()), CompressionMode.Decompress));
                return deserializeLongVector(gzipInputStream);
            } else {
                throw new IOException("Unable to deserialize vector");
            }
        } catch(IOException e) {
            System.Diagnostics.Debug.WriteLine(e.StackTrace);
            throw e;
        }
    }

    public TLStringVector deserializeStringVector(/*InputStream*/BufferedStream stream) {
        try {
            int clazzId = StreamingUtils.readInt(stream);
            if (clazzId == TLVector<String>.getClassId()) {
                TLStringVector res = new TLStringVector();
                res.deserializeBody(stream, this);
                return res;
            } else if (clazzId == TLGzipObject.getClassId()) {
                TLGzipObject obj = new TLGzipObject();
                obj.deserializeBody(stream, this);
                BufferedStream gzipInputStream = new /*BufferedInputStream*/BufferedStream(new GZipStream(new /*ByteArrayInputStream*/MemoryStream(obj.getPackedData()), CompressionMode.Decompress));
                return deserializeStringVector(gzipInputStream);
            } else {
                throw new IOException("Unable to deserialize vector");
            }
        } catch(IOException e) {
            System.Diagnostics.Debug.WriteLine(e.StackTrace);
            throw e;
        }
    }

    public TLBytes allocateBytes(int size) {
        return new TLBytes(new byte[size], 0, size);
    }

    public void releaseBytes(TLBytes unused) {

    }
}
}
