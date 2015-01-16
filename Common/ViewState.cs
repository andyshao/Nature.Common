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
* function: 缓存信息的统一管理，可以不缓存、缓存在隐藏域、Session、Cache、Application
* history:  created by 金洋  
* ***********************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web;

namespace Nature.Common
{
    #region 枚举enum SaveViewStateLocation
    /// <summary>
    /// 保存数据的位置
    /// </summary>
    public enum SaveViewStateLocation
    {
        /// <summary>
        /// 不保存
        /// </summary>
        NoSave = 1,

        /// <summary>
        /// 放在Cookie里面保存
        /// </summary>
        Cookie = 2,

        /// <summary>
        /// 放在隐藏域里面保存
        /// </summary>
        Hidden = 3,

        /// <summary>
        /// 放在Session里面保存
        /// </summary>
        Session = 4,

        /// <summary>
        /// 放在Cache里面保存
        /// </summary>
        Cache = 5,

        /// <summary>
        /// 放在Application里面保存
        /// </summary>
        Application = 6

    }
    #endregion

    /// <summary>
    /// 缓存属性值
    /// </summary>
    public class MyViewState  // : IStateManager 没有成功
    {
       
        #region 成员
        /// <summary>
        /// 保存数据的字典
        /// </summary>
        private Dictionary<string, string> vs = new Dictionary<string, string>();
        #endregion

        /// <summary>
        /// 构造函数，设置默认存放位置：不保存。
        /// </summary>
        public MyViewState()
        {
            //默认设置为不保存
            SaveLocation = SaveViewStateLocation.NoSave;
        }

      
        #region 属性

        #region 存放数据的位置

        /// <summary>
        /// 存放数据的位置。
        /// </summary>
        public SaveViewStateLocation SaveLocation { get; set; }

        #endregion

        #region 密钥
        /// <summary>
        /// 密钥
        /// </summary>
        private string _key = "";
        /// <summary>
        /// 密钥，不同的密钥会生成不同的密文。空字符串表示不需要加密。
        /// </summary>
        public string Key
        {
            set { _key = value; }
            get { return _key; }
        }
        #endregion

        #region 保存数据的标识
        private string _clientID = "myVS";
        /// <summary>
        /// 保存数据的标识
        /// </summary>
        public string ClientID
        {
            set { _clientID = value; }
            get { return _clientID; }
        }
        #endregion

        #region 索引器，类似于ViewState的使用方式
        /// <summary>
        /// 索引器，类似于ViewState的使用方式
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[string key]
        {
            set
            {
                if (vs.ContainsKey(key))
                {
                    vs[key] = value;
                }
                else
                {
                    vs.Add(key, value);
                }
            }
            get
            {
                if (vs.ContainsKey(key))
                    return vs[key];
                return null;
            }
        }
        #endregion
        #endregion

        #region 函数
        
        #region 保存数据
        /// <summary>
        /// 把数据保存到指定的位置里面。
        /// </summary>
        private void SaveViewState()     //virtual object
        {
            //拼接字符串
            var str = new StringBuilder(1000);
            foreach (KeyValuePair<string,string> entry in vs)
            {
                str.Append(entry.Key);
                str.Append("`");
                str.Append(entry.Value);
                str.Append("`");
            }

            if (str.Length == 0)        //没有赋值
                return ;

            str.Remove(str.Length - 1, 1);

            string myData =  str.ToString();

            if (Key.Length > 0)
            {
                //加密
                myData = DesBase64.Encrypt(myData, Key);
            }

            str.Length = 0;

            #region 保存
            switch (SaveLocation)
            {
                case SaveViewStateLocation.Cookie :
                    //HttpContext.Current.Response.Cookies[ClientID].Value = myData;
                    var httpCookie = HttpContext.Current.Response.Cookies[ClientID];
                    if (httpCookie != null)
                        httpCookie.Value = myData;
                    break;

                case SaveViewStateLocation.Hidden:
                    #region
                    if (Page != null)
                    {
                        Page.ClientScript.RegisterHiddenField(ClientID, myData);
                    }
                    #endregion
                    break;

                case SaveViewStateLocation.Session :
                    HttpContext.Current.Session[ClientID] = myData;
                    break;

                case SaveViewStateLocation.Cache :
                    HttpContext.Current.Cache[ClientID] = myData;
                    break;
                
                case SaveViewStateLocation.Application :
                    HttpContext.Current.Application[ClientID] = myData;
                    break;

            }
            #endregion

        }
        #endregion

        #region 加载数据
        /// <summary>
        /// 从保存的位置加载数据。
        /// </summary>
        private void LoadViewState()
        {
            //加载
            string str = "";

            #region 提取数据
            switch (SaveLocation)
            {
                case SaveViewStateLocation.Cookie:
                    if (HttpContext.Current.Request.Cookies[ClientID] == null)
                        return;
                    str = HttpContext.Current.Request.Cookies[ClientID].Value;
                    break;

                case SaveViewStateLocation.Hidden :
                    if (Page == null) return;
                    if (HttpContext.Current.Request[ClientID] == null) return;

                    str = HttpContext.Current.Request[ClientID];
                    break;

                case SaveViewStateLocation.Session:
                    str = HttpContext.Current.Session[ClientID].ToString();
                    break;

                case SaveViewStateLocation.Cache:
                    str = HttpContext.Current.Cache[ClientID].ToString();
                    break;

                case SaveViewStateLocation.Application:
                    str = HttpContext.Current.Application[ClientID].ToString();
                    break;
            }
            #endregion

            if (str.Length == 0)        //没有取到值
                return;

            if (Key.Length > 0)
            {
                //解密
                str = DesBase64.Decrypt(str, Key);
            }

            //拆分
            string[] arr = str.Split('`');
                  
            //赋值
            for (int i = 0; i < arr.Length; i += 2)
            {
                if (!vs.ContainsKey(arr[i]))
                    vs.Add(arr[i], arr[i + 1]);
            }
        }
        #endregion
        
        #region 用于给表单里面添加隐藏域和加事件
        /// <summary>
        /// 用于给表单里面添加隐藏域和加事件
        /// </summary>
        private Page _page;
        /// <summary>
        /// 传递Page实例，以实现自动保存数据，和添加隐藏域的功能
        /// </summary>
        public Page Page
        {
            set
            {
                _page = value;
                _page.InitComplete += MyPageInitComplete;
                _page.SaveStateComplete += MyPageSaveStateComplete;//自动保存内容
                ////_page.LoadComplete += new EventHandler(_page_LoadComplete);
                ////_page.PreLoad += new EventHandler(MyPage_PreLoad);          //本来想在Page_Load之前加载内容，但是出现了一点问题
            }
            get { return _page; }
        }
        #endregion

        #region 用于自动加载和保存数据的事件
        void MyPageInitComplete(object sender, EventArgs e)
        {
            LoadViewState();
            //throw new NotImplementedException();
        }

        void MyPageSaveStateComplete(object sender, EventArgs e)
        {
            SaveViewState();
            //throw new NotImplementedException();
        }
        #endregion

        #endregion

        #region 实现接口 未成功
        //public IEnumerable<T> InnerContainer
        //{
        //    get {return this.vs ;}
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="state"></param>
        //public  virtual void LoadViewState(object state)
        //{
        //    LoadViewState();
        //}
         
        ///// <summary>
        ///// 
        ///// </summary>
        //public void TrackViewState()
        //{
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //public bool IsTrackingViewState
        //{
        //    get
        //    {
        //        return this._isTrackingViewState;
        //    }
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        //private bool _isTrackingViewState;

        #endregion

    }
}
