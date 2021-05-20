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

namespace WPF_Main.Components.Forms.Training
{
    /// <summary>
    /// Логика взаимодействия для Training_window.xaml
    /// </summary>
    public partial class Training_window : Window
    {
        private LiveParams liveParams;

        private Learning_sample learning_Sample;

        public Training_window()
        {
            InitializeComponent();
        }

        internal Learning_sample Learning_Sample { get => learning_Sample; set => learning_Sample = value; }
        internal LiveParams LiveParams { get => liveParams; set => liveParams = value; }
    }
}
