﻿using ListDemo.Model;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace ListDemo.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class GridViewPage : Page
    {
        public GridViewPage()
        {
            this.InitializeComponent();

            this.pageList.ItemsSource = PageList.GetData();
        }
        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
