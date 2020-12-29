using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


namespace Lab1
{
    class ExtremeValues
    {
        public Dictionary<string, int> Protocols = new Dictionary<string, int> { };
        public Dictionary<string, int> Services = new Dictionary<string, int> { };
        public Dictionary<string, int> Flags = new Dictionary<string, int> { };
        public void DefineValues(List<string> data)
        {
            List<List<string>> subLists = new List<List<string>> { };
            foreach (string sub in data)
            {
                subLists.Add(sub.Split(',').ToList());
            }
            for (int i = 0; i < subLists.Count; i++)
            {
                if (!Protocols.ContainsKey(subLists[i][1]))
                    Protocols.Add(subLists[i][1], Protocols.Count + 1);
                if (!Services.ContainsKey(subLists[i][2]))
                    Services.Add(subLists[i][2], Services.Count + 1);
                if (!Flags.ContainsKey(subLists[i][3]))
                    Flags.Add(subLists[i][3], Flags.Count + 1);
            }
            for (int i = 0; i < subLists.Count; i++)
            {
                subLists[i][1] = Protocols[subLists[i][1]].ToString();
                subLists[i][2] = Services[subLists[i][2]].ToString();
                subLists[i][3] = Flags[subLists[i][3]].ToString();
            }
            data.Clear();
            foreach (List<string> lst in subLists)
            {
                string name = "";
                foreach (string str in lst)
                    name += str + ',';
                data.Add(name.Substring(0, name.Length - 1));
            }
            List<MinMax> minMaxValues = new List<MinMax> { };
            for (int i = 0; i < 41; i++)
            {
                List<double> numbers = new List<double> { };
                foreach (List<string> lst in subLists)
                    numbers.Add(Double.Parse((lst[i].Replace('.', ','))));
                minMaxValues.Add(new MinMax(numbers.Min(), numbers.Max()));
            }
            using (var sw = new StreamWriter("MinMax"))
            {
                for(int i = 0; i < minMaxValues.Count; i++)
                {
                    sw.WriteLine(minMaxValues[i].ToString());
                }
            }
            using (var sw = new StreamWriter("Collections"))
            {
                foreach(KeyValuePair<string, int> dct in Protocols)
                {
                    sw.WriteLine($"{dct.Key}, {dct.Value}");
                }
                foreach (KeyValuePair<string, int> dct in Services)
                {
                    sw.WriteLine($"{dct.Key}, {dct.Value}");
                }
                foreach (KeyValuePair<string, int> dct in Flags)
                {
                    sw.WriteLine($"{dct.Key}, {dct.Value}");
                }
            }
        }
    }
}
