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
 * function: 一些常用的、简单的函数的集合
 * history:  created by 金洋  
 * **********************************************
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

namespace Nature.Common
{
    /// <summary>
    /// 常用函数。
    /// </summary>
    public sealed class Functions
    {
        /// <summary>
        /// PageRegisterAlert 函数里使用的。区分不同的输出
        /// </summary>
        static Int32 _index;

        //字符串处理
        #region 截取字符串 StringCut
        /// <summary>
        /// 截取字符串。保证中英文混合的字符串，在截取后字符串的占位长度一致。
        /// </summary>
        /// <param name="value">要截取的字符串</param>
        /// <param name="length">保留的字节数，一个汉字按照两个字节计算</param>
        /// <returns>截取后的字符串</returns>
        public static string StringCut(string value, int length)
        {
            Byte[] tempStr =  Encoding.Default.GetBytes(value);
            if (tempStr.Length > length)
                return  Encoding.Default.GetString(tempStr, 0, length - 2) + "..";

            return value;
        }

        #endregion

        #region 过滤掉 HTML标签 StripHtml
        /// <summary>
        /// 过滤HTML标签。
        /// </summary>
        /// <param name="strHtml">要过滤的HTML字符串</param>
        /// <returns></returns>
        public static string StripHtml(string strHtml)
        {
            string[] aryReg ={
								  @"<script[^>]*?>.*?</script>",

								  @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
								  @"([\r\n])[\s]+",
								  @"&(quot|#34);",
								  @"&(amp|#38);",
								  @"&(lt|#60);",
								  @"&(gt|#62);", 
								  @"&(nbsp|#160);", 
								  @"&(iexcl|#161);",
								  @"&(cent|#162);",
								  @"&(pound|#163);",
								  @"&(copy|#169);",
								  @"&#(\d+);",
								  @"-->",
								  @"<!--.*\n"
							  };

            string[] aryRep = {
								   "",
								   "",
								   "",
								   "\"",
								   "&",
								   "<",
								   ">",
								   " ",
								   "\xa1",//chr(161),
								   "\xa2",//chr(162),
								   "\xa3",//chr(163),
								   "\xa9",//chr(169),
								   "",
								   "\r\n",
								   ""
							   };

            //string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                var regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, aryRep[i]);
            }
            //strOutput.Replace("<", "");
            //strOutput.Replace(">", "");
            strOutput = strOutput.Replace("\r\n", "");
            return strOutput;
        }
        #endregion

        #region 全角半角转换 ToDBC ToSBC
        /// <summary> 转半角的函数(DBC case) </summary>
        /// <param name="value">要转换成半角的字符串</param>
        /// <returns>半角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        public static string ToDBC(string value)
        {
            char[] c = value.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        /// <summary>
        /// 转全角的函数(SBC case)
        /// </summary>
        /// <param name="value">要转成全角的字符串</param>
        /// <returns>全角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        public static string ToSBC(string value)
        {
            //半角转全角：
            char[] c = value.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }



        #endregion

        #region MD5处理字符串 ToMD5
        /// <summary>
        /// 传入明文，返回用MD5处理后的字符串
        /// </summary>
        /// <param name="value">要处理的字符串</param>
        /// <returns>用MD5处理后的字符串</returns>
        public static string ToMD5(string value)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(value, "md5");
        }
        #endregion

        #region 字符串前面补充0，用来占位
        /// <summary>
        /// 字符串前面补充0，用来占位
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <param name="length">补充后的长度</param>
        /// <returns></returns>
        public static string FillZero(string str, int length)
        {
            string re = str;

            for (int i = str.Length; i < length;i++ )
            {
                re = "0" + re;
            }
            return re;
        }
        #endregion

        #region TimeSpan转换成带小数的毫秒
        /// <summary>
        /// TimeSpan转换成带小数的毫秒
        /// </summary>
        /// <param name="ts">The ts.</param>
        /// <returns></returns>
        /// user:jyk
        /// time:2012/10/10 19:04
        public static string TimeSpantoFloat(TimeSpan ts)
        {
            float f;
            try
            {
                //由于某些原因，可能会超出float的有效范围
                f = float.Parse(ts.ToString().Replace("00:00:0", ""));
                f = f*1000;
            }
            catch
            {
                f = float.MaxValue;
            }
            return f.ToString("0.00000毫秒");// string.Format("{0.0000}", f);
        }
        #endregion

        private static int _randIndex = 0;
        #region 生成随机数
        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="num1">开始</param>
        /// <param name="num2">结束</param>
        /// <returns>从多少到多少之间的数据，包括开始不包括结束</returns>
        /// user:jyk
        /// time:2013/2/26 10:55
        public static int RndInt(int num1, int num2)
        {
            if (_randIndex >= 1000000) _randIndex = 1;
            Random rnd = new Random(DateTime.Now.Millisecond + _randIndex);
            _randIndex++;
            return rnd.Next(num1, num2);
        }
        #endregion

        #region 生成数内部不重复的随机数
        /// <summary>
        /// 生成数内部不重复的随机数 0-9
        /// </summary>
        /// <param name="length">位数。范围：2-10</param>
        /// <returns></returns>
        /// user:jyk
        /// time:2013/2/26 10:55
        public static string RndNumNotRepeat(int length)
        {
            if (length > 10) length = 10;
            if (_randIndex >= 1000000) _randIndex = 1;
            var lstChar = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            var num = new StringBuilder(length);
            var rnd = new Random(DateTime.Now.Millisecond + _randIndex++);
            for (int i = 0; i < length; i++)
            {
                int rndIndex = rnd.Next(0, 10 - i);
                num.Append(lstChar[rndIndex]);
                lstChar.RemoveAt(rndIndex);
            }
            return num.ToString();
        }
        #endregion

        //输出js函数部分
        #region 关闭当前窗口(静态) CloseWindow
        /// <summary>
        /// 关闭当前窗口(静态)
        /// </summary>
        public static void CloseWindow()
        {
            HttpContext.Current.Response.Write("<script type=\"text/javascript\" language=\"javascript\">window.close()</script>");
        }
        #endregion

        #region 输出提示信息 MsgBox
        /// <summary>
        /// 输出提示信息，然后根据参数是否停止运行。
        /// 在服务器控件里做属性的值的验证的时候用。
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <param name="isEnd">true:结束运行；false:继续运行</param>
        public static void MsgBox(string msg, bool isEnd)
        {
            HttpContext.Current.Response.Write(msg);
            if (isEnd)
                HttpContext.Current.Response.End();

        }
        #endregion

        #region 打开没有Toolbar的新窗口 OpenWindow
        /// <summary>
        /// (Descript) 打开没有Toolbar的新窗口
        ///	  (Author) 谈伟
        ///	    (Date) 2005-1-18
        /// </summary>
        /// <param name="url">页面路径</param>
        /// <param name="height">窗口高度</param>
        /// <param name="width">窗口宽度</param>
        /// <param name="webName">窗口名称</param>
        public static void OpenWindow(string url, int height, int width, string webName)
        {
            //构造JAVASCRIPT
            const string tmpJs = "<script type=\"text/javascript\" language=\"javascript\">window.open('{0}','{1}','height={2}','width={3}','top=0,left=0,location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=yes,toolbar=no,directories=no');</script>";
            HttpContext.Current.Response.Write(string.Format(tmpJs, url, webName, height, width));
        }
        #endregion

        #region 弹出Alert窗口 PageRegisterAlert
        /// <summary>
        /// 页面显示后，弹出Alert窗口
        /// </summary>
        /// <param name="page">调用页面(一般为this.Page)</param>
        /// <param name="msg">弹出消息</param>
        public static void PageRegisterAlert(Page page, string msg)
        {
            //调用Page.RegisterStartupScript方法
            const string tmpJs = "<script type=\"text/javascript\" language=\"javascript\">alert('{0}')</script>";
            page.ClientScript.RegisterStartupScript(page.GetType(), "a" + _index, string.Format(tmpJs, msg));
            _index++;
            if (_index > 1000) _index = 1;
        }
        #endregion

        #region 输出一段js脚本
        /// <summary>
        /// 页面结尾处，输出一段js脚本
        /// </summary>
        /// <param name="page">调用页面(一般为this.Page)</param>
        /// <param name="js">要输出的js脚本</param>
        public static void PageRegisterJavascript(Page page, string js)
        {
            //调用Page.RegisterStartupScript方法
            const string tmpJs = "<script type=\"text/javascript\" language=\"javascript\">{0}</script>";
            page.ClientScript.RegisterStartupScript(page.GetType(), "a" + _index, string.Format(tmpJs, js));
            _index++;
            if (_index > 1000) _index = 1;
        }
        #endregion

        #region 输出一段信息
        /// <summary>
        /// 页面结尾处，输出一段信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg"></param>
        public static void PageRegisterString(Page page, string msg)
        {
            //调用Page.RegisterStartupScript方法
            page.ClientScript.RegisterStartupScript(page.GetType(), "a" + _index, msg);
            _index++;
            if (_index > 1000) _index = 1;
        }
        #endregion

        //验证信息
        #region 验证——数字部分

        #region IsInt
        /// <summary>
        /// 判断是否是Int类型的数。是返回true 否返回false。可以传入null。
        /// </summary>
        /// <param name="value">要判断的字符串</param>
        /// <returns></returns>
        public static bool IsInt(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            //判断是否只有 -
            if (value == "-")
                return false;

            //去掉第一个负号，中间是不可以有负号的
            if (value.Substring(0, 1) == "-")
                value = value.Remove(0, 1);

            foreach (char c in value)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
        #endregion

        #region IsNumeric
        /// <summary>
        /// 判断是否是Numeric类型的数，是返回true 否返回false。可以传入null。
        /// </summary>
        /// <param name="value">要验证的字符串</param>
        /// <returns></returns>
        public static bool IsNumeric(string value)
        {
            //System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex("-?([0]|([1-9]+\\d{0,}?))(.[\\d]+)?$");  
            //return reg1.IsMatch(strVal);  
            //string tmp="";

            //判断是否为null 和空字符串
            if (string.IsNullOrEmpty(value))
                return false;
            //判断是否只有.、-、 -.
            if (value == "." || value == "-" || value == "-.")
                return false;

            //记录是否有多个小数点
            bool hasPoint = false;			//是否有小数点

            //去掉第一个负号，中间是不可以有负号的
            value = value.TrimStart('-');

            foreach (char c in value)
            {
                if (c == '.')
                    if (hasPoint)
                        return false;
                    else
                        hasPoint = true;

                if ((c < '0' || c > '9') && c != '.')
                    return false;
            }
            return true;
        }
        #endregion

        #region StringToInt
        /// <summary>
        /// 转换为整数。不是整数的话，返回null
        /// </summary>
        /// <param name="value">要转换的字符串</param>
        /// <returns></returns>
        public static int? StringToInt(string value)
        {
            //判断是否是数字，是数字返回数字，不是数字返回-1
            if (IsInt(value))
                return Int32.Parse(value);

            return null;
        }

        #endregion

        #region StringToFloat
        /// <summary>
        /// 转换为Float类型的数。不是的话，返回null
        /// </summary>
        /// <param name="value">要转换的字符串</param>
        /// <returns></returns>
        public static float? StringToFloat(string value)
        {
            //判断是否是数字，是数字返回数字，不是数字返回-1
            if (IsNumeric(value))
                return float.Parse(value);

            return null;
        }

        #endregion

        #region IsIDString
        /// <summary>
        /// 判断是否为ID串（1,2,3,4）。是返回true 否返回false。可以传入null。
        /// ID为int类型
        /// </summary>
        /// <example >
        /// 1,2,3,4,5,6,7
        /// </example>
        /// <param name="value">要判断的字符串</param>
        /// <returns></returns>
        public static bool IsIDString(string value)
        {
            char beforeChar = ',';   //上一个字符

            bool flag = false;
            if (value == null)
                return false;
            if (value == "")
                return true;
            //判断是否只有 ,
            if (value == ",")
                return false;
            if (value == ",-")
                return false;

            //判断第一位是否是,号
            if (value.Substring(0, 1) == ",")
                return false;

            //判断最后一位是否是,号
            if (value.Substring(value.Length - 1, 1) == ",")
                return false;

            foreach (char c in value)
            {
                if (c == ',')
                    if (flag) return false; else flag = true;

                else if (c == '-')
                {
                    if (beforeChar != ',')
                        return false;
                }
                else if ((c >= '0' && c <= '9'))
                    flag = false;
                else
                    return false;

                beforeChar = c;
            }
            return true;
        }
        #endregion

        #region IsGUID
        /// <summary>
        /// 验证是否是GUID
        /// 6454bc76-5f98-de11-aa4c-00219bf56456
        /// </summary>
        /// <param name="value">要验证的字符串</param>
        /// <returns></returns>
        public static bool IsGuid(string value)
        {
            if (value == null)
                return false;

            if (value == "")
                return false;

            value = value.TrimStart('{');
            value = value.TrimEnd('}');

            //长度必须是36位
            if (value.Length != 36)
                return false;

            foreach (char c in value)
            {
                if ((c >= '0' && c <= '9'))
                    continue;
                if (c >= 'A' && c <= 'F')
                    continue;
                if (c == '-')
                    continue;
                if (c >= 'a' && c <= 'f')
                    continue;

                return false;
            }
            return true;
        }
        #endregion

        #endregion

        #region 验证——时间部分

        #region IsDateTime
        /// <summary>
        /// 判断是否是正确的时间格式。正确返回 true，不正确返回false。
        /// </summary>
        /// <param name="value">要判断的字符串</param>
        /// <returns></returns>
        public static bool IsDateTime(string value)
        {
            //判断时间是否正确
            try
            {
                Convert.ToDateTime(value);
                return true;
            }
            catch
            {
                //时间格式不正确
                //errorMsg = "您填的时间格式不正确，请按照2004-1-1的形式填写。";
                return false;
            }

        }
        #endregion

        #region StringToDateTime
        /// <summary>
        /// 转换时间。不正确的话，返回null
        /// </summary>
        /// <param name="value">要转换的字符串</param>
        /// <returns></returns>
        public static DateTime? StringToDateTime(string value)
        {
            //判断时间是否正确
            DateTime? tmpDateTime;
            try
            {
                tmpDateTime = Convert.ToDateTime(value);
            }
            catch
            {
                //时间格式不正确
                tmpDateTime = null;
            }

            return tmpDateTime;
        }
        #endregion

        #endregion

        //文件操作
        #region 删除文件
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">文件的物理地址</param>
        /// <returns></returns>
        public static bool DeleteFile(string path)
        {
            try
            {
                System.IO.File.Delete(path);
                return true;
            }
            catch
            {
                //errorMsg = "删除不成功！";
                return false;
            }
        }
        #endregion

        //网页相关
        #region 传入URL返回网页的html代码
        /// <summary>
        /// 传入URL返回网页的html代码
        /// </summary>
        /// <param name="url">要获取HTML的网址</param>
        /// <returns></returns>
        public static string GetHtmlByUrl(string url)
        {
            try
            {
                System.Net.WebRequest wReq = System.Net.WebRequest.Create(url);
                // Get the response instance.
                System.Net.WebResponse wResp = wReq.GetResponse();
                // Read an HTTP-specific property
                //if (wResp.GetType() ==HttpWebResponse)
                //{
                //DateTime updated  =((System.Net.HttpWebResponse)wResp).LastModified;
                //}
                // Get the response stream.
                System.IO.Stream respStream = wResp.GetResponseStream();
                // Dim reader As StreamReader = New StreamReader(respStream)
                if (respStream != null)
                {
                    var reader = new System.IO.StreamReader(respStream, Encoding.GetEncoding("utf-8"));//gb2312
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(url,ex);
            }
            return null;
        }

        #endregion

    }

}
