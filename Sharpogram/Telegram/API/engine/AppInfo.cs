using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.API
{
    // Based on (@author Korshakov Stepan <me@ex3ndr.com> for Java)

    public class AppInfo
    {
        protected int apiId;
        protected String deviceModel;
        protected String systemVersion;
        protected String appVersion;
        protected String langCode;
        protected String apiHash;

        public AppInfo(int apiId, String deviceModel, String systemVersion, String appVersion, String langCode)
        {
            this.apiId = apiId;
            this.apiHash = "";
            this.deviceModel = deviceModel;
            this.systemVersion = systemVersion;
            this.appVersion = appVersion;
            this.langCode = langCode;
        }

        public AppInfo(int apiId, String apiHash, String deviceModel, String systemVersion, String appVersion, String langCode)
        {
            this.apiId = apiId;
            this.apiHash = apiHash;
            this.deviceModel = deviceModel;
            this.systemVersion = systemVersion;
            this.appVersion = appVersion;
            this.langCode = langCode;
        }

        public int getApiId()
        {
            return apiId;
        }

        public String getDeviceModel()
        {
            return deviceModel;
        }

        public String getSystemVersion()
        {
            return systemVersion;
        }

        public String getAppVersion()
        {
            return appVersion;
        }

        public String getLangCode()
        {
            return langCode;
        }


        public String getApiHash()
        {
            return apiHash;
        }
    }
}
