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
* function: 为了能够让分页控件在自动运行模式下，可以方便的替换数据访问函数库，特此设置了这个接口。
*           以后可能扩充为整个数据访问函数库的接口，这个就要详细考虑了。
* history:  created by 金洋  
* ***********************************************/

using System.Collections.Generic;
using System.Data;
using Nature.Data.Model;

namespace Nature.Data
{
    /// <summary>
    /// 怪怪提供的一种方式，目前我还说不清楚。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public delegate T Func<T>();

    /// <summary>
    /// 为分页控件设计的接口，以达到可以更换数据访问函数库的目的
    /// </summary>
    public interface IDal
    {
        #region 获取记录集 DataSet和DataTable
        /// <summary>
        /// 传入SQL语句，返回DataTable的接口
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        DataTable ExecuteFillDataTable(string sql);

        /// <summary>
        /// 传入SQL语句，返回DataSet的接口
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        DataSet ExecuteFillDataSet(string sql);
        #endregion

        #region 获取实体类 IList<WebList2>
        /// <summary>
        /// 传入SQl语句，返回实体类WebList2的IList集合
        /// </summary>
        /// <param name="sql">查询语句。
        /// 比如：select as ID ,as title,as URL ,as AddedDate, as Intro ,as hits ,as img , as spare from tableName 
        /// </param>
        /// <param name="lstFormat">标题的最大字符数、内容简介的最大字符数，一个汉字按照两个字符计算。传入“0”则表示不截取标题。发表时间的格式化。</param>
        /// <returns>返回WebList2结构的集合。ID，URL，标题，时间，人气，图片名，简介，备用</returns>
        IList<WebList2> ExecuteFillWebList2(string sql, WebList2Format lstFormat);
        #endregion

        #region 获取第一条记录，第一个字段的值
        /// <summary>
        /// 传入SQL语句，返回第一条记录，第一个字段的值的接口。
        /// 可以用于统计总记录数
        /// </summary>
        /// <param name="sql">查询语句。
        /// 比如：select title   from tableName where ID =1
        /// </param>
        /// <returns></returns>
        string ExecuteString(string sql);
        #endregion

        /// <summary>
        /// 运行SQl语句返回每一条记录的第一个字段的值。返回字符串数组
        /// </summary>
        /// <param name="sql">查询语句。比如select myName from tableName</param>
        /// <returns></returns>
        string[] ExecuteStringsByColumns(string sql);
       

        /// <summary>
        /// 记录出错的描述信息
        /// </summary>
        string ErrorMessage { get; }
       
    }
    
}
