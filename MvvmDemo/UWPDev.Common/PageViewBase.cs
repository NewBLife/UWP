// ***********************************************************************
// FileName:PageViewBase
// Description:页面共通
// Project:
// Author:NewBLife 云中客
// Created:2016/5/4 22:02:13
// Copyright (c) 2016 NewBLife,All rights reserved.
// ***********************************************************************
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UWPDev.Common
{
    /// <summary>
    /// 页面共通
    /// </summary>
	public class PageViewBase : Page
    {
        /// <summary>
        /// 页面ViewModel基类对象
        /// </summary>
        private PageViewModelBase PageViewModel
        {
            get { return this.DataContext as PageViewModelBase; }
        }

        public PageViewBase()
        {
            this.LoadState += ViewBase_LoadState;
            this.SaveState += ViewBase_SaveState;
        }
        /// <summary>
        /// 保留与当前页关联的状态，以防
        /// 应用程序挂起或从导航缓存中丢弃
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ViewBase_SaveState(object sender, SaveStateEventArgs e)
        {
            if (PageViewModel != null)
            {
                PageViewModel.SaveState(e.PageState);
            }
        }
        /// <summary>
        /// 向该页填入在导航过程中传递的内容以及任何
        /// 在从以前的会话重新创建页时提供的已保存状态。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">提供要显示的组</param>
        void ViewBase_LoadState(object sender, LoadStateEventArgs e)
        {
            if (PageViewModel != null)
            {
                PageViewModel.LoadState(e.NavigationParameter, e.PageState);
            }
        }
        /// <summary>
        /// 注销返回按钮事件与通信消息等操作
        /// </summary>
        void ViewBase_SavingState()
        {
            if (PageViewModel != null)
            {
                PageViewModel.SavingState();
            }
        }
        #region 进程生命期管理

        private String _pageKey;

        /// <summary>
        /// 在当前页上注册此事件以向该页填入
        /// 在导航过程中传递的内容以及任何
        /// 在从以前的会话重新创建页时提供的已保存状态。
        /// </summary>
        public event LoadStateEventHandler LoadState;
        /// <summary>
        /// 在当前页上注册此事件以保留
        /// 与当前页关联的状态，以防
        /// 应用程序挂起或从导航缓存中丢弃
        /// 该页。
        /// </summary>
        public event SaveStateEventHandler SaveState;

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// 此方法调用 <see cref="LoadState"/>，应在此处放置所有
        /// 导航和进程生命周期管理逻辑。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。Parameter
        /// 属性提供要显示的组。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var frameState = SuspensionManager.SessionStateForFrame(this.Frame);
            this._pageKey = "Page-" + this.Frame.BackStackDepth;

            if (e.NavigationMode == NavigationMode.New)
            {
                // 在向导航堆栈添加新页时清除向前导航的
                // 现有状态
                var nextPageKey = this._pageKey;
                int nextPageIndex = this.Frame.BackStackDepth;
                while (frameState.Remove(nextPageKey))
                {
                    nextPageIndex++;
                    nextPageKey = "Page-" + nextPageIndex;
                }

                // 将导航参数传递给新页
                if (this.LoadState != null)
                {
                    this.LoadState(this, new LoadStateEventArgs(e.Parameter, null));
                }
            }
            else
            {
                // 通过将相同策略用于加载挂起状态并从缓存重新创建
                // 放弃的页，将导航参数和保留页状态传递
                // 给页
                if (this.LoadState != null)
                {
                    this.LoadState(this, new LoadStateEventArgs(e.Parameter, (Dictionary<String, Object>)frameState[this._pageKey]));
                }
            }
        }

        /// <summary>
        /// 当此页不再在 Frame 中显示时调用。
        /// 此方法调用 <see cref="SaveState"/>，应在此处放置所有
        /// 导航和进程生命周期管理逻辑。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。Parameter
        /// 属性提供要显示的组。</param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            var frameState = SuspensionManager.SessionStateForFrame(this.Frame);
            var pageState = new Dictionary<String, Object>();
            if (this.SaveState != null)
            {
                this.SaveState(this, new SaveStateEventArgs(pageState));
            }
            frameState[_pageKey] = pageState;
        }

        /// <summary>
        /// 当此页即将不再是Frame中的活动页面时调用。
        /// 因为OnNavigatedFrom事件在应用挂起关闭时也会触发
        /// 所以事件注销操作只能写在OnNavigatingFrom事件中。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            ViewBase_SavingState();
        }
        #endregion
    }

    /// <summary>
    /// 代表将处理 <see cref="NavigationHelper.LoadState"/> 事件的方法
    /// </summary>
    public delegate void LoadStateEventHandler(object sender, LoadStateEventArgs e);
    /// <summary>
    /// 代表将处理 <see cref="NavigationHelper.SaveState"/> 事件的方法
    /// </summary>
    public delegate void SaveStateEventHandler(object sender, SaveStateEventArgs e);

    /// <summary>
    /// 一个类，用于存放在某页尝试加载状态时所需的事件数据。
    /// </summary>
    public class LoadStateEventArgs : EventArgs
    {
        /// <summary>
        /// 最初请求此页时传递给 <see cref="Frame.Navigate(Type, Object)"/> 
        /// 的参数值。
        /// </summary>
        public Object NavigationParameter { get; private set; }
        /// <summary>
        /// 此页在以前会话期间保留的状态
        /// 的字典。 首次访问某页时，此项将为 null。
        /// </summary>
        public Dictionary<string, Object> PageState { get; private set; }

        /// <summary>
        /// 初始化 <see cref="LoadStateEventArgs"/> 类的新实例。
        /// </summary>
        /// <param name="navigationParameter">
        /// 最初请求此页时传递给 <see cref="Frame.Navigate(Type, Object)"/> 
        /// 的参数值。
        /// </param>
        /// <param name="pageState">
        /// 此页在以前会话期间保留的状态
        /// 的字典。 首次访问某页时，此项将为 null。
        /// </param>
        public LoadStateEventArgs(Object navigationParameter, Dictionary<string, Object> pageState)
            : base()
        {
            this.NavigationParameter = navigationParameter;
            this.PageState = pageState;
        }
    }
    /// <summary>
    /// 一个类，用于存放在某页尝试保存状态时所需的事件数据。
    /// </summary>
    public class SaveStateEventArgs : EventArgs
    {
        /// <summary>
        /// 要填入可序列化状态的空字典。
        /// </summary>
        public Dictionary<string, Object> PageState { get; private set; }

        /// <summary>
        /// 初始化 <see cref="SaveStateEventArgs"/> 类的新实例。
        /// </summary>
        /// <param name="pageState">要使用可序列化状态填充的空字典。</param>
        public SaveStateEventArgs(Dictionary<string, Object> pageState)
            : base()
        {
            this.PageState = pageState;
        }
    }

}
