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


        private void LS_window_button_InputToFinal_Click(object sender, RoutedEventArgs e)
        {
            LS_window_ListBox_final.Items.Add(LS_window_ListBox_input.SelectedItem);
            LS_window_ListBox_input.Items.Remove(LS_window_ListBox_input.SelectedItem);
        }
        private void LS_window_button_FinalToInput_Click(object sender, RoutedEventArgs e)
        {
            LS_window_ListBox_input.Items.Add(LS_window_ListBox_final.SelectedItem);
            LS_window_ListBox_final.Items.Remove(LS_window_ListBox_final.SelectedItem);
        }
        private void LS_window_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void LerningSet_window_Activated(object sender, EventArgs e)
        {
            foreach (string str in learning_Sample.getDictionaryKeys())
            {
                LS_window_ListBox_input.Items.Add(str);
            }

        }
    }
}
