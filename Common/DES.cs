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
    /// 加密字符串。DES+base64 。
    /// </summary>
    public class DesBase64
    {

        #region 加密，可以设置密钥
        /// <summary>
        /// 设置密钥，加密。
        /// </summary>
        /// <param name="sourceData">原文</param>
        /// <param name="key">密钥，8位数字，字符串方式</param>
        /// <returns></returns>
        public static string Encrypt(string sourceData, string key)
        {
            //set key and initialization vector values
            //Byte[] key = new byte[] {0x21, 2, 0x88, 4, 5, 0x56, 7, 0x99};
            //Byte[] iv = new byte[] {0x21, 2, 0x88, 4, 5, 0x56, 7, 0x99};

            #region 检查密钥是否符合规定

            if (key.Length > 8)
            {
                key = key.Substring(0, 8);
            }

            #endregion

            char[] tmp = key.ToCharArray();
            var keys = new byte[8];
            //Byte[] keys = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var iv = new byte[] {1, 2, 3, 4, 5, 6, 7, 8};

            //设置密钥
            for (int i = 0; i < 8; i++)
            {
                if (tmp.Length > i)
                {
                    keys[i] = (byte) tmp[i];
                }
                else
                {
                    keys[i] = (byte) i;
                }
            }

            //convert data to byte array
            Byte[] sourceDataBytes = Encoding.UTF8.GetBytes(sourceData);
            //get target memory stream
            var tempStream = new MemoryStream();
            //get encryptor and encryption stream
            var encryptor = new DESCryptoServiceProvider();
            var encryptionStream = new CryptoStream(tempStream, encryptor.CreateEncryptor(keys, iv),
                                                    CryptoStreamMode.Write);

            //encrypt data
            encryptionStream.Write(sourceDataBytes, 0, sourceDataBytes.Length);
            encryptionStream.FlushFinalBlock();

            //put data into byte array
            Byte[] encryptedDataBytes = tempStream.GetBuffer();
            //convert encrypted data into string
            return Convert.ToBase64String(encryptedDataBytes, 0, (int) tempStream.Length);

        }

        #endregion

        #region 解密，可以设置密钥
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="ciphertext">密文</param>
        /// <param name="key">密钥，8位数字，字符串方式</param>
        /// <returns></returns>
        public static string Decrypt(string ciphertext, string key)
        {
            //检查密钥是否符合规定
            if (key.Length > 8)
                key = key.Substring(0, 8);

            char[] tmp = key.ToCharArray();
            var keys = new byte[8];
            //Byte[] keys = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var iv = new byte[] {1, 2, 3, 4, 5, 6, 7, 8};

            //设置密钥
            for (int i = 0; i < 8; i++)
            {
                if (tmp.Length > i)
                    keys[i] = (byte) tmp[i];
                else
                    keys[i] = (byte) i;
            }

            //convert data to byte array
            Byte[] encryptedDataBytes = Convert.FromBase64String(ciphertext);
            //get source memory stream and fill it 
            var tempStream = new MemoryStream(encryptedDataBytes, 0, encryptedDataBytes.Length);
            //get decryptor and decryption stream 
            var decryptor = new DESCryptoServiceProvider();
            var decryptionStream = new CryptoStream(tempStream, decryptor.CreateDecryptor(keys, iv),
                                                    CryptoStreamMode.Read);

            //decrypt data 
            var allDataReader = new StreamReader(decryptionStream);

            return allDataReader.ReadToEnd();

        }

        #endregion

    }
    #endregion

}
