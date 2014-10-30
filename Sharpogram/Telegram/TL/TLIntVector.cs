using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.TL
{
    /**
     * TL Vector of integers. @see org.telegram.tl.TLVector
     *
     * Based on (@author Korshakov Stepan <me@ex3ndr.com> for Java)
     */
    public class TLIntVector : TLVector<int> {
        public TLIntVector() {
            setDestClass(typeof(int));
        }

        new public String toString() {
            return "vector<int>#1cb5c415";
        }

        public int[] toIntArray() {
            int[] res = new int[size()];
            for (int i = 0; i < res.Length; i++) {
                res[i] = get(i);
            }
            return res;
        }
    }
}
