using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Main.Components.Service;
using WPF_Main.Components.API;

namespace WPF_Main.Components
{
    class LiveParams
    {
        // получаем в mainWindow
        private string path;
        private Learning_sample learning_Sample;

        // получаем в LearningSet_window
        private LinkedList<Layer> layers;
        private int countOfSecretLayers;

        // получаем в Training_window
        private LinkedList<NeuralNetwork> nets;
        private int epoch; 
        private int forTestingPercent;
        private int countOfNets;
        private float learningTemp;
        private float cost;
        private bool isEpoch;
        private bool isCost;
        private int currEpoch;
        private float currCost;
        public LiveParams()
        {
            path = null;
            learning_Sample = null;
            layers = new LinkedList<Layer>();
            nets = new LinkedList<NeuralNetwork>();
            countOfSecretLayers = 0;
            epoch = 10000;
            forTestingPercent = 80;
            countOfNets = 1;
            learningTemp = 0.01f;
            cost = 0.5f;
            isCost = false;
            isEpoch = false;
            currEpoch = 0;
            currCost = 0f;
        }

        public int[] getLayers()
        {
            int[] result = new int[layers.Count];
            int i = 0;
            foreach (Layer item in layers)
            {
                result[i] = item.countOfNeurons;
                i++;
            }
            return result;
        }

        public string[] getActivations()
        {
            string[] result = new string[layers.Count-1];
            int i = -1;
            foreach (Layer item in layers)
            { 
                if (i == -1)
                {
                    i++;
                    continue;
                }
                result[i] = Convert.ToString(item.function);
            }
            return result; 
        }

        public string Path { get => path; set => path = value; }
        public LinkedList<Layer> Layers { get => layers; set => layers = value; }
        public int CountOfSecretLayers { get => countOfSecretLayers; set => countOfSecretLayers = value; }
        public LinkedList<NeuralNetwork> Nets { get => nets; set => nets = value; }
        public int Epoch { get => epoch; set => epoch = value; }
        public int ForTestingPercent { get => forTestingPercent; set => forTestingPercent = value; }
        public int CountOfNets { get => countOfNets; set => countOfNets = value; }
        public float LearningTemp { get => learningTemp; set => learningTemp = value; }
        public float Cost { get => cost; set => cost = value; }
        public bool IsEpoch { get => isEpoch; set => isEpoch = value; }
        public bool IsCost { get => isCost; set => isCost = value; }
        public int CurrEpoch { get => currEpoch; set => currEpoch = value; }
        public float CurrCost { get => currCost; set => currCost = value; }
        internal Learning_sample Learning_Sample { get => learning_Sample; set => learning_Sample = value; }

        public string[] getActivationsArray()
        {
            LinkedList<string> activation = new LinkedList<string>();
            foreach(Layer layer in layers)
            {
                activation.AddLast(layer.function.ToString());
            }
            return activation.ToArray<string>();
        }
    }
}
