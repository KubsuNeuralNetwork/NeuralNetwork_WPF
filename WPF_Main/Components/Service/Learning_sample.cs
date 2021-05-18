using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Main.Components.Service
{
    /*
        Класс отвечает за формирование обучающей выборки из txt файла.

     */
    class Learning_sample
    {
        private Dictionary<string, LinkedList<float>> _learning_sampleMap;
        private LinkedList<VectorsNames> isTarget; // Является ли вектор целевым
        private string[] _columns_names;
        private string _learning_sample_str;
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
                _columns_names[name_index] = column_name.Replace("\r",string.Empty);
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
        public Dictionary<string, LinkedList<float>> Learning_sampleMap { get => _learning_sampleMap;}
        internal LinkedList<VectorsNames> IsTarget { get => isTarget; set => isTarget = value; }

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
            _learning_sampleMap.TryGetValue(key,out list);
            float[] arr = list.ToArray<float>();
            return arr;
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
        public void changeTargetItem(string name, int target)
        {
            isTarget.Find(new VectorsNames(name)).Value.IsTarget = target;
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
        #endregion
    }

    class VectorsNames
    {
        private string name;
        private int isTarget;

        public VectorsNames(string name)
        {
            this.name = name;
            this.isTarget = 0;
        }

        public new string ToString => (name + " " + isTarget);

        public int IsTarget { get => isTarget; set => isTarget = value; }
        public string Name { get => name; set => name = value; }

        public override bool Equals(object obj)
        {
            return obj is VectorsNames names &&
                   name == names.name;
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
