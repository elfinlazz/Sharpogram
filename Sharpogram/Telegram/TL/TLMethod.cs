using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.TL
{
    /**
     * Basic object for RPC methods. It contains special methods for deserializing result of RPC method call.
     *
     * @param <T> return type of method
     * Based on (@author Korshakov Stepan <me@ex3ndr.com> for Java)
     */
    public abstract class TLMethod<T> : TLObject where T : TLObject {
        public T deserializeResponse(byte[] data, TLContext context) {
            
            return deserializeResponse(new MemoryStream(data), context);
        }

        public abstract T deserializeResponse(/*InputStream*/MemoryStream stream, TLContext context);
    }
}
