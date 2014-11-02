using System;
using System.Text;

using Telegram.TLCore;

namespace Telegram.API
{
    public class TLApiContext : TLContext
    {
        protected void init()
        {
            registerClass(784507964, typeof(TLDcOption));
            registerClass(777313652, typeof(TLConfig));
        }
    }
}
