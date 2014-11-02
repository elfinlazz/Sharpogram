using System;
using System.Text;
using System.Windows;

using Telegram.API;

namespace Sharpogram
{
    public class ApiCallback : AbsApiCallback {

        override public void onAuthCancelled(TelegramApi telegramApi) {
            MessageBox.Show("onAuthCancelled");
        }

        override public void onUpdatesInvalidated(TelegramApi api) {
            MessageBox.Show("onUpdatesInvalidated");
        }

        override public void onUpdate(TLAbsUpdates tlAbsUpdates) {
            MessageBox.Show("onUpdate");
        }
    }
}
