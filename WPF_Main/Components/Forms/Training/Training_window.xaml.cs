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
        private LinkedList<NeuralNetwork> nets;

        private int forTestingPercent;
        private int countOfNets;
        private float learningTemp;
        private bool isEpoch;
        private int epochCount;
        private bool isCost;
        private float cost;

        private const int limitForTestingPercent = 100;
        private const int limitCountOfNets = 5;
        private const float limitLearningTemp = 2f;
        private const int limitEpochCount = 1000000;
        private const float limitCost = 2f;

        public Training_window()
        {
            InitializeComponent();
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            learning_Sample = liveParams.Learning_Sample;
            forTestingPercent = liveParams.ForTestingPercent;
            countOfNets = liveParams.CountOfNets;
            learningTemp = liveParams.LearningTemp;
            isEpoch = liveParams.IsEpoch;
            isCost = liveParams.IsCost;
            epochCount = liveParams.Epoch;
            cost = liveParams.Cost;

            ForTesting_textBox.Text = Convert.ToString(forTestingPercent);
            CountOfNets_textBox.Text = Convert.ToString(countOfNets);
            learninTemp_textBox.Text = Convert.ToString(learningTemp);
            Epoch_textBox.Text = Convert.ToString(epochCount);
            Cost_textBox.Text = Convert.ToString(cost);
            Epoch_checkBox.IsChecked = isEpoch;
            Cost_checkBox.IsChecked = isCost;
        }

        internal Learning_sample Learning_Sample { get => learning_Sample; set => learning_Sample = value; }
        internal LiveParams LiveParams { get => liveParams; set => liveParams = value; }
        public int ForTestingPercent { get => forTestingPercent; set => forTestingPercent = value; }
        public int CountOfNets { get => countOfNets; set => countOfNets = value; }
        public float LearningTemp { get => learningTemp; set => learningTemp = value; }
        public int EpochCount { get => epochCount; set => epochCount = value; }
        public float Cost { get => cost; set => cost = value; }
        public bool IsEpoch { get => isEpoch; set => isEpoch = value; }
        public bool IsCost { get => isCost; set => isCost = value; }
        public LinkedList<NeuralNetwork> Nets { get => nets; set => nets = value; }

        private void Back_button_Click(object sender, RoutedEventArgs e)
        {
            LearningSet_window mainWindow = new LearningSet_window();
            liveParams.LearningTemp = learningTemp;
            liveParams.ForTestingPercent = forTestingPercent;
            liveParams.CountOfNets = countOfNets;
            liveParams.Epoch = epochCount;
            liveParams.Cost = cost;
            liveParams.IsCost = isCost;
            liveParams.IsEpoch = isEpoch;
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
        private void ForTesting_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                textBox.Text = textBox.Text.Replace(" ", string.Empty);
                textBox.Text = Layer.limitNum(textBox.Text, limitForTestingPercent);
                textBox.Text = Layer.deleteNotUsedNull(textBox.Text);
                forTestingPercent = Convert.ToInt32(textBox.Text);
            }
            catch (FormatException)
            {
                forTestingPercent = limitForTestingPercent;
                liveParams.ForTestingPercent = forTestingPercent;
            }
        }
        private void CountOfNets_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                textBox.Text = textBox.Text.Replace(" ", string.Empty);
                textBox.Text = Layer.limitNum(textBox.Text, limitCountOfNets);
                textBox.Text = Layer.deleteNotUsedNull(textBox.Text);
                countOfNets = Convert.ToInt32(textBox.Text);
            }
            catch (FormatException)
            {
                countOfNets = limitCountOfNets;
                liveParams.CountOfNets = countOfNets;
            }

        }

        private void learninTemp_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                textBox.Text = textBox.Text.Replace(" ", string.Empty);
                textBox.Text = Layer.limitDouble(textBox.Text, limitLearningTemp);
                textBox.Text = Layer.deleteNotUsedNull(textBox.Text);
                learningTemp = float.Parse(textBox.Text);
            }
            catch (FormatException)
            {
                learningTemp = limitLearningTemp;
                liveParams.LearningTemp = learningTemp;
            }
        }
        private void Epoch_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                textBox.Text = textBox.Text.Replace(" ", string.Empty);
                textBox.Text = Layer.limitNum(textBox.Text, limitEpochCount);
                textBox.Text = Layer.deleteNotUsedNull(textBox.Text);
                epochCount = Convert.ToInt32(textBox.Text);
            }
            catch (FormatException)
            {
                epochCount = limitEpochCount;
                liveParams.Epoch = epochCount;
            }
        }
        private void Cost_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                textBox.Text = textBox.Text.Replace(" ", string.Empty);
                textBox.Text = Layer.limitDouble(textBox.Text, limitCost);
                textBox.Text = Layer.deleteNotUsedNull(textBox.Text);
                cost = float.Parse(textBox.Text);
            }
            catch (FormatException)
            {
                cost = limitCost;
                liveParams.Cost = cost;
            }
        }

        private void learninTemp_textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Layer.InputDouble(sender, e);
        }

        private void Epoch_textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Layer.InputDigits(sender, e);
        }

        private void Cost_textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Layer.InputDigits(sender, e);
        }

        private void CountOfNets_textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Layer.InputDigits(sender, e);
        }
        private void ForTesting_textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Layer.InputDigits(sender, e);
        }

        private void Epoch_checkBox_Checked(object sender, RoutedEventArgs e)
        {
            Epoch_textBox.IsEnabled = true;
            isEpoch = true;
            epochCount = Convert.ToInt32(Epoch_textBox.Text);
        }

        private void Cost_checkBox_Checked(object sender, RoutedEventArgs e)
        {
            
            Cost_textBox.IsEnabled = true;
            isCost = true;
            cost = float.Parse(Cost_textBox.Text);

        }

        private void Epoch_checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            epochCount = 10000;
            isEpoch = false;
            Epoch_textBox.IsEnabled = false;
        }

        private void Cost_checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            cost = 0.01f;
            isCost = false;
            Cost_textBox.IsEnabled = false;
        }

        private void StartLearning_button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
