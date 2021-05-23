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
        public float activationParam = 1;
        public int countOfNeurons;
        public int numberOfLayer;
        public string nameOfLayer;
        private bool isFirst;

        public bool IsFirst { get => isFirst; set => isFirst = value; }


        #region Конструкторы
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

        #endregion

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


            TextBox countOfNeurons_textBox = new TextBox();
            countOfNeurons_textBox.PreviewTextInput += InputDigits;
            countOfNeurons_textBox.Text = Convert.ToString(countOfNeurons);
            countOfNeurons_textBox.TextChanged += TextBox_TextChanged;
            countOfNeurons_textBox.VerticalContentAlignment = VerticalAlignment.Center;
            countOfNeurons_textBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            countOfNeurons_textBox.Margin = new Thickness(5, 0, 0, 0);
            countOfNeurons_textBox.Width = 50;

            Label label_CountOfNeurons = new Label();
            label_CountOfNeurons.Content = "Нейронов";
            label_CountOfNeurons.Width = 70;
            createRightBorder(label_CountOfNeurons);


            Label label_ActivationFunction = new Label();
            label_ActivationFunction.Content = "Функция активации:";
            label_ActivationFunction.Width = 132;

            ComboBox comboBox = new ComboBox();
            comboBox.Items.Add(ActivationFunctions.sigmoid);
            comboBox.Items.Add(ActivationFunctions.leakyrelu);
            comboBox.Items.Add(ActivationFunctions.tahn);
            comboBox.Items.Add(ActivationFunctions.relu);
            comboBox.SelectedItem = comboBox.Items.GetItemAt(getActivationFunctionNumber(function));
            comboBox.SelectionChanged += ComboBox_SelectionChanged;
            comboBox.Margin = new Thickness(5,0,5,0);
            comboBox.Width = 85;
            this.function = (ActivationFunctions)comboBox.SelectedItem;


            Label activationParam_label = new Label();
            activationParam_label.Content = "x:";
            activationParam_label.Width = 30;
            activationParam_label.BorderBrush = Brushes.Black;
            activationParam_label.BorderThickness = new Thickness(1, 0, 0, 0);

            TextBox activationParam_textBox = new TextBox();
            activationParam_textBox.Text = Convert.ToString(activationParam);
            activationParam_textBox.PreviewTextInput += InputDouble;
            activationParam_textBox.TextChanged += ActivationParam_textBox_TextChanged;
            activationParam_textBox.VerticalContentAlignment = VerticalAlignment.Center;
            activationParam_textBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            activationParam_textBox.Margin = new Thickness(0, 0, 0, 0);
            activationParam_textBox.Width = 50;

            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.Children.Add(label_Layer_number);
            stackPanel.Children.Add(label_Layer_name);
            stackPanel.Children.Add(countOfNeurons_textBox);
            stackPanel.Children.Add(label_CountOfNeurons);
            stackPanel.Children.Add(label_ActivationFunction);
            stackPanel.Children.Add(comboBox);
            stackPanel.Children.Add(activationParam_label);
            stackPanel.Children.Add(activationParam_textBox);


            listBoxItem.Content = stackPanel;

            listBoxItem.BorderBrush = Brushes.Black;
            listBoxItem.BorderThickness = new Thickness(0, 1, 0, 0);
            if (isFirst)
                listBoxItem.BorderThickness = new Thickness(0, 0, 0, 0);
            return listBoxItem;
        }

        private void ActivationParam_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Foreground = Brushes.Black;
            Regex regex = new Regex("^[0-9]*[.,]?[0-9]+$");
            bool isRegex = regex.IsMatch(textBox.Text);
            if (textBox.Text != "" && isRegex)
            {
                textBox.Text = limitDouble(textBox.Text, getActivationLimit());
                textBox.Text = deleteNotUsedNull(textBox.Text,false);
                this.activationParam = float.Parse(textBox.Text);
                textBox.Select(textBox.Text.Length, 0);
            } else
            {
                textBox.Foreground = Brushes.Red;
            }
        }

        private double getActivationLimit()
        {
            return 4.0;
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

        public static string limitDouble(string str, double max)
        {
            try
            {
                if (str != "")
                {
                    str = str.Replace(".", ",");
                    double num = Convert.ToDouble(str);
                    if (num > max)
                        throw new OverflowException();
                }
                return str;
            }
            catch (OverflowException)
            {
                return Convert.ToString(max);
            }
            catch (FormatException)
            {
                return str;
            }
        }

        private Thickness createRightBorder(Label sender)
        {
            sender.BorderBrush = Brushes.Black;
            Thickness thickness = sender.BorderThickness;
            sender.BorderThickness = new Thickness(0, thickness.Top, 1,thickness.Bottom);
            return new Thickness();
        }

        public static string deleteNotUsedNull(string str, bool isInt = true)
        {
            if (isInt)

            {
                for (int i = 0; i < str.Length && str[i] == '0'; i++)
                {
                    str = str.Remove(0, 1);
                }
            } else
            {
                while (str.Length >= 2 && str[1] == '0')
                {
                    str = str.Remove(0, 1);
                }
            }

            return str;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                textBox.Text = textBox.Text.Replace(" ", string.Empty);
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
        public static void InputDigits(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[^0-9]+$");
            e.Handled = regex.IsMatch(e.Text);
        }
        public static void InputDouble(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.,]+");
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
