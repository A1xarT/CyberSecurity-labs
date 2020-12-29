using System.Collections.Generic;
using System.IO;

namespace Lab1
{
    class FileFilter
    {
        public static List<string> GetRows(int start, int end, string dest)
        {
            var lst = new List<string> { };
            using (var sr = new StreamReader(dest))
            {
                for (int i = 0; i < start; i++)
                    sr.ReadLine();
                for (int i = 0; i < end - start; i++)
                    lst.Add(sr.ReadLine());
            }
            return lst;
        }
        public static List<string> GetRows(string dest)
        {
            var lst = new List<string> { };
            using (var sr = new StreamReader(dest))
            {
                while(!sr.EndOfStream)
                {
                    lst.Add(sr.ReadLine());
                }
            }
            return lst;
        }
        public static List<string> GetRows(string dest, List<string> attackTypes)
        {
            var lst = new List<string> { };
            using (var sr = new StreamReader(dest))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    foreach(string attackType in attackTypes)
                    {
                        if(line.Contains(attackType))
                        {
                            lst.Add(line);
                            break;
                        }
                    }
                }
            }
            return lst;
        }
        public static List<string> GetRows(string dest, List<string> attackTypes, int n)
        {
            var lst = new List<string> { };
            using (var sr = new StreamReader(dest))
            {
                while (!sr.EndOfStream)
                {
                    if (n <= 0)
                        break;
                    string line = sr.ReadLine();
                    foreach (string attackType in attackTypes)
                    {
                        if (line.Contains(attackType))
                        {
                            lst.Add(line);
                            n--;
                            break;
                        }
                    }
                }
            }
            return lst;
        }
    }
}
