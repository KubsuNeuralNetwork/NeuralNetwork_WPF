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
using WPF_Main.Components.API;
using WPF_Main.Components.Forms;
using WPF_Main.Components.Forms.Training;
using System.IO;
using System.Windows.Forms;

namespace WPF_Main.Components.Forms.Testing
{
    /// <summary>
    /// Логика взаимодействия для Testing_window.xaml
    /// </summary>
    public partial class Testing_window : Window
    {
        private LiveParams liveParams;

        private Learning_sample learning_Sample;
        private LinkedList<NeuralNetwork> nets;


        private int count_of_inputs;
        private int count_of_target;
        private float[] input;
        private float[] output;
        public Testing_window()
        {
            InitializeComponent();
        }

        public LinkedList<NeuralNetwork> Nets { get => nets; set => nets = value; }
        internal Learning_sample Learning_Sample { get => learning_Sample; set => learning_Sample = value; }
        internal LiveParams LiveParams { get => liveParams; set => liveParams = value; }

        private void Testing_Window_Activated(object sender, EventArgs e)
        {
            learning_Sample = liveParams.Learning_Sample;
            nets = liveParams.Nets;
            count_of_inputs = learning_Sample.count_of_inputVectors();
            count_of_target = learning_Sample.count_of_TargetVectors();
            input = new float[count_of_inputs];
            output = new float[count_of_target];
            drawValues();
        }

        private void drawValues()
        {
            Input_Layers_ListBox.Items.Clear();
            Target_Layers_ListBox.Items.Clear();
            int i = 1;
            foreach (VectorsNames vector in learning_Sample.IsTarget)
            {
                if (vector.IsTarget == 0)
                    Input_Layers_ListBox.Items.Add(vector.getView(i++));
                if (vector.IsTarget == 1)
                    Target_Layers_ListBox.Items.Add(vector.getView(i++));
            }
        }

        private void Back_button_Click(object sender, RoutedEventArgs e)
        {
            Training_window newWindow = new Training_window();
            newWindow.LiveParams = liveParams;
            newWindow.Show();
            this.Close();
        }

        private void getValues()
        {
            int currInput = 0;
            foreach (VectorsNames vector in learning_Sample.IsTarget)
            {
                if (vector.IsTarget == 0)
                {
                    input[currInput] = vector.Norm_value;
                    currInput++;
                }
            }
        }

        private void Check_button_Click(object sender, RoutedEventArgs e)
        {
            getValues();
            foreach(NeuralNetwork net in nets)
            {
                output = net.FeedForward(input);
                int currOutput = 0;
                foreach (VectorsNames vector in learning_Sample.IsTarget)
                {
                    if (vector.IsTarget == 1)
                    {
                        vector.setCurrent_value(output[currOutput]);
                        currOutput++;
                    }
                }
            }
            drawValues();
        }

        private void SaveNN_button_Click(object sender, RoutedEventArgs e)
        {

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                LiveParams.WriteToBinaryFile<LiveParams>(saveFileDialog1.FileName, liveParams);
            }
        }
    }
}
