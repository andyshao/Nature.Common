/**
 * 自然框架之共用类库
 * http://www.natureFW.com/
 *
 *
 * @author
 * 金洋（金色海洋jyk）
 * 
 * @copyright
 * Copyright (C) 2005-2013 金洋.
 *
 * Licensed under a GNU Lesser General Public License.
 * http://creativecommons.org/licenses/LGPL/2.1/
 *
 * Nature.Common is free software. You are allowed to download, modify and distribute 
 * the source code in accordance with LGPL 2.1 license, however if you want to use 
 * Nature.Common on your site or include it in your commercial software, you must be registered.
 * http://www.natureFW.com/registered
 * Nature.Common:自然框架之共用类库
 */

/* ***********************************************
 * author :  金洋（金色海洋jyk）
 * email  :  jyk0011@live.cn 
 * function: URL参数的处理，接收、验证和拼接
 * history:  created by 金洋  
 * **********************************************
 */

using System.Web;

namespace Nature.Common
{
    /// <summary>
    /// URL参数的处理，接收、验证和拼接
    /// </summary>
    /// user:jyk
    /// time:2013/2/6 15:40
    public class URLParamVerification
    {
        #region 验证URL参数的函数
        #region 一般页面 ID
        /// <summary>
        /// 验证记录ID。没传，或者ID不正确，设置为 string.Empty
        /// 可能不传ID的页面使用
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns></returns>
        /// user:jyk
        /// time:2012/11/13 10:00
        public static string DataID(HttpContext context)
        {
            //DataList.aspx、DataForm.aspx 页面通过URL里的参数设置。
            //其他页面自行设置
            string dataID = context.Request.QueryString["id"];
            if (dataID != null)
                dataID = dataID.Trim('"');
            else
                return "";
            
            if (!Functions.IsInt(dataID))
            {
                //没有传递，设置默认值
                dataID = string.Empty;
            }

            return dataID;
        }
        #endregion

        #region FormDataID

        /// <summary>
        /// 设置外键。通过URL参数 fpvid 获取。
        /// </summary>
        /// <param name="context"></param>
        /// <param name="paraName">url参数名称 </param>
        /// <returns></returns>
        /// user:jyk
        /// time:2012/11/13 10:00
        public static string FormDataID(HttpContext context, string paraName)
        {
            string tmpID = context.Request.QueryString[paraName];
            if (tmpID != null)
                tmpID = tmpID.Trim('"');
            else
                return "";
            
            //验证ID参数是否是数字。
            if (string.IsNullOrEmpty(tmpID))
            {
                //没有传递，设置默认值
                tmpID = string.Empty;
            }
            else
            {
                if (!Functions.IsInt(tmpID))
                {
                    if (!Functions.IsGuid(tmpID))
                    {
                        //没有传递，设置默认值
                        tmpID = string.Empty;
                    }

                }
            }
            return tmpID;

        }
        #endregion

        #region ModuleID
        /// <summary>
        /// 设置ModuleID。通过URL参数 mid 获取。
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// user:jyk
        /// time:2012/11/13 10:00
        public static int ModuleID(HttpContext context)
        {
            string tmpModuleID = context.Request.QueryString["mdid"];
            if (tmpModuleID != null)
                tmpModuleID = tmpModuleID.Trim('"');
            else
                return 0;

            //验证模块ID参数是否是数字。
            if (!Functions.IsInt(tmpModuleID))
            {
                context.Response.Write("模块mdid参数不正确！" + tmpModuleID);
                context.Response.End();
            }
            return int.Parse(tmpModuleID);

        }
        #endregion

        #region PageViewID

        /// <summary>
        /// 设置页面视图ID。通过URL参数 mpvid 获取。
        /// </summary>
        /// <param name="context"></param>
        /// <param name="paraName"> </param>
        /// <returns></returns>
        /// user:jyk
        /// time:2012/11/13 10:00
        public static int PageViewID(HttpContext context, string paraName)
        {
            string tmpPageViewID = context.Request.QueryString[paraName];
            if (tmpPageViewID != null)
                tmpPageViewID = tmpPageViewID.Trim('"');
            else
                return 0;

            //验证页面视图ID参数是否是数字。
            if (!Functions.IsInt(tmpPageViewID))
            {
                //Response.Write("页面视图mpvid参数不正确！" + tmpPageViewID);
                //Response.End();
                //没有传递，设置默认值
                return -9;
            }
            else
            {
                return int.Parse(tmpPageViewID);
            }
        }
        #endregion

        #region PageViewID

        /// <summary>
        /// 设置页面视图ID。通过URL参数 mpvid 获取。
        /// </summary>
        /// <param name="context"></param>
        /// <param name="paraName"> </param>
        /// <returns></returns>
        /// user:jyk
        /// time:2012/11/13 10:00
        public static string StringID(HttpContext context, string paraName)
        {
            string tmpID = context.Request.QueryString[paraName];
            if (tmpID != null)
                tmpID = tmpID.Trim('"');
            else
                return "";

            //验证页面视图ID参数是否是数字。
            if (!Functions.IsInt(tmpID))
            {
                //Response.Write("页面视图mpvid参数不正确！" + tmpPageViewID);
                //Response.End();
                //没有传递，设置默认值
                return "";
            }
            else
            {
                return tmpID;
            }
        }
        #endregion

        #endregion
    }
}
