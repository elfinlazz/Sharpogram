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
        private Int64 id;
        private Int64 priority;
        private String address;
        private Int64 port;

        public ConnectionInfo(Int64 id, Int64 priority, String address, Int64 port)
        {
            this.id = id;
            this.priority = priority;
            this.address = address;
            this.port = port;
        }

        public Int64 getPriority()
        {
            return priority;
        }

        public String getAddress()
        {
            return address;
        }

        public Int64 getPort()
        {
            return port;
        }

        public Int64 getId()
        {
            return id;
        }
    }
}
