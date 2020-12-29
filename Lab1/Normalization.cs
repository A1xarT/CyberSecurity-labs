using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Lab1
{
    class Normalization
    {
        public List<string> Data;
        public Dictionary<string, int> Collections = new Dictionary<string, int> { };
        public Normalization(List<string> lst)
        {
            if (lst == null)
                return;
            Data = lst.ToList();
        }
        public void Normalize()
        {
            if (Data == null) return;
            FillDictionaries();
            List<List<string>> subLists = new List<List<string>> { };
            foreach (string sub in Data)
            {
                subLists.Add(sub.Split(',').ToList());
            }
            
            for (int i = 0; i < subLists.Count; i++)
            {
                for(int j = 1; j <= 3; j++)
                    subLists[i][j] = Collections[subLists[i][j]].ToString();
            }
            Data.Clear();
            List<MinMax> minMaxes = new List<MinMax> { };
            using(var sr = new StreamReader("MinMax"))
            {
                while(!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    minMaxes.Add(new MinMax(double.Parse(line.Split(' ')[0]), Double.Parse(line.Split(' ')[1])));
                }
            }
            for (int i = 0; i < 41; i++)
                foreach (List<string> lst in subLists)
                    lst[i] = GetNormalized(Double.Parse(lst[i].Replace('.', ',')), minMaxes[i].Min, minMaxes[i].Max).ToString();
            foreach (List<string> lst in subLists)
            {
                string name = "";
                for (int i = 0; i < 42; i++)
                    name += lst[i].Replace(',', '.') + ',';
                Data.Add(name.Substring(0, name.Length - 1));
            }
        }
        public void FillDictionaries()
        {
            string line;
            using(var sr = new StreamReader("Collections"))
            {
                while(!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    KeyValuePair<string, int> pair = new KeyValuePair<string, int>(line.Split(',')[0], int.Parse(line.Split(',')[1]));
                    Collections.Add(pair.Key, pair.Value);
                }
            }

        }
        double GetNormalized(double item, double min, double max)
        {
            if (max - min == 0) return 0;
            return (max - item) / (max - min);
        }
        public void ConsoleOut()
        {
            foreach (string str in Data)
                Console.WriteLine(str);
        }
    }
}
