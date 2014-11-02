using System;
using System.Text;

namespace Telegram.API
{
    public interface AbsApiCallback
    {
        public void onAuthCancelled(TelegramApi api);

        public void onUpdatesInvalidated(TelegramApi api);

        public void onUpdate(TLAbsUpdates updates);
    }
}