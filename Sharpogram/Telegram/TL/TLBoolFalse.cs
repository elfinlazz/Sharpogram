using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.TL
{
    /**
     * Packed type of tl-bool false value
     *
     * @author Korshakov Stepan <me@ex3ndr.com> for Java
     */
    public class TLBoolFalse : TLObject {

        public const uint CLASS_ID = 0xbc799737;

        new public static uint getClassId() {
            return CLASS_ID;
        }

        public String toString() {
            return "boolFalse#bc799737";
        }
    }
}
