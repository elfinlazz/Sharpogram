using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.TL
{
    /**
     * TL Vector of strings. @see org.telegram.tl.TLVector
     *
     * Based on (@author Korshakov Stepan <me@ex3ndr.com> for Java)
     */
    public class TLStringVector : TLVector<String> {
        public TLStringVector() {
            setDestClass(typeof(String));
        }

        new public String toString() {
            return "vector<string>#1cb5c415";
        }
    }
}
