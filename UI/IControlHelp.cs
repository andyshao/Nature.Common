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
 * function: 控件的取值、赋值、自我描绘的接口
 * history:  created by 金洋 2009-6-25 8:51:30 
 * **********************************************
*/

using Nature.MetaData.Entity;

namespace Nature.UI
{
    #region 控件的取值、赋值、自我描绘的接口
    /// <summary>
    /// 控件的取值、赋值、自我描绘的接口
    /// </summary>
    public interface IControlHelp
    {
        // 属性
        /// <summary>
        /// 统一控件的取值和赋值的属性
        /// </summary>
        /// <returns>控件的某个值</returns>
        string ControlValue { get; set; }

        //函数
        /// <summary>
        /// 根据kind获取控件的某个属性的值
        /// </summary>
        /// <param name="kind">取值方式</param>
        /// <returns></returns>
        string GetControlValue(string kind);

        /// <summary>
        /// 根据kind设置控件的默认值
        /// </summary>
        /// <param name="kind">赋值方式</param>
        /// <param name="value">要设置的值</param>
        void SetControlValue(string kind, string value);

        /// <summary>
        /// 设置控件的状态
        /// 1：正常
        /// 2：只读
        /// 3：不可用
        /// </summary>
        /// <param name="kind">1：正常；2：只读；3：不可用</param>
        void SetControlState(string kind);

        /// <summary>
        /// 通过控件的描述信息，进行自我描述。比如设置maxlength 等。
        /// </summary>
        /// <param name="formColumnMeta">字段信息</param>
        /// <param name="dal">数据访问函数库</param>
        /// <param name="isForm">True：表单控件；False：查询控件</param>
        void ShowMe(IColumn formColumnMeta, Data.IDal dal, bool isForm);

    }
    #endregion

    

}
