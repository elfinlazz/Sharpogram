using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Telegram.TLCore
{
    /**
    * Based on (@author Korshakov Stepan <me@ex3ndr.com> for Java)
    */

    public class TLBytes
    {
        private byte[] data;
        private int offset;
        private int len;

        public TLBytes(byte[] data)
        {
            this.data = data;
            this.offset = 0;
            this.len = data.Length;
        }

        public TLBytes(byte[] data, int offset, int len)
        {
            this.data = data;
            this.offset = offset;
            this.len = len;
        }

        public byte[] getData()
        {
            return data;
        }

        public int getOffset()
        {
            return offset;
        }

        public int getLength()
        {
            return len;
        }

        public byte[] cleanData()
        {
            if (offset == 0 && len == data.Length)
            {
                return data;
            }
            byte[] result = new byte[len];
            /*System.arraycopy(data, offset, result, 0, len);*/
            Array.ConstrainedCopy(data, offset, result, 0, len);
            
            return result;
        }
/*
        public String ToTLString(byte[] t_data, int t_offset, int t_len) {
            return (new MemoryStream(t_data, t_offset, t_len)).ToString();
        }
 */
    }
}
