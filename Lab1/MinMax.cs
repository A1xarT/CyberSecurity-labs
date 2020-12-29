
namespace Lab1
{
    class MinMax
    {
        public MinMax(double _min, double _max)
        {
            Min = _min; 
            Max = _max;
        }
        public double Min { get; set; }
        public double Max { get; set; }
        public override string ToString()
        {
            return $"{Min} {Max}";
        }
    }
}
