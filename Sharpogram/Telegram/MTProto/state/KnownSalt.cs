using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.MTProto 
{
    // Based on (@author Korshakov Stepan <me@ex3ndr.com> for Java)

    public class KnownSalt
    {
        private int validSince;
        private int validUntil;
        private long salt;

        public KnownSalt(int validSince, int validUntil, long salt)
        {
            this.validSince = validSince;
            this.validUntil = validUntil;
            this.salt = salt;
        }

        public int getValidSince()
        {
            return validSince;
        }

        public int getValidUntil()
        {
            return validUntil;
        }

        public long getSalt()
        {
            return salt;
        }
    }
}
