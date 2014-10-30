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
     * @author Korshakov Stepan <me@ex3ndr.com> for Java
     */
    public class TLBoolTrue : TLObject {

        private const uint CLASS_ID = 0x997275b5;

        new public static uint getClassId() {
            return CLASS_ID;
        }

        public String toString() {
            return "boolTrue#997275b5";
        }
    }
}
