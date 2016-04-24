using SuspendSample.Common;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace SuspendSample
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            var frameState = SuspensionManager.SessionStateForFrame(this.Frame);
            var _pageKey = "Page-" + this.GetType().ToString();
            var pageState = new Dictionary<String, Object>();

            pageState[nameof(txtInput)] = txtInput.Text;

            frameState[_pageKey] = pageState;
        }
        /// <summary>
        /// 恢复数据
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.NavigationMode == NavigationMode.New)
            {

            }
            else
            {
                var frameState = SuspensionManager.SessionStateForFrame(this.Frame);
                var _pageKey = "Page-" + this.GetType().ToString();

                var data = (Dictionary<String, Object>)frameState[_pageKey];
                if (data != null && data.ContainsKey(nameof(txtInput)))
                {
                    txtInput.Text = data[nameof(txtInput)].ToString();
                }
            }
        }
    }
}
