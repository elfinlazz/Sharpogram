using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Telegram.TLCore;

namespace Telegram.API
{
    public class TLDcOption : TLObject
    {
        public static readonly Int64 CLASS_ID = 784507964;
        protected Int64 id;
        protected String hostname;
        protected String ipAddress;
        protected Int64 port;
  
        public TLDcOption() {}

        public TLDcOption(Int64 _id, String _hostname, String _ipAddress, Int64 _port)
        {
            this.id = _id;
            this.hostname = _hostname;
            this.ipAddress = _ipAddress;
            this.port = _port;
        }

        public override Int64 getClassId()
        {
            return 784507964;
        }

        public Int64 getId()
        {
            return this.id;
        }
  
        public void setId(int value)
        {
            this.id = value;
        }
  
        public String getHostname()
        {
            return this.hostname;
        }
  
        public void setHostname(String value)
        {
            this.hostname = value;
        }
  
        public String getIpAddress()
        {
            return this.ipAddress;
        }
  
        public void setIpAddress(String value)
        {
            this.ipAddress = value;
        }

        public Int64 getPort()
        {
            return this.port;
        }
  
        public void setPort(int value)
        {
            this.port = value;
        }
  
        public override void serializeBody(StreamWriter stream)
        {
            try {
                StreamingUtils.writeInt((uint)this.id, stream);
                StreamingUtils.writeTLString(this.hostname, stream);
                StreamingUtils.writeTLString(this.ipAddress, stream);
                StreamingUtils.writeInt((uint)this.port, stream);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
            }
        }
  
        public override void deserializeBody(BufferedStream stream, TLContext context)
        {
            try {
                this.id = StreamingUtils.readInt(stream);
                this.hostname = StreamingUtils.readTLString(stream);
                this.ipAddress = StreamingUtils.readTLString(stream);
                this.port = StreamingUtils.readInt(stream);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
            }
        }
  
        public String toString()
        {
        return "dcOption#2ec2a43c";
        }
    }
}
