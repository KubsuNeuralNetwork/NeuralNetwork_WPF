﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Main.Components.Models
{
    /*
        Класс отвечает за формирование обучающей выборки из txt файла.

     */
    class Learning_sample
    {
        private Dictionary<string, LinkedList<double>> _learning_sampleMap;
        private string[] _columns_names;
        private string _learning_sample_str;
        public Learning_sample(string learning_sample_str)
        {
            _learning_sampleMap = new Dictionary<string, LinkedList<double>>();
            _learning_sample_str = learning_sample_str.Replace('.', ',');
            string[] learnin_sample_byRow = _learning_sample_str.Split('\n');
            _columns_names = new string[learnin_sample_byRow[0].Split('\t').Length];
            Console.WriteLine(learning_sample_str);
            int name_index = 0;
            foreach (string column_name in learnin_sample_byRow[0].Split('\t'))
            {
                _columns_names[name_index] = column_name;
                name_index++;
            }
            int arr_countOfRow = learnin_sample_byRow.Length - 1;
            int arr_countOfColums = learnin_sample_byRow[0].Split().Length - 1;
            double[,] arr = new double[arr_countOfRow, arr_countOfColums];
            for (int i = 1; i < learnin_sample_byRow.Length; i++)
            {
                string str = learnin_sample_byRow[i];
                string[] temp = str.Trim().Split('\t',' ');

                for (int j = 0; j < temp.Length; j++)
                {
                    temp[j] = temp[j].Trim();
                    if (temp[j] != "")
                    {
                        arr[i - 1, j] = Convert.ToDouble(temp[j]);
                        Console.Write(temp[j]);
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
                LinkedList<double> list = new LinkedList<double>();
                for (int j = 0; j < arr_countOfRow; j++)
                {
                    list.AddLast(arr[j, i]);
                }
                _learning_sampleMap.Add(_columns_names[i], list);
            }
            _learning_sample_str = convert_mapToString();
        }


        public Learning_sample()
        {
        }


        public string Learning_sample_str { get => _learning_sample_str; set => _learning_sample_str = value; }
        public Dictionary<string, LinkedList<double>> Learning_sampleMap { get => _learning_sampleMap;}

        private string convert_mapToString()
        {
            string str = "";
            foreach (KeyValuePair<string, LinkedList<double>> pair in _learning_sampleMap)
            {
                str += pair.Key + " \n";
                foreach (double num in pair.Value)
                {
                    str += num.ToString() + '\t';
                }
                str += "\n";
            }
            return str;
        }

        public double[] getArrayByKey(string column_name)
        {
            LinkedList<double> list;
            _learning_sampleMap.TryGetValue(column_name,out list);
            double[] arr = list.ToArray<double>();
            return arr;
        }
    }
}
