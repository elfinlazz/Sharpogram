using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.TLCore
{
    
    /**
     * Basic class of gzipped object
     *
     * Based on (@author Korshakov Stepan <me@ex3ndr.com> for Java)
     */
    public class TLGzipObject : TLObject {
        public static readonly uint CLASS_ID = 0x3072CFA1;

        override public uint getClassId() {
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

        public override void serializeBody(/*OutputStream*/StreamWriter stream) {
            try {
                StreamingUtils.writeTLBytes(packedData, stream);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
            }
        }

        public override void deserializeBody(/*InputStream*/BufferedStream stream, TLContext context)
        {
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
