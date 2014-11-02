using System;
using System.Collections.Generic;
using System.Text;

using Telegram.API;

namespace Telegram.MTProto
{
    class MTProtoApiState : AbsMTProtoState {
        private KnownSalt[] knownSalts = new KnownSalt[0];
        private int dcId = 0;

        public MTProtoApiState(int _dcId) {
            _dcId = dcId;
        }

        override public byte[] getAuthKey() {
            return new ApiState().getAuthKey(dcId);
        }

        override public ConnectionInfo[] getAvailableConnections() {
            return new ApiState().getAvailableConnections(dcId);
        }

        override public KnownSalt[] readKnownSalts() {
            return knownSalts;
        }

        override protected void writeKnownSalts(KnownSalt[] salts) {
            knownSalts = salts;
        }
    }
}
