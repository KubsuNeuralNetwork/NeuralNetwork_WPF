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
using WPF_Main.Components;

namespace WPF_Main.Components.Forms.LearningSet
{
    /// <summary>
    /// Логика взаимодействия для LearningSet_window.xaml
    /// </summary>
    public partial class LearningSet_window : Window
    {
        private LiveParams liveParams;

        private Learning_sample learning_Sample;

        public LearningSet_window()
        { 
            InitializeComponent();
        }
        private void LerningSet_window_Activated(object sender, EventArgs e)
        {
            learning_Sample = liveParams.Learning_Sample;
            foreach (string str in learning_Sample.getDictionaryKeys())
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = str;
                item.Height = 20;
                item.FontWeight = FontWeights.Bold;
                LS_window_ListBox_input.Items.Add(item);
            }

        }
        internal Learning_sample Learning_Sample { get => learning_Sample; set => learning_Sample = value; }
        internal LiveParams LiveParams { get => liveParams; set => liveParams = value; }

        private void LS_window_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }



        private void Next_button_Click(object sender, RoutedEventArgs e)
        {
            LearningSet_window learningSet_Window = new LearningSet_window();
            learningSet_Window.Learning_Sample = learning_Sample;
            learningSet_Window.Show();
            this.Close();
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

        private void Back_button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.LiveParams = liveParams;
            mainWindow.Show();
            this.Close();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
