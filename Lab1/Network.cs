using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Lab1
{
    [Serializable]
    class PatternNeuron
    {
        public PatternNeuron(double[] inputLayer, string outputWord)
        {
            int inputNumber = inputLayer.Length;
            weights = new double[inputNumber];
            for (int i = 0; i < inputNumber; i++)
                weights[i] = inputLayer[i];
            output = outputWord;
        }
        public double ActivateNeruon(double[] input)
        {
            double activatedNeuron = 0, Sigma = 0.5;
            for (int i = 0; i < input.Length; i++)
                activatedNeuron += Math.Exp(-Math.Pow((weights[i] - input[i]), 2) / Math.Pow(Sigma, 2));
            activatedValue = activatedNeuron;
            return activatedNeuron;
        }
        public double[] weights;
        public double activatedValue;
        public string output;
    }
    [Serializable]
    class Network
    {
        private readonly List<PatternNeuron> Patterns;
        public Network()
        {
            Patterns = new List<PatternNeuron> { };
            Console.WriteLine("PNN Network has been created");
        }
        public void Train(List<List<double>> records, List<string> outputs)
        {
            int recordsNumber = records.Count;
            for (int i = 0; i < recordsNumber; i++)
            {
                PatternNeuron pNeuron = new PatternNeuron(records[i].ToArray(), outputs[i]);
                Patterns.Add(pNeuron);
            }
            Console.WriteLine("Network has been trained");
        }
        public Tuple<string, long> GetAnswer(List<double> record)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < Patterns.Count; i++)
            {
                Patterns[i].ActivateNeruon(record.ToArray());
                if (Patterns[i].activatedValue == 41)
                {
                    stopwatch.Stop();
                    return new Tuple<string, long>(Patterns[i].output, stopwatch.ElapsedMilliseconds);
                }
            }
            var fittingNeuron = Patterns.OrderBy(p => p.activatedValue).Last();
            stopwatch.Stop();
            return new Tuple<string, long>(fittingNeuron.output, stopwatch.ElapsedMilliseconds);
        }
    }
}
