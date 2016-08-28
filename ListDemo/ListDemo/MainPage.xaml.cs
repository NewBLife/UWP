using ListDemo.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace ListDemo
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

        private void btnGridView_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GridViewPage));
        }

        private void btnFlipView_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FlipViewPage));
        }

        private void btnListView_Click(object sender, RoutedEventArgs e)
        {

            Frame.Navigate(typeof(ListViewPage));
        }

        private void btnSplitView_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SplitViewPage));

        }

        private void btnListBox_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ListBoxPage));

        }

        private void btnItemsControl_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ItemscontrolPage));

        }

        private void btnPivot_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PivotPage));

        }
    }
}
