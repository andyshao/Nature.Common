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

namespace Nature.Common
{
    /// <summary>
    /// 把数据变成json的value的形式。也可以变成key的形式
    /// </summary>
    public class Json
    {
        //json部分
        #region 字符串 类型的数据转换为 json格式
        /// <summary>
        /// 字符串类型的数据转换为 json格式
        /// </summary>
        /// <param name="value">要转换的字符串</param>
        /// <param name="sb">StringBuilder的实例，即json的容器</param>
        /// <returns></returns>
        public static void StringToJson(String value, StringBuilder sb)
        {
            sb.Append('\"');
            foreach (char c in value)
            {
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\"");
                        break;
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '/':
                        sb.Append("\\/");
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            sb.Append('\"');
        }

        #endregion

        #region int 类型的数据转换为 json格式
        /// <summary>
        /// int 类型的数据转换为 json格式
        /// </summary>
        /// <param name="value">要转换的int</param>
        /// <param name="sb">StringBuilder的实例，即json的容器</param>
        /// <returns></returns>
        public static void IntToJson(int value, StringBuilder sb)
        {
            sb.Append(value);
        }
        #endregion

        #region Double 类型的数据转换为 json格式
        /// <summary>
        /// Double 类型的数据转换为 json格式
        /// </summary>
        /// <param name="value">要转换的double</param>
        /// <param name="sb">StringBuilder的实例，即json的容器</param>
        /// <returns></returns>
        public static void DoubleToJson(double value, StringBuilder sb)
        {
            sb.Append(value);
        }
        #endregion

        #region Double 类型的数据转换为 json格式
        /// <summary>
        /// Double 类型的数据转换为 json格式
        /// </summary>
        /// <param name="value">要转换的double</param>
        /// <param name="sb">StringBuilder的实例，即json的容器</param>
        /// <returns></returns>
        public static void DecimalToJson(decimal value, StringBuilder sb)
        {
            sb.Append(value);
        }
        #endregion

        #region Bool 类型的数据转换为 json格式
        /// <summary>
        /// Bool 类型的数据转换为 json格式
        /// </summary>
        /// <param name="value">要转换的bool</param>
        /// <param name="sb">StringBuilder的实例，即json的容器</param>
        /// <returns></returns>
        public static void BoolToJson(bool value, StringBuilder sb)
        {
            sb.Append(value ? "true" : "false");
        }

        #endregion

        #region DateTime 类型的数据转换为 json格式
        /// <summary>
        /// DateTime 类型的数据转换为 json格式
        /// </summary>
        /// <param name="value">要转换的日期</param>
        /// <param name="sb">StringBuilder的实例，即json的容器</param>
        public static void DateTimeToJson(DateTime value, StringBuilder sb)
        {
            sb.Append('\"');
            sb.Append(value.ToString("yyyy-MM-dd HH:mm"));
            sb.Append('\"');

        }
        #endregion

        #region Array 类型的数据转换为 json格式
        /// <summary>
        /// 数组 类型的数据转换为 json格式
        /// </summary>
        /// <param name="array">要转换的数组</param>
        /// <param name="sb">StringBuilder的实例，即json的容器</param>
        public static void ArrayToJson(Object[] array, StringBuilder sb)
        {
            if (array.Length == 0)
            {
                sb.Append("[]");
                return;
            }

            sb.Append('[');
            foreach (Object o in array)
            {
                ObjectToJson(o, sb);
                sb.Append(',');
            }
            // 将最后添加的 ',' 变为 ']': 
            sb[sb.Length - 1] = ']';

        }
        #endregion

        #region Array 类型的数据转换为 json格式
        /// <summary>
        /// byte 类型的数据转换为 json格式
        /// </summary>
        /// <param name="b">要转换的byte</param>
        /// <param name="sb">StringBuilder的实例，即json的容器</param>
        public static void ByteToJson(byte b, StringBuilder sb)
        {
            sb.Append(b);

        }
        #endregion

        #region Object 类型的数据转换为 json格式
        /// <summary>
        /// Object 类型的数据转换为 json格式
        /// </summary>
        /// <param name="o">要转换的Object</param>
        /// <param name="sb">StringBuilder的实例，即json的容器</param>
        public static void ObjectToJson(Object o, StringBuilder sb)
        {
            if (o == null)
                sb.Append("null");

            else if (o is String)
                StringToJson((String)o, sb);

            else if (o is int)
                IntToJson((int)o, sb);

            else if (o is double)
                DoubleToJson((double)o, sb);

            else if (o is decimal)
                DecimalToJson((decimal)o, sb);

            else if (o is Object[])
                ArrayToJson((Object[])o, sb);

            else if (o is Boolean)
                BoolToJson((Boolean)o, sb);

            else if (o is DateTime)
                DateTimeToJson((DateTime)o, sb);

            else if (o is byte)
                ByteToJson((byte)o, sb);

            else if (o is short)
                IntToJson((short)o, sb);

            else if (o is Guid )
                StringToJson(Convert.ToString(o), sb);
        }

        #endregion 

        #region 把json格式的控件描述信息转换成字典 Dictionary
        /// <summary>
        /// 把控件的描述信息转换成字典 Dictionary  
        /// string：属性名称
        /// string：值
        /// </summary>
        /// <param name="json">json格式的控件扩展信息</param>
        /// <returns></returns>
        /// user:jyk
        /// time:2012/8/29 20:28
        public static Dictionary<string, string> JsonToDictionary1(string json)
        {
            var dic = new Dictionary<string, string>();

            string[] jsonKeyValues = json.Split(',');

            foreach (string jsonKeyValue in jsonKeyValues)
            {
                string[] keys = jsonKeyValue.Split(':');
                if (keys.Length == 2)
                {
                    dic.Add(keys[0].Trim('\"'), keys[1].Trim('\"'));
                }
            }

            return dic;

        }
        #endregion

        #region 把json格式的控件描述信息转换成字典 Dictionary
        /// <summary>
        /// 把控件的描述信息转换成字典 Dictionary  
        /// string：属性名称
        /// string：值
        /// </summary>
        /// <param name="json">json格式的控件扩展信息</param>
        /// <returns></returns>
        /// user:jyk
        /// time:2012/8/29 20:28
        public static Dictionary<string, string> JsonToDictionary(string json)
        {
            if (string.IsNullOrEmpty( json))
                return null;

            json = json.TrimStart('{');
            json = json.TrimEnd('}');
            var dic = new Dictionary<string, string>();

            bool isKey = true;              //当前遍历的是key
            bool isDoubleQuotes = false;    //在双引号中

            bool isDaKuoHao = false;        //在大括号中

            string tmpKey = "";                  //临时的key
            string tmpValue = "";                //临时的Value

            foreach (char a in json)
            {
                switch (a)
                {
                    case ',':
                        #region

                        if (isDoubleQuotes)
                        {
                            //双引号中
                            if (isKey)
                                tmpKey += a;
                            else
                                tmpValue += a;
                        }
                        else if (isDaKuoHao )
                        {
                            //大括号中，暂时不在分析
                            if (isKey)
                                tmpKey += a;
                            else
                                tmpValue += a;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(tmpKey))
                            {
                                //不在双引号中，一对key、value遍历完毕，加入集合
                                if (dic.ContainsKey(tmpKey))
                                {
                                    throw new Exception("已经有这个key了！" + tmpKey);
                                }

                                dic.Add(tmpKey, tmpValue);
                            }

                            tmpKey = "";
                            tmpValue = "";

                            //下一个字符是key
                            isKey = true;
                        }

                        #endregion
                        break;

                    case ':':
                        #region

                        if (isDoubleQuotes)
                        {
                            //双引号中
                            if (isKey)
                            {
                                tmpKey += a;
                            }
                            else
                                tmpValue += a;
                        }
                        else if (isDaKuoHao)
                        {
                            //大括号中，暂时不在分析
                            if (isKey)
                                tmpKey += a;
                            else
                                tmpValue += a;
                        }
                        else
                        {
                            //不在双引号中，后面的字符是value的开始
                            isKey = false;

                        }

                        #endregion
                        break;

                    case '"':
                        #region

                        if (isDaKuoHao)
                        {
                            //在大括号中，目前暂不分析
                            tmpValue += a;
                        }
                        else
                        {
                            //转换双引号状态
                            isDoubleQuotes = !isDoubleQuotes;
                        }

                        #endregion
                        break;

                    case '{':
                        #region 大括号，暂时不分析，直接按照value来处理
                        isKey = false;
                   
                        isDaKuoHao = true;
                        tmpValue += a;

                        #endregion

                        break;

                    case '}':
                        #region 大括号，暂时不分析，直接按照value来处理

                        isDaKuoHao = false;
                        tmpValue += a;
                        isKey = true;
                        #endregion

                        break;

                    default:
                        #region
                        if (isKey)
                            tmpKey += a;
                        else
                            tmpValue += a;
                        #endregion
                        break;

                }

            }

            //遍历完毕
            dic.Add(tmpKey.Trim(), tmpValue);

            return dic;

        }
        #endregion

    }
}
