using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPF_Main.Components.API;

namespace WPF_Main.Components.Service
{
    /*
        Класс отвечает за формирование обучающей выборки из txt файла.
     */
    [Serializable]
    class Learning_sample
    {
        private Dictionary<string, LinkedList<float>> _learning_sampleMap;
        private LinkedList<VectorsNames> isTarget; // Является ли вектор целевым
        private string[] _columns_names;
        private string _learning_sample_str;
        private float[,] norm;
        private int j_size;
        private int i_size;
        public Learning_sample(string learning_sample_str)
        {
            isTarget = new LinkedList<VectorsNames>();
            _learning_sampleMap = new Dictionary<string, LinkedList<float>>();
            _learning_sample_str = learning_sample_str.Replace('.', ',');
            string[] learnin_sample_byRow = _learning_sample_str.Split('\n');
            _columns_names = new string[learnin_sample_byRow[0].Split('\t').Length];
            Console.WriteLine(learning_sample_str);
            int name_index = 0;
            foreach (string column_name in learnin_sample_byRow[0].Split('\t'))
            {
                _columns_names[name_index] = column_name.Replace("\r", string.Empty);
                name_index++;
            }
            int arr_countOfRow = learnin_sample_byRow.Length - 1;
            int arr_countOfColums = learnin_sample_byRow[0].Split().Length - 1;
            float[,] arr = new float[arr_countOfRow, arr_countOfColums];
            for (int i = 1; i < learnin_sample_byRow.Length; i++)
            {
                string str = learnin_sample_byRow[i];
                LinkedList<string> temp = new LinkedList<string>(str.Trim(' ').Split('\t', ' ', '\r'));
                int j = 0;
                foreach (string num in temp)
                {
                    if (num != " " && num != "")
                    {
                        arr[i - 1, j] = float.Parse(num);
                        Console.Write(num);
                        j++;
                    }
                }
            }
            Console.WriteLine();
            for (int i = 0; i < arr_countOfRow; i++)
            {
                for (int j = 0; j < arr_countOfColums; j++)
                {
                    Console.Write(arr[i, j]);
                    Console.Write("\t\t");
                }
                Console.Write('\n');
            }

            for (int i = 0; i < arr_countOfColums; i++)
            {
                LinkedList<float> list = new LinkedList<float>();
                for (int j = 0; j < arr_countOfRow; j++)
                {
                    list.AddLast(arr[j, i]);
                }
                _learning_sampleMap.Add(_columns_names[i], list);
                isTarget.AddLast(new VectorsNames(_columns_names[i]));
            }
            isTarget.Last.Value.IsTarget = 1;
            _learning_sample_str = convert_mapToString();
        }


        public Learning_sample()
        {
        }

        public string Learning_sample_str { get => _learning_sample_str; set => _learning_sample_str = value; }
        public Dictionary<string, LinkedList<float>> Learning_sampleMap { get => _learning_sampleMap; }
        internal LinkedList<VectorsNames> IsTarget { get => isTarget; set => isTarget = value; }
        public float[,] Norm { get => norm; set => norm = value; }
        public int I_size { get => j_size; set => j_size = value; }
        public int J_size { get => i_size; set => i_size = value; }

        private string convert_mapToString()
        {
            string str = "";
            foreach (KeyValuePair<string, LinkedList<float>> pair in _learning_sampleMap)
            {
                str += pair.Key + " \n";
                foreach (float num in pair.Value)
                {
                    str += num.ToString() + '\t';
                }
                str += "\n";
            }
            return str;
        }

        public string[] getDictionaryKeys()
        {
            return _learning_sampleMap.Keys.ToArray();
        }



        public float[] getArrayByKey(string key)
        {
            LinkedList<float> list;
            _learning_sampleMap.TryGetValue(key, out list);
            float[] arr = list.ToArray<float>();
            return arr;
        }

        public LinkedList<float> getListByKey(string key)
        {
            LinkedList<float> list;
            _learning_sampleMap.TryGetValue(key, out list);
            return list;
        }

        // Нормализовать все входные данные
        public void Normalize()
        {
            string[] str = _learning_sampleMap.Keys.ToArray();
            float max, min;
            LinkedList<float> list;
            j_size = str.Length;
            _learning_sampleMap.TryGetValue(str[0], out list);
            i_size = list.Count;
            norm = new float[i_size, j_size];           
            for (int j = 0; j < j_size; j++)
            {
                _learning_sampleMap.TryGetValue(str[j], out list);
                max = list.First.Value;
                min = list.First.Value;
                foreach (float num in list)
                {
                    if (num < min) min = num;
                    if (num > max) max = num;
                }
                if (min != max)
                    foreach (float num in list)
                    {
                        list.Find(num).Value = (num - min) / (max - min);
                    }
                else if (min != 0)
                    foreach (float num in list)
                    {
                        list.Find(num).Value = num / min;
                    }
                _learning_sampleMap.Remove(str[j]);
                _learning_sampleMap.Add(str[j], list);
                add_min_max(min, max, str[j]);
            }
;
            Console.WriteLine(convert_mapToString());
            createNormMatrix();
            for (int i = 0; i < i_size; i++)
            {
                for (int j = 0; j < j_size; j++)
                {
                    Console.Write(norm[i, j] + " ");
                }
                Console.Write("\n");
            }
        }

        private void createNormMatrix()
        {
            int currColumn = 0;
            foreach (KeyValuePair<string, LinkedList<float>> pair in _learning_sampleMap)
            {
                if (isTarget.Find(new VectorsNames(pair.Key)).Value.IsTarget == 0)
                {
                    int j = 0;
                    foreach (float num in pair.Value)
                    {
                        norm[j, currColumn] = num;
                        j++;
                    }
                    currColumn++;
                }
            }
            foreach (KeyValuePair<string, LinkedList<float>> pair in _learning_sampleMap)
            {
                if (isTarget.Find(new VectorsNames(pair.Key)).Value.IsTarget == 1)
                {
                    int j = 0;
                    foreach (float num in pair.Value)
                    {
                        norm[j, currColumn] = num;
                        j++;
                    }
                    currColumn++;
                }
            }


        }

        public LinkedList<float> reverse_Normalize(LinkedList<float> list, float min, float max)
        {
            LinkedList<float> reverse_normalize_vector_list = new LinkedList<float>();
            float[] vect = list.ToArray();
            for (int j = 0; j < vect.Length; j++)
            {
                reverse_normalize_vector_list.AddLast(vect[j] * (max - min) + min);
            }
            return reverse_normalize_vector_list;
        }


        #region Работа с Target
        public string getTargetString()
        {
            string result = "";
            foreach (VectorsNames target in isTarget)
            {
                result += target.ToString + "\n";
            }
            return result;
        }

        public int isTargetItem(string name)
        {
            return isTarget.Find(new VectorsNames(name)).Value.IsTarget;
        }
        public bool changeTargetItem(string name, int target)
        {
            int prevTarget = isTarget.Find(new VectorsNames(name)).Value.IsTarget;
            if (target == prevTarget)
                return false;
            switch (target)
            {
                case 1:
                    if (count_of_inputVectors() == 1 && prevTarget == 0)
                        return false;
                    break;
                case 0:
                    if (count_of_TargetVectors() == 1 && prevTarget == 1)
                        return false;
                    break;
                case -1:
                    if ((count_of_TargetVectors() - 1 < 1 && prevTarget == 1) || (count_of_inputVectors() - 1 < 1 && prevTarget == 0))
                        return false;
                    break;
            }
            isTarget.Find(new VectorsNames(name)).Value.IsTarget = target;
            VectorsNames vector = isTarget.Find(new VectorsNames(name)).Value;
            VectorsNames firstTarget = null;
            if (target == 1)
            {
                isTarget.Remove(new VectorsNames(name));
                LinkedList<float> list;
                _learning_sampleMap.TryGetValue(name, out list);                
                isTarget.AddLast(vector);
            }
            if (target == 0)
            {
                foreach (VectorsNames item in isTarget)
                {
                    if (item.IsTarget == 1)
                    {
                        firstTarget = item;
                        break;
                    }
                }
                isTarget.Remove(new VectorsNames(name));
                isTarget.AddBefore(isTarget.Find(firstTarget),vector);
            }

            return true;            
        }

        public int count_of_inputVectors()
        {
            int counter = 0;
            foreach (VectorsNames item in isTarget)
            {
                if (item.IsTarget == 0)
                    counter++;
            }
            return counter;
        }
        public int count_of_TargetVectors()
        {
            int counter = 0;
            foreach (VectorsNames item in isTarget)
            {
                if (item.IsTarget == 1)
                    counter++;
            }
            return counter;
        }
        public int count_of_NotUsedVectors()
        {
            int counter = 0;
            foreach (VectorsNames item in isTarget)
            {
                if (item.IsTarget == 1)
                    counter++;
            }
            return counter;
        }
        private void add_min_max(float min, float max, string str)
        {
            VectorsNames vect = IsTarget.Find(new VectorsNames(str)).Value;
            vect.Min = min;
            vect.Max = max;

        }
        #endregion
    }

    [Serializable]
    class VectorsNames
    {
        private string name;
        private int isTarget;
        private float min;
        private float max;
        private float curr_value;
        private float norm_value;

        public VectorsNames(string name)
        {
            this.name = name;
            this.isTarget = 0;
            this.curr_value = 0;
            this.norm_value = 0;
        }

        public new string ToString => (name + " " + isTarget);

        public int IsTarget { get => isTarget; set => isTarget = value; }
        public string Name { get => name; set => name = value; }
        public float Min { get => min; set => min = value; }
        public float Max { get => max; set => max = value; }
        public float Curr_value { get => curr_value; set => curr_value = value; }
        public float Norm_value { get => norm_value; set => norm_value = value; }

        public override bool Equals(object obj)
        {
            return obj is VectorsNames names &&
                   name == names.name;
        }

        public ListBoxItem getView()
        {
            ListBoxItem listBoxItem = new ListBoxItem();
            listBoxItem.Height = 33;
            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;

            Label column_name_Label = new Label();
            column_name_Label.Content = name;
            column_name_Label.MinWidth = 60;
            column_name_Label.MaxWidth = 80;            
            stack.Children.Add(column_name_Label);


            TextBox input = new TextBox();
            input.PreviewTextInput += Layer.InputDouble;
            input.TextChanged += TextBox_TextChanged;
            input.Text = Convert.ToString(curr_value);
            input.Width = 80;
            input.Margin = new Thickness(5, 0, 0, 0);
            if (isTarget == 1)
                input.IsReadOnly = true;
            stack.Children.Add(input);

            listBoxItem.Content = stack;

            return listBoxItem;
        }

        public ListBoxItem getView(int tabIndex)
        {
            ListBoxItem listBoxItem = new ListBoxItem();
            listBoxItem.Height = 33;
            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;

            Label column_name_Label = new Label();
            column_name_Label.Content = name;
            column_name_Label.MinWidth = 60;
            column_name_Label.MaxWidth = 80;
            stack.Children.Add(column_name_Label);


            TextBox input = new TextBox();
            input.PreviewTextInput += Layer.InputDouble;
            input.TextChanged += TextBox_TextChanged;
            input.Text = Convert.ToString(curr_value);
            input.Width = 80;
            input.Margin = new Thickness(5, 0, 0, 0);
            input.TabIndex = tabIndex;
            if (isTarget == 1)
                input.IsReadOnly = true;
            stack.Children.Add(input);

            listBoxItem.Content = stack;

            return listBoxItem;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                textBox.Text = textBox.Text.Replace(" ", string.Empty);
                textBox.Text = Layer.limitDouble(textBox.Text, 100000f);
                textBox.Text = Layer.deleteNotUsedNull(textBox.Text);
                this.curr_value = float.Parse(textBox.Text);
            }
            catch (FormatException)
            {
                this.curr_value = 10000f;
            }
            if (isTarget == 0)
                this.norm_value = (curr_value - min) / (max - min);
        }

        public void setCurrent_value(float norm)
        {
            norm_value = norm;
            if (isTarget == 1)
                this.curr_value = norm_value * (max - min) + min;
        }

        public override int GetHashCode()
        {
            int hashCode = -1525980690;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            hashCode = hashCode * -1521134295 + isTarget.GetHashCode();
            return hashCode;
        }
    }
}