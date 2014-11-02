using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Telegram.TLCore;

namespace Telegram.API
{
    public class TLConfig : TLObject
    {
        public static readonly Int64 CLASS_ID = 777313652;
        protected Int64 date;
        protected Boolean testMode;
        protected Int64 thisDc;
        protected TLVector<TLDcOption> dcOptions;
        protected Int64 chatSizeMax;
        protected Int64 broadcastSizeMax;
  
        public TLConfig() {}

        public TLConfig(Int64 _date, Boolean _testMode, Int64 _thisDc, TLVector<TLDcOption> _dcOptions, Int64 _chatSizeMax, Int64 _broadcastSizeMax)
        {
            this.date = _date;
            this.testMode = _testMode;
            this.thisDc = _thisDc;
            this.dcOptions = _dcOptions;
            this.chatSizeMax = _chatSizeMax;
            this.broadcastSizeMax = _broadcastSizeMax;
        }

        public override Int64 getClassId()
        {
            return 777313652;
        }

        public Int64 getDate()
        {
            return this.date;
        }
  
        public void setDate(int value)
        {
            this.date = value;
        }
  
        public Boolean getTestMode()
        {
            return this.testMode;
        }
  
        public void setTestMode(Boolean value)
        {
            this.testMode = value;
        }

        public Int64 getThisDc()
        {
            return this.thisDc;
        }
  
        public void setThisDc(int value)
        {
            this.thisDc = value;
        }
  
        public TLVector<TLDcOption> getDcOptions()
        {
            return this.dcOptions;
        }
  
        public void setDcOptions(TLVector<TLDcOption> value)
        {
            this.dcOptions = value;
        }

        public Int64 getChatSizeMax()
        {
            return this.chatSizeMax;
        }
  
        public void setChatSizeMax(int value)
        {
            this.chatSizeMax = value;
        }

        public Int64 getBroadcastSizeMax()
        {
            return this.broadcastSizeMax;
        }
  
        public void setBroadcastSizeMax(int value)
        {
            this.broadcastSizeMax = value;
        }
  
        public override void serializeBody(StreamWriter stream)
        {
            try {
                StreamingUtils.writeInt((uint)this.date, stream);
                StreamingUtils.writeTLBool(this.testMode, stream);
                StreamingUtils.writeInt((uint)this.thisDc, stream);
                StreamingUtils.writeTLVector(this.dcOptions, stream);
                StreamingUtils.writeInt((uint)this.chatSizeMax, stream);
                StreamingUtils.writeInt((uint)this.broadcastSizeMax, stream);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
            }
        }
  
        public override void deserializeBody(BufferedStream stream, TLContext context)
        {
            try {
                this.date = StreamingUtils.readInt(stream);
                this.testMode = StreamingUtils.readTLBool(stream);
                this.thisDc = StreamingUtils.readInt(stream);
                this.dcOptions = (TLVector<TLDcOption>)StreamingUtils.readTLVector(stream, context);
                this.chatSizeMax = StreamingUtils.readInt(stream);
                this.broadcastSizeMax = StreamingUtils.readInt(stream);
            } catch(IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
            }
        }
  
        public String toString()
        {
            return "config#2e54dd74";
        }
    }
}
