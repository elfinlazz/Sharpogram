using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.TLCore
{
    /**
     * Packed type of tl-bool true value
     *
     * Based on (@author Korshakov Stepan <me@ex3ndr.com> for Java)
     */
    public class TLBoolTrue : TLBool {

        public static readonly Int64 CLASS_ID = 0x997275b5;

        override public Int64 getClassId()
        {
            return CLASS_ID;
        }

        public String toString() {
            return "boolTrue#997275b5";
        }

        public override void serializeBody(System.IO.StreamWriter stream) { }
        public override void deserializeBody(System.IO.BufferedStream stream, TLContext context) { }
    }
}
