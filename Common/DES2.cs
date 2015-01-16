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
 * function: 加密算法的封装。DES + base64
 * history:  created by 金洋  
 * ***********************************************/

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Nature.Common
{
    #region 加密、解密字符串
    /// <summary>
    /// 加密字符串。DES 。
    /// </summary>
    public class DesUrl
    {
        #region 加密，可以设置密钥
        /// <summary>
        /// 设置密钥，加密。
        /// </summary>
        /// <param name="sourceData">原文</param>
        /// <param name="key">密钥，8位，字符串方式</param>
        /// <returns></returns>
        public static string Encrypt(string sourceData, string key)
        {
            //检查密钥是否符合规定
            if (key.Length > 8)
                key = key.Substring(0, 8);

            //访问数据加密标准(DES)算法的加密服务提供程序 (CSP) 版本的包装对象
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = ASCIIEncoding.ASCII.GetBytes(key);　//建立加密对象的密钥和偏移量
            des.IV = ASCIIEncoding.ASCII.GetBytes(key);　 //原文使用ASCIIEncoding.ASCII方法的GetBytes方法

            byte[] inputByteArray = Encoding.Default.GetBytes(sourceData);//把字符串放到byte数组中

            MemoryStream ms = new MemoryStream();//创建其支持存储区为内存的流　
            //定义将数据流链接到加密转换的流
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            //上面已经完成了把加密后的结果放到内存中去

            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();

        }

        #endregion

        #region 解密，可以设置密钥
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="ciphertext">密文</param>
        /// <param name="key">密钥，8位，字符串方式</param>
        /// <returns></returns>
        public static string Decrypt(string ciphertext, string key)
        {
            string oldKey = key;
            //检查密钥是否符合规定
            if (key.Length > 8)
                key = key.Substring(0, 8);

            MemoryStream ms = new MemoryStream();
               
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                byte[] inputByteArray = new byte[ciphertext.Length/2];
                for (int x = 0; x < ciphertext.Length/2; x++)
                {
                    int i = (Convert.ToInt32(ciphertext.Substring(x*2, 2), 16));
                    inputByteArray[x] = (byte) i;
                }

                des.Key = ASCIIEncoding.ASCII.GetBytes(key); //建立加密对象的密钥和偏移量，此值重要，不能修改
                des.IV = ASCIIEncoding.ASCII.GetBytes(key);
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);

                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                //建立StringBuild对象，createDecrypt使用的是流对象，必须把解密后的文本变成流对象
                StringBuilder ret = new StringBuilder();
            }
            catch (Exception e)
            {
                throw new Exception("miwen:" + ciphertext + ";\n<br>key:" + oldKey + "\n<br>" + e.Message);
            }

            return System.Text.Encoding.Default.GetString(ms.ToArray());

        }

        #endregion

    }
    #endregion

}
