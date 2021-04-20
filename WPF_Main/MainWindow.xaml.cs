using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Main.Components.Models;

namespace WPF_Main
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true) {
                try
                {
                    Learning_sample ls = new Learning_sample(File.ReadAllText(openFileDialog.FileName));
                    File_text.Text = ls.Learning_sample_str;
                } catch (System.ArgumentException ex)
                {
                    File_text.Text = "Файл имена солбцов содержат одинаковые имена";
                    File_text.Foreground = Brushes.Red;
                }
            }
        }
    }
}
