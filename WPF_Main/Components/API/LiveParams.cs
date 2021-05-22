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

        public LiveParams()
        {
            path = null;
            learning_Sample = null;
            layers = new LinkedList<Layer>();
            countOfSecretLayers = 0;
        }

        public string Path { get => path; set => path = value; }
        public LinkedList<Layer> Layers { get => layers; set => layers = value; }
        public int CountOfSecretLayers { get => countOfSecretLayers; set => countOfSecretLayers = value; }
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
