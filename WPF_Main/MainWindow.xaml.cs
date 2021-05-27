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
using WPF_Main.Components.Forms;
using WPF_Main.Components.Forms.LearningSet;
using WPF_Main.Components.Service;
using WPF_Main.Components;


namespace WPF_Main
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LiveParams liveParams;

        private bool isErrorOpenFile;  
        private Learning_sample learning_Sample;
        private string path;

        public string Path { get => path; set => path = value; }
        internal LiveParams LiveParams { get => liveParams; set => liveParams = value; }

        public MainWindow()
        {
            liveParams = new LiveParams();
            InitializeComponent();
            isErrorOpenFile = false;
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            learning_Sample = liveParams.Learning_Sample;
            path = liveParams.Path;
            if (path != null)
            {
                File_text.Text = path;
            }
        }

        private void Button_OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Обучающая выборка (*TXT)|*.TXT";
            File_text.Foreground = Brushes.Black;
            if (openFileDialog.ShowDialog() == true)
            {

                if (isErrorOpenFile == true)
                {
                    File_text.Foreground = Brushes.Black;
                    isErrorOpenFile = false;
                }
                path = openFileDialog.FileName;
                liveParams.Path = path;
                File_text.Text = path;
                InitLearning_sample();
            }
        }

        private void InitLearning_sample()
        {
            try
            {
                learning_Sample = new Learning_sample(File.ReadAllText(path));
                liveParams.Learning_Sample = learning_Sample;
            } catch (System.ArgumentException)
            {
                File_text.Text = "Ошибка открытия файла";
                File_text.Foreground = Brushes.Red;
                isErrorOpenFile = true;
            }

        }
        private void Next_button_Click(object sender, RoutedEventArgs e)
        {
            if (learning_Sample == null && path != null)
                InitLearning_sample();
            if (learning_Sample != null && path != null)
            {
                LearningSet_window learningSet_Window = new LearningSet_window();
                learningSet_Window.LiveParams = liveParams;
                learningSet_Window.Show();
                this.Close();
            } else
            {
                File_text.Text = "Выберете файл";
                File_text.Foreground = Brushes.Coral;
            }
        }

        private void Exit_button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void About_button_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.Owner = this;
            about.ShowDialog();
        }

    }
}
