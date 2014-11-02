using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Telegram.MTProto;
using Telegram.API;

namespace Sharpogram
{
    public class ApiState : AbsApiState {

        private Dictionary<Int64, ConnectionInfo[]> connections = new Dictionary<Int64, ConnectionInfo[]>();
        private Dictionary<Int64, byte[]> keys = new Dictionary<Int64, byte[]>();
        private Dictionary<Int64, Boolean> isAuth = new Dictionary<Int64, Boolean>();

        private int primaryDc = 1;

        public ApiState() {
        }

        public ApiState(Boolean isTest) {
            connections.Add(1, new ConnectionInfo[]{
                new ConnectionInfo(1, 0, isTest ? "149.154.167.40" : "149.154.167.50", 443)
            });
        }

        public ApiState(ConnectionInfo[] connections) {
            this.connections.Add(1, connections);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        /*override */public int getPrimaryDc() {
            return primaryDc;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        /*override */public void setPrimaryDc(int dc) {
            primaryDc = dc;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        /*override */public Boolean isAuthenticated(int dcId) {
            if (isAuth.ContainsKey(dcId)) {
                return isAuth[dcId];
            }
            return false;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        /*override */public void setAuthenticated(int dcId, Boolean auth) {
            isAuth.Add(dcId, auth);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        /*override */public void updateSettings(TLConfig config) {
            connections.Clear();
            Dictionary<Int64, List<ConnectionInfo>> tConnections = new Dictionary<Int64, List<ConnectionInfo>>();
            int id = 0;
            foreach (TLDcOption option in config.getDcOptions()) {
                if (!tConnections.ContainsKey(option.getId())) {
                    tConnections.Add(option.getId(), new List<ConnectionInfo>());
                }
                tConnections[option.getId()].Add(new ConnectionInfo(id++, 0, option.getIpAddress(), option.getPort()));
            }

            foreach (int dc in tConnections.Keys) {
                List<ConnectionInfo> connectionInfo;
                tConnections.TryGetValue(dc, out connectionInfo);
                connections.Add(dc, connectionInfo.ToArray());
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        /*override */public byte[] getAuthKey(int dcId) {
            return keys[dcId];
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        /*override */public void putAuthKey(int dcId, byte[] key) {
            keys.Add(dcId, key);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        /*override */public  ConnectionInfo[] getAvailableConnections(int dcId) {
            if (!connections.ContainsKey(dcId)) {
                return new ConnectionInfo[0];
            }

            return connections[dcId];
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        /*override */public  AbsMTProtoState getMtProtoState(int dcId) {
            return new MTProtoApiState(dcId);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        /*override */public  void resetAuth() {
            isAuth.Clear();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        /*override */public void reset() {
            isAuth.Clear();
            keys.Clear();
        }
    }
}
