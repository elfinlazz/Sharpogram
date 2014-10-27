using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.TL
{
    
/**
 * Basic class of gzipped object
 *
 * Based on (@author Korshakov Stepan <me@ex3ndr.com> for Java)
 */
public class TLGzipObject : TLObject {
    public static const int CLASS_ID = 0x3072CFA1;

    public int getClassId() {
        return CLASS_ID;
    }

    public TLGzipObject(byte[] packedData) {
        this.packedData = packedData;
    }

    public TLGzipObject() {

    }

    private byte[] packedData;

    public byte[] getPackedData() {
        return packedData;
    }

    public void setPackedData(byte[] packedData) {
        this.packedData = packedData;
    }

    public void serializeBody(/*OutputStream*/StreamWriter stream) {
        try {
            StreamingUtils.writeTLBytes(packedData, stream);
        } catch(IOException e) {
            System.Diagnostics.Debug.WriteLine(e.StackTrace);
        }
    }

    public void deserializeBody(/*InputStream*/StreamReader stream, TLContext context) {
        try {
            packedData = StreamingUtils.readTLBytes(stream);
        } catch(IOException e) {
            System.Diagnostics.Debug.WriteLine(e.StackTrace);
        }
    }

    public String toString() {
        return "gzip_packed#3072cfa1";
    }
}
}
