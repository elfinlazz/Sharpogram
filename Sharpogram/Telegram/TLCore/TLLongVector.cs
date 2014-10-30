using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.TLCore
{
    /**
     * TL Vector of longs. @see org.telegram.tl.TLVector
     *
     * Based on (@author Korshakov Stepan <me@ex3ndr.com> for Java)
     */
    public class TLLongVector : TLVector<long> {
        public TLLongVector() {
            setDestClass(typeof(long));
        }

        new public String toString() {
            return "vector<long>#1cb5c415";
        }
    }
}
