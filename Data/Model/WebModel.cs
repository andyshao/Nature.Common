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
* function: 定义页面里使用的列表用的实体类
* history:  created by 金洋  
* ***********************************************/


using System.Collections.Generic;

namespace Nature.Data.Model
{
    /// <summary>
    /// 简单的列表，ID、名称、连接
    /// </summary>
    public class WebList1
    {
        /// <summary>
        /// 记录的主键ID。
        /// 对应字段名（别名）：ID
        /// </summary>
        public string ID { set; get; }

        /// <summary>
        /// 链接地址，用于静态页或者URL重写，也可以是动态页面
        /// 对应字段名（别名）：URL
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// 标题、名称等
        /// 对应字段名（别名）：Title
        /// </summary>
        public string Title { set; get; }		
    }


    /// <summary>
    /// 一般的列表，ID、名称、连接之外，还有图片名、简介、发表时间等字段，还可以“嵌套”
    /// </summary>
    public class WebList2
    {
        /// <summary>
        /// 记录的主键ID
        /// 对应字段名（别名）：ID
        /// </summary>
        public string ID { set; get; }
			
        /// <summary>
        /// 链接地址，用于静态页或者URL重写
        /// 对应字段名（别名）：URL
        /// </summary>
        public string URL { set; get; }	
		
        /// <summary>
        /// 未设置截取长度的时候，完整的标题，设置截取长度后，显示截取后的标题
        /// 对应字段名（别名）：Title
        /// </summary>
        public string Title { set; get; }
		
        /// <summary>
        /// 永远显示完整标题
        /// 对应字段名（别名）：Title
        /// </summary>
        public string FullTitle { set; get; }
		
        /// <summary>
        /// 日期时间字段
        /// 对应字段名（别名）：AddedDate
        /// </summary>
        public string AddedDate { set; get; }	

        /// <summary>
        /// 简介内容
        /// 对应字段名（别名）：Intro
        /// </summary>
        public string Introduction { set; get; }	

        /// <summary>
        /// 人气，int的字段
        /// 对应字段名（别名）：hits
        /// </summary>
        public int Hits { set; get; }	    

        /// <summary>
        /// 图片名称
        /// 对应字段名（别名）：img
        /// </summary>
        public string Img { set; get; }

        /// <summary>
        /// 分类，分类ID
        /// 对应字段名（别名）：kind
        /// </summary>
        public string Kind { set; get; }
        
        /// <summary>
        /// 备用
        /// 对应字段名（别名）：spare
        /// </summary>
        public string Spare { set; get; }

        /// <summary>
        /// 其他信息
        /// </summary>
        public IList<WebList2> Other { set; get; }

    }

    /// <summary>
    /// WebList2的格式化，标题、简介的最大字符数，发表时间的格式化。
    /// </summary>
    public class WebList2Format
    {
        /// <summary>
        /// 标题(Title)的最大字符数，一个汉字按两个字节计算。
        /// </summary>
        public int TitleMaxCount { set; get; }
        /// <summary>
        /// 发表时间（AddedDate）的格式化。空表示不格式化。
        /// </summary>
        public string DateFormat { set; get; }
        /// <summary>
        /// 简介(Introduction)的最大字符数，一个汉字按两个字节计算。
        /// </summary>
        public int IntroMaxCount { set; get; }

        /// <summary>
        /// WebList2里面Other对应的格式化信息
        /// </summary>
        public IList<WebList2Format> Other { set; get; }

        /// <summary>
        /// 初始化，设置默认值
        /// </summary>
        public WebList2Format()
        {
            TitleMaxCount = 0;
            DateFormat = "";
            IntroMaxCount = 0;
        }

    }
}
