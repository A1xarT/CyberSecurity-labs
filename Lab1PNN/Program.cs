using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creating and training
            int start = 0, end = 650;
            string trainData = @"D:\Docs\Neuros\Lab1PNN\KDD99 Data.txt";
            Normalization Nrm = new Normalization(FileFilter.getRows(start, end, trainData));
            Nrm.Normalize();
            int rowsCount = Nrm.Data.Count;
            List<string> stringedData = new List<string> { };
            foreach (string str in Nrm.Data)
            {
                string[] strArr = str.Split(',');
                foreach (string str2 in strArr)
                    stringedData.Add(str2);
            }
            var recordsList = new List<List<double>> { };
            var realValues = new List<string> { };
            for (int i = 0; i < rowsCount; i++)
            {
                var subLst = new List<double> { };
                for (int j = 0; j < 41; j++)
                    subLst.Add(Double.Parse(stringedData[i * 42 + j].Replace('.', ',')));
                recordsList.Add(subLst);
                realValues.Add(Nrm.errors[Nrm.errors.Count - 1 - (int)Math.Round
                    (Double.Parse(stringedData[i * 42 + 41].Replace('.', ',')) * (Nrm.errors.Count - 1))].Trim('.'));
            }
            Network network = new Network();
            network.Train(recordsList, realValues);
            // Getting answers
            // start = 0; end = 638;
            string TestData = @"D:\Docs\Neuros\Lab1PNN\TestData.txt";
            Nrm = new Normalization(FileFilter.getRows(start, end, TestData));
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
                realValues.Add(Nrm.errors[Nrm.errors.Count - 1 - (int)Math.Round(Double.Parse(stringedData[i * 42 + 41].Replace('.', ',')) * (Nrm.errors.Count - 1))].Trim('.'));
            }
            List<long> timeList = new List<long> { };
            int rightAnswers = end - start;
            for(int i = 0; i < recordsList.Count; i++)
            {
                var lst = recordsList[i];
                var answer = network.GetAnswer(lst);
                if (answer.Item1 != realValues[i]) rightAnswers--;
                    timeList.Add(answer.Item2);
                Console.WriteLine(answer.Item1);
            }
            Console.WriteLine($"Accurancy = {(double)rightAnswers/(end-start)* 100}%");
            Console.WriteLine($"Average time = {timeList.Average()}");
            Console.ReadKey();
        }
    }
}
