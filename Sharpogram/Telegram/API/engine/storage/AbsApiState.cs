using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Telegram.MTProto;

namespace Telegram.API
{
    // Based on (@author Korshakov Stepan <me@ex3ndr.com> for Java)

    public interface AbsApiState
    {
        int getPrimaryDc();

        void setPrimaryDc(int dc);

        Boolean isAuthenticated(int dcId);

        void setAuthenticated(int dcId, Boolean auth);

        void updateSettings(TLConfig config);


        byte[] getAuthKey(int dcId);

        void putAuthKey(int dcId, byte[] key);

        ConnectionInfo[] getAvailableConnections(int dcId);

        AbsMTProtoState getMtProtoState(int dcId);

        void resetAuth();

        void reset();
    }
}
