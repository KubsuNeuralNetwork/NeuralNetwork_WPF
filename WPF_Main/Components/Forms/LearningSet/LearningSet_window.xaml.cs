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
using WPF_Main.Components.Models;

namespace WPF_Main.Components.Forms.LearningSet
{
    /// <summary>
    /// Логика взаимодействия для LearningSet_window.xaml
    /// </summary>
    public partial class LearningSet_window : Window
    {
        private Learning_sample learning_Sample;
        public LearningSet_window()
        {
            InitializeComponent();
        }

        internal Learning_sample Learning_Sample { get => learning_Sample; set => learning_Sample = value; }
    }
}
