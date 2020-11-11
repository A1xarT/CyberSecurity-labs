using System.Collections.Generic;
using System.IO;

namespace Lab1
{
    class FileFilter
    {
        public static List<string> getRows(int start, int end, string dest)
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
    }
}
