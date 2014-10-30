using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.MTProto
{
    // Based on (@author Korshakov Stepan <me@ex3ndr.com> for Java)

    public class MemoryProtoState : AbsMTProtoState {

        private KnownSalt[] salts = new KnownSalt[0];

        private String address;
        private int port;
        private byte[] authKey;

        public MemoryProtoState(byte[] authKey, String address, int port) {
            this.authKey = authKey;
            this.port = port;
            this.address = address;
        }

        override public byte[] getAuthKey() {
            return authKey;
        }

        override public ConnectionInfo[] getAvailableConnections() {
            return new ConnectionInfo[]{new ConnectionInfo(0, 0, address, port)};
        }

        override public KnownSalt[] readKnownSalts() {
            return salts;
        }

        override protected void writeKnownSalts(KnownSalt[] salts) {
            this.salts = salts;
        }
    }
}
