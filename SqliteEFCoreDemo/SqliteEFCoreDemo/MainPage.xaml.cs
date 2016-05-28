using System.Linq;
using SqliteEFCoreDemo.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace SqliteEFCoreDemo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new EfDbContext())
            {
                lstCourse.ItemsSource = db.Courses.ToList();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new EfDbContext())
            {
                var newCourse = new Course
                {
                    ID = txbCId.Text.Trim(),
                    Name = txbCName.Text.Trim()
                };

                txbCId.Text = string.Empty;
                txbCName.Text = string.Empty;

                db.Courses.Add(newCourse);
                db.SaveChanges();

                lstCourse.ItemsSource = db.Courses.ToList();
            }
        }
    }
}
