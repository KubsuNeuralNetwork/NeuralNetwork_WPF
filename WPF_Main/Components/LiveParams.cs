using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Main.Components.Service;

namespace WPF_Main.Components
{
    class LiveParams
    {
        private string path;
        private Learning_sample learning_Sample;

        public LiveParams()
        {
            path = null;
            learning_Sample = null;
        }

        public string Path { get => path; set => path = value; }
        internal Learning_sample Learning_Sample { get => learning_Sample; set => learning_Sample = value; }
    }
}
