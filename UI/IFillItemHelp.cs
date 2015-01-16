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
 * function: 填充Item的接口
 * history:  created by 金洋 2009-6-25 8:51:30 
 * **********************************************
*/


using Nature.Data;

namespace Nature.UI
{
    /// <summary>
    /// 填充Item的接口
    /// </summary>
    public interface IFillItemHelp
    {
        /// <summary>
        /// 字符串填充
        /// </summary>
        /// <param name="valuesAndTexts">选项。~分割。</param>
        void ItemAddByString(string valuesAndTexts);

        /// <summary>
        /// 根据SQL语句提取数据，绑定Item
        /// </summary>
        /// <param name="sql">提取数据的SQL语句</param>
        /// <param name="dal">数据访问函数库的实例</param>
        string BindListBySql(string sql, IDal dal);

    }
}
