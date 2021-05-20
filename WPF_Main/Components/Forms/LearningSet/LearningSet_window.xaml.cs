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

namespace WPF_Main.Components.Forms.LearningSet
{
    /// <summary>
    /// Логика взаимодействия для LearningSet_window.xaml
    /// </summary>
    public partial class LearningSet_window : Window
    {
        private LiveParams liveParams;

        private Learning_sample learning_Sample;

        private LinkedList<Layer> layers;
        private int countOfSecretLayers = 0;

        public LearningSet_window()
        { 
            InitializeComponent();
            SecretLayers_textBox.Text = Convert.ToString(countOfSecretLayers);
        }
        private void LerningSet_window_Activated(object sender, EventArgs e)
        {
            learning_Sample = liveParams.Learning_Sample;
            foreach (string name in learning_Sample.getDictionaryKeys())
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = name;
                item.Height = 20;
                if (learning_Sample.isTargetItem(name) == 1)
                    item.FontWeight = FontWeights.Bold;
                if (learning_Sample.isTargetItem(name) == -1)
                {
                    item.Foreground = Brushes.LightGray;
                }
                LS_window_ListBox_input.Items.Add(item);

                layers = liveParams.Layers;
                createLayers();
            }

        }
        internal Learning_sample Learning_Sample { get => learning_Sample; set => learning_Sample = value; }
        internal LiveParams LiveParams { get => liveParams; set => liveParams = value; }

        private void LS_window_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)LS_window_ListBox_input.SelectedItem;
            int target = learning_Sample.isTargetItem((string)item.Content);
            Load_Target_RadioButton(target);
        }

        private void Load_Target_RadioButton(int target)
        {
            switch (target)
            {
                case 1:
                    Target_vector_radioButton.IsChecked = true;
                    break;
                case 0:
                    Input_vector_RadioButton.IsChecked = true;
                    break;
                case -1:
                    NotUsed_vector_radioButton.IsChecked = true;
                    break;
            }

        }



        private void Next_button_Click(object sender, RoutedEventArgs e)
        {
            Training_window training_window = new Training_window();
            training_window.LiveParams = liveParams;
            training_window.Show();
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

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void Input_vector_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)LS_window_ListBox_input.SelectedItem;
            learning_Sample.changeTargetItem((string)item.Content, 0);
            item.FontWeight = FontWeights.Normal;
            item.Foreground = Brushes.Black;
        }

        private void Target_vector_radioButton_Checked(object sender, RoutedEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)LS_window_ListBox_input.SelectedItem;
            learning_Sample.changeTargetItem((string)item.Content, 1);
            item.FontWeight = FontWeights.Bold;
            item.Foreground = Brushes.Black;
        }

        private void NotUsed_vector_radioButton_Checked(object sender, RoutedEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)LS_window_ListBox_input.SelectedItem;
            learning_Sample.changeTargetItem((string)item.Content, -1);
            item.FontWeight = FontWeights.Normal;
            item.Foreground = Brushes.LightGray;
        }

        private void SecretLayers_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SecretLayers_textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputDigits(e);
        }

        private void createLayers()
        {
            if (SecretLayers_textBox.Text.Trim() != "")
            {
                int countOfNeuronOnInputLayer = learning_Sample.count_of_inputVectors();
                int countOfNeuronOnTargetLayer = learning_Sample.count_of_TargetVectors();
                int countOfLayers = 2 + countOfSecretLayers;

                if (layers.Count == 0)
                {
                    Layer layer;

                    layer = new Layer(1, countOfNeuronOnInputLayer, "Входной слой");
                    layers.AddLast(layer);

                    layer = new Layer(countOfLayers, countOfNeuronOnTargetLayer, "Выходной слой");
                    layers.AddLast(layer);
                }
                drawLayers();               
            }
        }

        private void drawLayers()
        {
            SecretLayers_ListBox.Items.Clear();
            foreach (Layer layer in layers)
            {
                SecretLayers_ListBox.Items.Add(layer.createLayerView());
            }
        }

        private void deleteLastSecretLayer()
        {
            Layer layer = new Layer(layers.Count - 1, "Скрытий слой");
            layers.Last.Value.numberOfLayer--;
            layers.Remove(layer);
            
        }
        private void addLastSecretLayer()
        {
            Layer layer = new Layer(layers.Count, "Скрытий слой");
            Layer lastLayer = layers.Last.Value;
            lastLayer.numberOfLayer++;
            layers.AddLast(lastLayer);
            layers.Last.Previous.Value = layer;
        }

        public void InputDigits(TextCompositionEventArgs e)
        {
            if ((e.Text) == null || !(e.Text).All(char.IsDigit))
            {
                e.Handled = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int prev_countOfSecretLayers = countOfSecretLayers;
            this.countOfSecretLayers = Convert.ToInt32(SecretLayers_textBox.Text.Trim());
            if (prev_countOfSecretLayers < countOfSecretLayers)
            {
                for (int i = 0; i < Math.Abs(countOfSecretLayers - prev_countOfSecretLayers); i++)
                {
                    addLastSecretLayer();
                    drawLayers();
                }
            }
            if (prev_countOfSecretLayers > countOfSecretLayers)
            {
                for (int i = 0; i < Math.Abs(countOfSecretLayers - prev_countOfSecretLayers); i++)
                {
                    deleteLastSecretLayer();
                    drawLayers();
                }
            }
        }

    }
}
