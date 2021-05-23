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
using System.Text.RegularExpressions;
using WPF_Main.Components.API;
using WPF_Main.Components.Forms.Training;
using WPF_Main.Components.Forms.AboutActivationFunctions;
using WPF_Main.Components.Forms.LearningSet;

namespace WPF_Main.Components.Forms.Training
{
    /// <summary>
    /// Логика взаимодействия для Training_window.xaml
    /// </summary>
    public partial class Training_window : Window
    {
        private LiveParams liveParams;

        private Learning_sample learning_Sample;

        private int forTestingPercent = 80;
        private int countOfNets = 1;
        private float learningTemp = 0.01f;
        private int epochCount = 10000;
        private float cost = 0.01f;

        public Training_window()
        {
            InitializeComponent();
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            learning_Sample = liveParams.Learning_Sample;
            
        }

        internal Learning_sample Learning_Sample { get => learning_Sample; set => learning_Sample = value; }
        internal LiveParams LiveParams { get => liveParams; set => liveParams = value; }
        public int ForTestingPercent { get => forTestingPercent; set => forTestingPercent = value; }
        public int CountOfNets { get => countOfNets; set => countOfNets = value; }
        public float LearningTemp { get => learningTemp; set => learningTemp = value; }
        public int EpochCount { get => epochCount; set => epochCount = value; }
        public float Cost { get => cost; set => cost = value; }

        private void Back_button_Click(object sender, RoutedEventArgs e)
        {
            LearningSet_window mainWindow = new LearningSet_window();
            mainWindow.LiveParams = liveParams;
            mainWindow.Show();
            this.Close();
        }

        private void Next_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void About_button_Click(object sender, RoutedEventArgs e)
        {

        }




        private void ForTesting_textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Layer.InputDigits(sender, e);
        }

        private void ForTesting_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                textBox.Text = textBox.Text.Replace(" ", string.Empty);
                textBox.Text = Layer.limitNum(textBox.Text, 100);
                textBox.Text = Layer.deleteNotUsedNull(textBox.Text);
                forTestingPercent = Convert.ToInt32(textBox.Text);
            }
            catch (FormatException)
            {
                forTestingPercent = 100;
            }
        }


    }
}
