using System;

namespace Telegram.MTProto
{

    // Based on (@author Korshakov Stepan <me@ex3ndr.com> for Java)

    public interface LogInterface
    {
        void w(String tag, String message);

        void d(String tag, String message);

        void e(String tag, Exception t);
    }
}
