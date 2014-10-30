using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Telegram.MTProto
{

// Based on (@author Korshakov Stepan <me@ex3ndr.com> for Java)

    public class TimeOverlord {

        private static TimeOverlord instance;
        private static readonly DateTime Jan1st1970 = new DateTime
                                    (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static long CurrentTimeMillis() {
            return (long) (DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static TimeOverlord getInstance() {
            if (instance == null) {
                instance = new TimeOverlord();
            }
            return instance;
        }

        private long nanotimeShift;

        private long timeAccuracy = long.MaxValue;
        protected long timeDelta;

        private TimeOverlord() {
            nanotimeShift = CurrentTimeMillis() - DateTime.Now.Millisecond / 1000000000;
        }

        public long createWeakMessageId() {
            return (getServerTime() / 1000) << 32;
        }

        public long getLocalTime() {
            return CurrentTimeMillis();
        }

        public long getServerTime() {
            return getLocalTime() + timeDelta;
        }

        public long getTimeAccuracy() {
            return timeAccuracy;
        }

        public long getTimeDelta() {
            return timeDelta;
        }

        public void setTimeDelta(long timeDelta, long timeAccuracy) {
            this.timeDelta = timeDelta;
            this.timeAccuracy = timeAccuracy;
        }

        public void onForcedServerTimeArrived(long serverTime, long duration) {
            timeDelta = serverTime - getLocalTime();
            timeAccuracy = duration;
        }

        public void onServerTimeArrived(long serverTime, long duration) {
            if (duration < 0) {
                return;
            }
            if (duration < timeAccuracy) {
                timeDelta = serverTime - getLocalTime();
                timeAccuracy = duration;
            } else if (Math.Abs(getLocalTime() - serverTime) > (duration / 2 + timeAccuracy / 2)) {
                timeDelta = serverTime - getLocalTime();
                timeAccuracy = duration;
            }
        }

        public void onMethodExecuted(long sentId, long responseId, long duration) {
            if (duration < 0) {
                return;
            }

            onServerTimeArrived((responseId >> 32) * 1000, duration);
        }
    }
}
