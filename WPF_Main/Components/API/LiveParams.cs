using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Main.Components.Service;
using WPF_Main.Components.API;
using System.IO;

namespace WPF_Main.Components
{
    [Serializable]
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

        /// <summary>
        /// Writes the given object instance to a binary file.
        /// <para>Object type (and all child types) must be decorated with the [Serializable] attribute.</para>
        /// <para>To prevent a variable from being serialized, decorate it with the [NonSerialized] attribute; cannot be applied to properties.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the binary file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the binary file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        /// <summary>
        /// Reads an object instance from a binary file.
        /// </summary>
        /// <typeparam name="T">The type of object to read from the binary file.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the binary file.</returns>
        public static T ReadFromBinaryFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }
    }
}
