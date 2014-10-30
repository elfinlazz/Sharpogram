using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.MTProto
{

    // Based on (@author Korshakov Stepan <me@ex3ndr.com> for Java)

    public class Logger {

        public static readonly Boolean LOG_THREADS = true;
        public static readonly Boolean LOG_IGNORED = true;
        public static readonly Boolean LOG_PING    = true;

        private static LogInterface logInterface;

        public static void registerInterface(LogInterface logInterface) {
            Logger.logInterface = logInterface;
        }

        public static void w(String tag, String message) {
            if (logInterface != null) {
                logInterface.w(tag, message);
            } else {
                System.Diagnostics.Debug.WriteLine(tag + ":" + message);
            }
        }

        public static void d(String tag, String message) {
            if (logInterface != null) {
                logInterface.d(tag, message);
            } else {
                System.Diagnostics.Debug.WriteLine(tag + ":" + message);
            }
        }

        public static void e(String tag, Exception t) {
            if (logInterface != null) {
                logInterface.e(tag, t);
            } else {
                System.Diagnostics.Debug.WriteLine(t.StackTrace);
            }
        }
    }
}
