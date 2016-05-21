using EfSqliteUwpDemo.Models;
using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 を参照してください

namespace EfSqliteUwpDemo
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
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
            using (var database = new SensorDbContext())
            {
                Sensors.ItemsSource = database.Sensors.ToList();
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            using (var database = new SensorDbContext())
            {
                var sensor = new Sensor
                {
                    SensorId = Guid.NewGuid(),
                    Location = $"Room{DateTime.Now}"
                };

                database.Sensors.Add(sensor);
                database.SaveChanges();

                Sensors.ItemsSource = database.Sensors.ToList();
            }
        }
    }
}
