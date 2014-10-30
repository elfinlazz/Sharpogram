using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Telegram.MTProto
{
    // Based on (@author Korshakov Stepan <me@ex3ndr.com> for Java)

    public abstract class AbsMTProtoState
    {

        public abstract byte[] getAuthKey();

        public abstract ConnectionInfo[] getAvailableConnections();

        public abstract KnownSalt[] readKnownSalts();

        protected abstract void writeKnownSalts(KnownSalt[] salts);

        public void mergeKnownSalts(int currentTime, KnownSalt[] salts) {
        KnownSalt[] knownSalts = readKnownSalts();
        Dictionary<long, KnownSalt> ids = new Dictionary<long, KnownSalt>();
        foreach (KnownSalt s in knownSalts) {
            if (s.getValidUntil() < currentTime) {
                continue;
            }
            ids.Add(s.getSalt(), s);
        }
        foreach (KnownSalt s in salts) {
            if (s.getValidUntil() < currentTime) {
                continue;
            }
            ids.Add(s.getSalt(), s);
        }
        writeKnownSalts((KnownSalt[]) ids.Values.ToArray());
    }

        public void addCurrentSalt(long salt)
        {
            int time = (int)(TimeOverlord.getInstance().getServerTime() / 1000);
            mergeKnownSalts(time, new KnownSalt[] { new KnownSalt(time, time + 30 * 60, salt) });
        }

        public void badServerSalt(long salt)
        {
            int time = (int)(TimeOverlord.getInstance().getServerTime() / 1000);
            writeKnownSalts(new KnownSalt[] { new KnownSalt(time, time + 30 * 60, salt) });
        }

        public void initialServerSalt(long salt)
        {
            int time = (int)(TimeOverlord.getInstance().getServerTime() / 1000);
            writeKnownSalts(new KnownSalt[] { new KnownSalt(time, time + 30 * 60, salt) });
        }

        public long findActualSalt(int time) {
        KnownSalt[] knownSalts = readKnownSalts();
        foreach (KnownSalt salt in knownSalts) {
            if (salt.getValidSince() <= time && time <= salt.getValidUntil()) {
                return salt.getSalt();
            }
        }

        return 0;
    }

        public int maximumCachedSalts(int time) {
        int count = 0;
        foreach (KnownSalt salt in readKnownSalts()) {
            if (salt.getValidSince() > time) {
                count++;
            }
        }
        return count;
    }

        public int maximumCachedSaltsTime() {
        int max = 0;
        foreach (KnownSalt salt in readKnownSalts()) {
            max = Math.Max(max, salt.getValidUntil());
        }
        return max;
    }
    }
}
