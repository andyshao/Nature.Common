/**
 * 自然框架之共用类库
 * http://www.natureFW.com/
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
 * function: 定义一个Attribute，记录元数据里字段的ID，用于属性和字段的对应关系。
 * history:  created by 金洋 2009-6-25 8:51:30 
 * **********************************************
*/


using System;

namespace Nature.Attributes
{
    /// <summary>
    /// 定义一个Attribute，记录元数据里字段的ID，用于属性和字段的对应关系。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ColumnIDAttribute : Attribute
    {
        /// <summary>
        /// 字段ID，即字段编号
        /// </summary>
        public int ColumnID { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value">字段编号。int类型</param>
        public ColumnIDAttribute(int value)
        {
            ColumnID = value;
        }
    }
}
