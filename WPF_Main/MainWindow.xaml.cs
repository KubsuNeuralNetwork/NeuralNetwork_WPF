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
using WPF_Main.Components.Forms;


namespace WPF_Main
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isErrorOpenFile;
        public MainWindow()
        {
            InitializeComponent();
            isErrorOpenFile = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true) {
                try
                {
                    if (isErrorOpenFile == true)
                    {
                        File_text.Foreground = Brushes.Black;
                        isErrorOpenFile = false;
                    }
                    Learning_sample ls = new Learning_sample(File.ReadAllText(openFileDialog.FileName));
                    File_text.Text = ls.Learning_sample_str;
                } catch (System.ArgumentException ex)
                {
                    File_text.Text = "Файл имена солбцов содержат одинаковые имена";
                    File_text.Foreground = Brushes.Red;
                    isErrorOpenFile = true;
                }
            }
        }
        private void Next_button_Click(object sender, RoutedEventArgs e)
        {
      
            
        }

        private void Exit_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void About_button_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.Owner = this;
            about.ShowDialog();
        }
    }
}
