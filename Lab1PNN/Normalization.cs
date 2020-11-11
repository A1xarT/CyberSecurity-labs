using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1
{
    class Normalization
    {
        public Dictionary<string, int> protocols = new Dictionary<string, int> { };
        public Dictionary<string, int> services = new Dictionary<string, int> { };
        public Dictionary<string, int> flags = new Dictionary<string, int> { };
        public Dictionary<string, int> error_types = new Dictionary<string, int> { };
        public Dictionary<int, string> errors = new Dictionary<int, string> { };
        public Normalization(List<string> lst)
        {
            Data = new List<string> { };
            foreach (string str in lst)
                Data.Add(str);
        }
        public void Normalize()
        {
            List<List<string>> subLists = new List<List<string>> { };
            foreach (string sub in Data)
            {
                subLists.Add(sub.Split(',').ToList<string>());
            }
            for (int i = 0; i < subLists.Count; i++)
            {
                if (!protocols.ContainsKey(subLists[i][1]))
                    protocols.Add(subLists[i][1], protocols.Count + 1);
                if (!services.ContainsKey(subLists[i][2]))
                    services.Add(subLists[i][2], services.Count + 1);
                if (!flags.ContainsKey(subLists[i][3]))
                    flags.Add(subLists[i][3], flags.Count + 1);
                if (!error_types.ContainsKey(subLists[i][41]))
                {
                    error_types.Add(subLists[i][41], error_types.Count + 1);
                    errors.Add(errors.Count, subLists[i][41]);
                }
            }
            for (int i = 0; i < subLists.Count; i++)
            {
                subLists[i][1] = protocols[subLists[i][1]].ToString();
                subLists[i][2] = services[subLists[i][2]].ToString();
                subLists[i][3] = flags[subLists[i][3]].ToString();
                subLists[i][41] = error_types[subLists[i][41]].ToString();
            }
            Data.Clear();
            foreach (List<string> lst in subLists)
            {
                string name = "";
                foreach (string str in lst)
                    name += str + ',';
                Data.Add(name.Substring(0, name.Length - 1));
            }
            //this.ConsoleOut();
            List<Tuple<double, double>> MaxMinValues = new List<Tuple<double, double>> { };
            for (int i = 0; i < 42; i++)
            {
                List<double> numbers = new List<double> { };
                foreach (List<string> lst in subLists)
                    numbers.Add(Double.Parse((lst[i].Replace('.', ','))));
                MaxMinValues.Add(new Tuple<double, double>(numbers.Min(), numbers.Max()));
            }
            for (int i = 0; i < 42; i++)
                foreach (List<string> lst in subLists)
                    lst[i] = getNormalized(Double.Parse(lst[i].Replace('.', ',')), MaxMinValues[i].Item1, MaxMinValues[i].Item2).ToString();
            Data.Clear();
            foreach (List<string> lst in subLists)
            {
                string name = "";
                for (int i = 0; i < 42; i++)
                    name += lst[i].Replace(',', '.') + ',';
                Data.Add(name.Substring(0, name.Length - 1));
            }

        }
        double getNormalized(double item, double min, double max)
        {
            if (max - min == 0) return 0;
            return (max - item) / (max - min);
        }
        public void ConsoleOut()
        {
            foreach (string str in Data)
                Console.WriteLine(str);
        }
        public List<string> Data;
    }
}
