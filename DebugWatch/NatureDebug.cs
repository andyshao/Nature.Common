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
* function: 记录详细步骤和每个步骤的详细用时的实体类
* history:  created by 金洋   2013-09-27
* ***********************************************/


using System;
using System.Collections.Generic;
using System.Diagnostics;
using Nature.Common;

namespace Nature.DebugWatch
{
    /// <summary>
    /// 记录详细步骤和每个步骤的详细用时的实体类
    /// </summary>
    public class NatureDebug
    {
        /// <summary>
        /// 计时器
        /// </summary>
        private Stopwatch _stopwatch;
       
        /// <summary>
        /// 步骤名称，比如验证用户是否登录、获取记录。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 访问人的ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 开始时间 
        /// </summary>
        public DateTime StartTime { get; set; }

        
        /// <summary>
        /// 步骤执行完毕，使用的时间，单位：毫秒
        /// </summary>
        public string UseTime { get; set; }

        /// <summary>
        /// 请求的Url地址，包括参数
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 如果出错的话，给客户的友好提示信息，并且程序不能继续运行。
        /// 比如添加数据的时候，某数据的格式不正确，告诉客户相关信息。
        /// string.Empty  表示没有问题。
        /// </summary>
        public string ErrorMessage { get; set; }
        
        /// <summary>
        /// 步骤内部的多个子步骤
        /// </summary>
        public IList<NatureDebugInfo> DetailList { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public NatureDebug()
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            ErrorMessage = "";
        }

        /// <summary>
        /// 停止计时，并且计算执行时间
        /// </summary>
        public void Stop()
        {
            _stopwatch.Stop();
            UseTime = Functions.TimeSpantoFloat(_stopwatch.Elapsed);
            
        }
    }

    /// <summary>
    /// 记录详细步骤和每个步骤的详细用时的实体类
    /// </summary>
    public class NatureDebugInfo
    {
        /// <summary>
        /// 计时器
        /// </summary>
        private Stopwatch _stopwatch;
       
        /// <summary>
        /// 步骤名称，比如验证用户是否登录、获取记录。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 步骤执行完毕，使用的时间，单位：毫秒
        /// </summary>
        public string UseTime { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }

        private IList<NatureDebugInfo> _detail;
        /// <summary>
        /// 步骤内部的多个子步骤
        /// </summary>
        public IList<NatureDebugInfo> DetailList
        {
            get { return _detail ?? (_detail = new List<NatureDebugInfo>()); }
            set { _detail = value; }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public NatureDebugInfo()
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        /// <summary>
        /// 停止计时，并且计算执行时间
        /// </summary>
        public void Stop()
        {
            _stopwatch.Stop();
            UseTime = Functions.TimeSpantoFloat(_stopwatch.Elapsed);

        }
    }
}
