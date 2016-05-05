// ***********************************************************************
// FileName:IPageViewModelBase
// Description:ViewModel共通接口
// Project:
// Author:NewBLife 云中客
// Created:2016/5/4 21:55:59
// Copyright (c) 2016 NewBLife,All rights reserved.
// ***********************************************************************
using System.Collections.Generic;

namespace UWPDev.Common.Interface
{
    /// <summary>
    /// ViewModel共通接口
    /// PageViewModelBase继承实现
    /// </summary>
    public interface IPageViewModelBase
    {
        #region 进程生命期管理
        /// <summary>
        /// 在此页将要在 Frame 中显示时
        /// 从以前保存的状态恢复页面数据。
        /// </summary>
        /// <param name="navParameter">页面对象</param>
        /// <param name="state">以前保存的状态字典</param>
        void LoadState(object navParameter, Dictionary<string, object> state);
        /// <summary>
        /// 当此页即将不再在 Frame 中显示时
        /// 注销此页事件与通信消息等。
        /// （因为SaveState在应用挂起关闭时也会触发）
        /// </summary>
        void SavingState();
        /// <summary>
        /// 当此页不再在 Frame 中显示时
        /// 保存此页数据到字典中。
        /// </summary>
        /// <param name="state">字典</param>
        void SaveState(Dictionary<string, object> state);
        /// <summary>
        /// 从保存状态字典中取得数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="state">保存状态字典</param>
        /// <param name="stateKey">字典主键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>取得结果</returns>
        T RestoreStateItem<T>(Dictionary<string, object> state, string stateKey, T defaultValue = default(T));

        #endregion
    }
}
