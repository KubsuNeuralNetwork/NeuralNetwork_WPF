using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WPF_Main.Components.API
{

    public class Layer
    {
        public ActivationFunctions function;
        public int countOfNeurons;
        public int numberOfLayer;
        public string nameOfLayer;
        private bool isFirst;

        public bool IsFirst { get => isFirst; set => isFirst = value; }

        public Layer()
        {
        }
        public Layer(int numberOfLayer)
        {
            this.countOfNeurons = 10;
            this.numberOfLayer = numberOfLayer;
            this.nameOfLayer = "Слой";
            this.isFirst = false;
            function = ActivationFunctions.sigmoid;
        }
        public Layer(int numberOfLayer, ActivationFunctions activation)
        {
            this.countOfNeurons = 10;
            this.numberOfLayer = numberOfLayer;
            this.nameOfLayer = "Слой";
            this.isFirst = false;
            function = activation;
        }
        public Layer(int numberOfLayer, bool isFirst)
        {
            this.countOfNeurons = 10;
            this.numberOfLayer = numberOfLayer;
            this.nameOfLayer = "Слой";
            this.isFirst = isFirst;
            function = ActivationFunctions.sigmoid;
        }
        public Layer(int numberOfLayer, string nameOfLayer)
        {
            this.countOfNeurons = 10;
            this.numberOfLayer = numberOfLayer;
            this.nameOfLayer = nameOfLayer;
            this.isFirst = false;
            function = ActivationFunctions.sigmoid;
        }

        public Layer(int numberOfLayer, int countOfNeurons, string nameOfLayer)
        {
            this.function = ActivationFunctions.sigmoid;
            this.countOfNeurons = countOfNeurons;
            this.numberOfLayer = numberOfLayer;
            this.nameOfLayer = nameOfLayer;
            this.isFirst = false;
        }
        public Layer(int numberOfLayer, int countOfNeurons, string nameOfLayer, ActivationFunctions function, bool isFirst)
        {
            this.function = function;
            this.countOfNeurons = countOfNeurons;
            this.numberOfLayer = numberOfLayer;
            this.nameOfLayer = nameOfLayer;
            this.isFirst = isFirst;
        }



        public ListBoxItem createLayerView()
        {
            ListBoxItem listBoxItem = new ListBoxItem();
            listBoxItem.Height = 33;

            Label label_Layer_number = new Label();
            label_Layer_number.Content = Convert.ToString(numberOfLayer - 2);
            label_Layer_number.Width = 25;
            label_Layer_number.HorizontalContentAlignment = HorizontalAlignment.Right;

            Label label_Layer_name = new Label();
            label_Layer_name.Content = nameOfLayer;
            label_Layer_name.Width = 45;
            label_Layer_name.HorizontalContentAlignment = HorizontalAlignment.Left;
            createRightBorder(label_Layer_name);


            TextBox textBox = new TextBox();
            textBox.PreviewTextInput += InputDigits;
            textBox.Text = Convert.ToString(countOfNeurons);
            textBox.TextChanged += TextBox_TextChanged;
            textBox.VerticalContentAlignment = VerticalAlignment.Center;
            textBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            textBox.Margin = new Thickness(5, 0, 0, 0);
            textBox.Width = 50;

            Label label_CountOfNeurons = new Label();
            label_CountOfNeurons.Content = "Нейронов";
            label_CountOfNeurons.Width = 70;
            createRightBorder(label_CountOfNeurons);


            ComboBox comboBox = new ComboBox();
            comboBox.Items.Add(ActivationFunctions.sigmoid);
            comboBox.Items.Add(ActivationFunctions.leakyrelu);
            comboBox.Items.Add(ActivationFunctions.tahn);
            comboBox.Items.Add(ActivationFunctions.relu);
            comboBox.SelectedItem = comboBox.Items.GetItemAt(getActivationFunctionNumber(function));
            comboBox.SelectionChanged += ComboBox_SelectionChanged;
            comboBox.Margin = new Thickness(5,0,0,0);
            comboBox.Width = 85;
            this.function = (ActivationFunctions)comboBox.SelectedItem;

            Label label_ActivationFunction = new Label();
            label_ActivationFunction.Content = "Функция активации";
            label_ActivationFunction.Width = 130;
            createRightBorder(label_ActivationFunction);


            StackPanel stackPanel = new StackPanel();
            stackPanel.Children.Add(label_Layer_number);
            stackPanel.Children.Add(label_Layer_name);
            stackPanel.Children.Add(textBox);
            stackPanel.Children.Add(label_CountOfNeurons);
            stackPanel.Children.Add(comboBox);
            stackPanel.Children.Add(label_ActivationFunction);
            stackPanel.Orientation = Orientation.Horizontal;


            listBoxItem.Content = stackPanel;

            listBoxItem.BorderBrush = Brushes.Black;
            listBoxItem.BorderThickness = new Thickness(0, 1, 0, 0);
            if (isFirst)
                listBoxItem.BorderThickness = new Thickness(0, 0, 0, 0);
            return listBoxItem;
        }

        public static string limitNum(string str, int max)
        {
            try
            {
                int num = Convert.ToInt32(str);
                if (num > max)
                    throw new OverflowException();
                return str;
            }
            catch (OverflowException)
            {
                return Convert.ToString(max);
            }

        }

        private Thickness createRightBorder(Label sender)
        {
            sender.BorderBrush = Brushes.Black;
            Thickness thickness = sender.BorderThickness;
            sender.BorderThickness = new Thickness(0, thickness.Top, 1,thickness.Bottom);
            return new Thickness();
        }

        public static string deleteNotUsedNull(string str)
        {
            for (int i = 0; i < str.Length && str[i] == '0'; i++)
            {
                str = str.Remove(0, 1);
            }
            return str;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                textBox.Text = limitNum(textBox.Text, 10000);
                textBox.Text = deleteNotUsedNull(textBox.Text);
                this.countOfNeurons = Convert.ToInt32(textBox.Text);
            } catch (FormatException)
            {
                countOfNeurons = 0;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            this.function = (ActivationFunctions)comboBox.SelectedItem;
        }

        public override bool Equals(object obj)
        {
            return obj is Layer layer &&
                   numberOfLayer == layer.numberOfLayer && nameOfLayer == layer.nameOfLayer;
        }
        public void InputDigits(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[^0-9 ]+$");
            e.Handled = regex.IsMatch(e.Text);
        }

        private int getActivationFunctionNumber(ActivationFunctions activation)
        {
            switch (activation)
            {
                case ActivationFunctions.sigmoid:
                    return 0;
                case ActivationFunctions.leakyrelu:
                    return 1;
                case ActivationFunctions.tahn:
                    return 2;
                case ActivationFunctions.relu:
                    return 3;
                default:
                    return 0;
            }
        }
        public int CompareTo(Layer layer) => layer.numberOfLayer - this.numberOfLayer;
    }
}
