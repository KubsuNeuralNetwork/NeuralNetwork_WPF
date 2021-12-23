using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_Main.Components.Service;
using Microsoft.Research.DynamicDataDisplay.DataSources;
namespace WPF_Main.Components.Forms
{
    public class MyViewModel
    {
        public ObservableDataSource<Point> Data { get; set; }

        public MyViewModel()
        {
            Data = new ObservableDataSource<Point>();
        }
    }
    /// <summary>
    /// Логика взаимодействия для About.xaml
    /// </summary>
    public partial class About : Window
    {
        MyViewModel viewModel;
        public About()
        {
            InitializeComponent();

            viewModel = new MyViewModel();
            DataContext = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager manager = new Manager();
            About_textBox.Text = manager.Run();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            double[] my_array = new double[10];

            for (int i = 0; i < my_array.Length; i++)
            {
                my_array[i] = Math.Sin(i);
                viewModel.Data.Collection.Add(new Point(i, my_array[i]));
            }
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            double[] my_array = new double[10];

            for (int i = 0; i < my_array.Length; i++)
            {
                my_array[i] = Math.Sin(i);
                viewModel.Data.Collection.Add(new Point(i, my_array[i]));
            }
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            int i = viewModel.Data.Collection.Count;
            viewModel.Data.Collection.Add(new Point(i, Math.Sin(i)));
        }
    }
}
