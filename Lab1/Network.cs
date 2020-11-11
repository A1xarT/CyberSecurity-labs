using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1
{
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
    
    class Network
    {
        List<PatternNeuron> Patterns;
        public Network()
        {
            Patterns = new List<PatternNeuron> { };
            Console.WriteLine("PNN Network has been created");
        }
        public void Train(List<List<double>> records, List<string> outputs)
        {
            int recordsNumber = records.Count;
            for(int i = 0; i < recordsNumber; i++)
            {
                PatternNeuron pNeuron = new PatternNeuron(records[i].ToArray(), outputs[i]);
                Patterns.Add(pNeuron);
            }
            Console.WriteLine("Network has been trained");
        }
        public Tuple<string, long> GetAnswer(List<double> record)
        {
            long elapsedTime = DateTime.Now.Ticks;
            for(int i = 0; i < Patterns.Count; i++)
            {
                Patterns[i].ActivateNeruon(record.ToArray());
                if(Patterns[i].activatedValue == Patterns[i].weights.Length)
                    return new Tuple<string, long>(Patterns[i].output, DateTime.Now.Ticks - elapsedTime);
            }
            var fittingNeuron = Patterns.OrderBy(p=>p.activatedValue).Last();
            return new Tuple<string, long>(fittingNeuron.output, DateTime.Now.Ticks - elapsedTime);
        }

    }
}
