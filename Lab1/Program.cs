using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            //ExtremeValues values = new ExtremeValues { };
            //values.DefineValues(FileFilter.GetRows("FullData.txt"));

            // Creating and training
            int start = 0, end = 10;
            int rowsCount;
            List<string> stringedData;
            var recordsList = new List<List<double>> { };
            var realValues = new List<string> { };
            string trainData = "TrainData.txt";
            Normalization Nrm;
            Nrm = new Normalization(FileFilter.GetRows(start, end, trainData));
            Nrm.Normalize();
            if (Nrm.Data != null)
            {
                rowsCount = Nrm.Data.Count;
                stringedData = new List<string> { };
                foreach (string str in Nrm.Data)
                {
                    string[] strArr = str.Split(',');
                    foreach (string str2 in strArr)
                        stringedData.Add(str2);
                }
                recordsList = new List<List<double>> { };
                realValues = new List<string> { };
                for (int i = 0; i < rowsCount; i++)
                {
                    var subLst = new List<double> { };
                    for (int j = 0; j < 41; j++)
                        subLst.Add(Double.Parse(stringedData[i * 42 + j].Replace('.', ',')));
                    recordsList.Add(subLst);
                    realValues.Add(stringedData[i * 42 + 41].ToString().Substring(0, stringedData[i * 42 + 41].Length - 1));
                }
            }
            BinaryFormatter formatter = new BinaryFormatter();
            Network network;
            if (File.Exists("network.dat"))
            {
                using (FileStream fs = new FileStream("network.dat", FileMode.OpenOrCreate))
                {
                    network = (Network)formatter.Deserialize(fs);
                }
            }
            else
            {
                network = new Network();
            }
            network.Train(recordsList, realValues);
            // Getting answers
            start = 0; end = 10;
            string TestData = "TestData.txt";
            Nrm = new Normalization(FileFilter.GetRows(start, end, TestData));
            Nrm.Normalize();
            rowsCount = Nrm.Data.Count;
            stringedData = new List<string> { };
            foreach (string str in Nrm.Data)
            {
                string[] strArr = str.Split(',');
                foreach (string str2 in strArr)
                    stringedData.Add(str2);
            }
            recordsList = new List<List<double>> { };
            realValues = new List<string> { };
            for (int i = 0; i < rowsCount; i++)
            {
                var subLst = new List<double> { };
                for (int j = 0; j < 41; j++)
                    subLst.Add(Double.Parse(stringedData[i * 42 + j].Replace('.', ',')));
                recordsList.Add(subLst);
                realValues.Add(stringedData[i * 42 + 41].ToString().Substring(0, stringedData[i * 42 + 41].Length - 1));
            }
            List<long> timeList = new List<long> { };
            int rightAnswers = end - start;
            for (int i = 0; i < recordsList.Count; i++)
            {
                var lst = recordsList[i];
                var answer = network.GetAnswer(lst);
                if (answer.Item1 != realValues[i]) rightAnswers--;
                timeList.Add(answer.Item2);
                Console.WriteLine($"{answer.Item1}");
            }
            Console.WriteLine($"Accurancy = {(double)rightAnswers / (end - start) * 100}%");
            Console.WriteLine($"Average time = {timeList.Average()}");

            using (FileStream fs = new FileStream("network.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, network);
            }
            Console.ReadKey();
        }
    }
}
