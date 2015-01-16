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
 * function: 定义字段元数据的接口，便于引用。便于IControlHelp的定义
 * history:  created by 金洋 2009-6-25 8:51:30 
 * **********************************************
*/

namespace Nature.MetaData.Entity
{
    /// <summary>
    /// 给字段的元数据实体类定义一个接口。
    /// 目前的目的是为了方便引用，否则会出现互相引用。
    /// 便于IControlHelp的定义。
    /// </summary>
    public interface IColumn 
    {
        //ControlInfo ControlInfo { get; }
        /// <summary>
        /// 加入到集合里使用的key
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        /// user:jyk
        /// time:2012/9/15 10:24
        int Key { set; get; }
    }
}
