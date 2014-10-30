using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.MTProto
{
    // Based on (@author Korshakov Stepan <me@ex3ndr.com> for Java)

    public class ConnectionInfo
    {
        private int id;
        private int priority;
        private String address;
        private int port;

        public ConnectionInfo(int id, int priority, String address, int port)
        {
            this.id = id;
            this.priority = priority;
            this.address = address;
            this.port = port;
        }

        public int getPriority()
        {
            return priority;
        }

        public String getAddress()
        {
            return address;
        }

        public int getPort()
        {
            return port;
        }

        public int getId()
        {
            return id;
        }
    }
}
