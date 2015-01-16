using System.Configuration;
using System.Globalization;
using System.Web;

namespace Nature.SsoConfig
{

    #region 配置信息类

    /// <summary>
    /// 网站应用端的配置信息类
    /// </summary>
    /// user:jyk
    /// time:2013/1/30 8:50
    public static class SsoInfo
    {
        /// <summary>
        /// 返回统一配置的网站应用ID
        /// </summary>
        /// user:jyk
        /// time:2013/1/30 8:52
        public static string WebAppID
        { 
            get { return ConfigurationManager.AppSettings["WebappID"];  }
        }

        /// <summary>
        /// sso验证的网址，不带斜杠
        /// </summary>
        /// user:jyk
        /// time:2013/1/30 8:52
        public static string SSOUrl
        {
            get
            {
                string tmpUrl = ConfigurationManager.AppSettings["SSOURL"];
                if (string.IsNullOrEmpty(tmpUrl))
                {
                    //取本站的网址，用于快捷部署
                    tmpUrl = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port.ToString(CultureInfo.InvariantCulture);
                }
                return tmpUrl;
            }
        }

        /// <summary>
        /// 元数据服务的网址
        /// </summary>
        /// user:jyk
        /// time:2013/1/30 8:52
        public static string ResourceURL
        {
            get
            {
                string tmpUrl = ConfigurationManager.AppSettings["ResourceURL"];
                if (string.IsNullOrEmpty(tmpUrl))
                {
                    //取本站的网址，用于快捷部署
                    tmpUrl = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port.ToString(CultureInfo.InvariantCulture);
                }
                return tmpUrl;
            }
        }

        /// <summary>
        /// 数据增删改查服务的网址
        /// </summary>
        /// user:jyk
        /// time:2013/1/30 8:52
        public static string DataServiceUrl
        {
            get
            {
                string tmpUrl = ConfigurationManager.AppSettings["DataServiceURL"];
                if (string.IsNullOrEmpty(tmpUrl))
                {
                    //如果没有设置，取元数据的服务地址
                    tmpUrl = ConfigurationManager.AppSettings["MetaServiceURL"];
                }

                if (string.IsNullOrEmpty(tmpUrl))
                {
                    tmpUrl = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port.ToString(CultureInfo.InvariantCulture);
                }
                return tmpUrl;
            }
        }

        /// <summary>
        /// 解密网站应用的票据的密钥
        /// </summary>
        /// user:jyk
        /// time:2013/1/30 8:52
        public static string AppKey
        {
            get
            {
                if (ConfigurationManager.AppSettings["AppKey"] == null)
                    return "-1";
                return ConfigurationManager.AppSettings["AppKey"];
            }
        }

        /// <summary>
        /// ajax登录时，客户端是否显示操作日志。True 显示；False不显示
        /// </summary>
        /// user:jyk
        /// time:2013/1/30 8:52
        public static string IsWriteAjaxDebug
        {
            get { return ConfigurationManager.AppSettings["Debug"]; }
        }

        /// <summary>
        /// 服务器端是否写操作日志。True 写；False不写
        /// </summary>
        /// user:jyk
        /// time:2013/1/30 8:52
        public static bool IsWriteSsoLog
        {
            get
            {
                if (ConfigurationManager.AppSettings["DebugSsoLog"] == null)
                    return false;
                return ConfigurationManager.AppSettings["DebugSsoLog"] == "True";
            }
        }

    }

    #endregion
     
}