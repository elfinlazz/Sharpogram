using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.TLCore
{
    /**
     * Packed type of tl-bool false value
     *
     * @author Korshakov Stepan <me@ex3ndr.com> for Java
     */
    public class TLBoolFalse : TLBool {

        public static readonly Int64 CLASS_ID = 0xbc799737;

        override public Int64 getClassId()
        {
            return CLASS_ID;
        }

        public String toString() {
            return "boolFalse#bc799737";
        }

        public override void serializeBody(System.IO.StreamWriter stream) { }
        public override void deserializeBody(System.IO.BufferedStream stream, TLContext context) { }
    }
}
