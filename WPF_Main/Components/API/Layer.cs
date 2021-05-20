using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPF_Main.Components.API
{

    public class Layer
    {
        public ActivationFunctions function;
        public int countOfNeurons;
        public int numberOfLayer;
        public string nameOfLayer;
        public Layer()
        {
        }
        public Layer(int numberOfLayer)
        {
            this.countOfNeurons = 10;
            this.numberOfLayer = numberOfLayer;
            function = ActivationFunctions.sigmoid;
        }
        public Layer(int numberOfLayer, string nameOfLayer)
        {
            this.countOfNeurons = 10;
            this.numberOfLayer = numberOfLayer;
            this.nameOfLayer = nameOfLayer;
            function = ActivationFunctions.sigmoid;
        }
        public Layer(int numberOfLayer, int countOfNeurons, string nameOfLayer)
        {
            function = ActivationFunctions.sigmoid;
            this.countOfNeurons = countOfNeurons;
            this.numberOfLayer = numberOfLayer;
            this.nameOfLayer = nameOfLayer;
        }



        public ListBoxItem createLayerView()
        {
            ListBoxItem listBoxItem = new ListBoxItem();
            listBoxItem.Height = 30;

            Label label = new Label();
            label.Content = Convert.ToString(numberOfLayer) + " " + nameOfLayer;

            ComboBox comboBox = new ComboBox();
            comboBox.Items.Add(ActivationFunctions.sigmoid);
            comboBox.Items.Add(ActivationFunctions.leakyrelu);
            comboBox.Items.Add(ActivationFunctions.tahn);
            comboBox.Items.Add(ActivationFunctions.relu);
            comboBox.SelectedItem = comboBox.Items.GetItemAt(0);
            comboBox.SelectionChanged += ComboBox_SelectionChanged;
            this.function = (ActivationFunctions)comboBox.SelectedItem;

            TextBox textBox = new TextBox();
            textBox.PreviewTextInput += InputDigits;
            textBox.Text = Convert.ToString(countOfNeurons);
            textBox.MinWidth = 70;

            StackPanel stackPanel = new StackPanel();          
            stackPanel.Children.Add(label);
            stackPanel.Children.Add(textBox);
            stackPanel.Children.Add(comboBox);
            stackPanel.Orientation = Orientation.Horizontal;
            

            listBoxItem.Content = stackPanel;

            return listBoxItem;
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
        public int CompareTo(Layer layer) => layer.numberOfLayer - this.numberOfLayer;
    }
}
