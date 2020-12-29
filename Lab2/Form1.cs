using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;


namespace Lab2
{
    public partial class Form1 : Form
    {
        List<int> bounds = new List<int> { };
        List<List<Shift>> shifts = new List<List<Shift>> { };
        List<List<ShiftProbability>> probabilities = new List<List<ShiftProbability>> { };
        List<List<double>> Tables = new List<List<double>> { };
        int levels = 20, step = 2, t1Param = 160;
        Bitmap image = new Bitmap("test5.png", true);
        Graphics g;
        Font font = new Font("Arial", 16);
        Pen pen;
        SolidBrush brush = new SolidBrush(Color.Black);
        List<int> finalPoints = new List<int> { };
        public Form1()
        {
            g = this.CreateGraphics();
            g.ResetClip();
            pen = new Pen(brush);
            InitializeComponent();
        }
        private void Parse_Data_Click(object sender, EventArgs e)
        {
            bounds.Clear();
            shifts.Clear();
            probabilities.Clear();
            finalPoints.Clear();
            bool isIncreasing = true;
            int prevPosition = 0;
            Color backColor = image.GetPixel(1, 1);
            bounds.Add(0);
            AddShiftList();
            for (int i = 0; i < image.Width; i += step)         // filling shifts list
            {
                for (int j = 0; j < image.Height; j++)
                {
                    if (image.GetPixel(i, j) != backColor)
                    {
                        if (i == 0)
                        {
                            prevPosition = j;
                            break;
                        }
                        int prevLevel = GetLevel(prevPosition), curLevel = GetLevel(j);

                        if (isIncreasing && prevLevel > curLevel || !isIncreasing && prevLevel < curLevel)
                        {
                            isIncreasing = !isIncreasing;
                            bounds.Add(i);
                            AddShiftList();
                        }
                        AddShift(prevLevel, curLevel);
                        prevPosition = j;
                        break;
                    }
                }
            }
            bounds.Add(image.Width - 1 - (image.Width - 1) % step);
            ProbabilitiesMeasurement();
            WriteLog();
        }
        private void ProbabilitiesMeasurement()
        {
            var lst = shifts.ToList();
            for (int i = 0; i < lst.Count; i++)
                lst[i] = shifts[i].ToList();
            int partCounter = 0, from = 0, to = 0, shiftCount = 0;
            bool restart = true;
            AddProbabilityList();
            while (lst.Count > 0)
            {
                if (lst.First().Count == 0)
                {
                    if (!probabilities.Last().Exists(x => x.From.Equals(from) && x.To.Equals(to)))
                    {
                        AddProbability(from, to, (double)shiftCount / shifts[shifts.Count - lst.Count].FindAll(x => x.From.Equals(from)).Count);
                    }
                    lst.RemoveAt(0);
                    AddProbabilityList();
                    partCounter++;
                    restart = true;
                    continue;
                }
                if (restart)
                {
                    shiftCount = 1;
                    from = lst.First()[0].From;
                    to = lst.First()[0].To;
                    lst.First().RemoveAt(0);
                    restart = false;
                }
                else
                {
                    var shift = lst.First().Find(x => x.From.Equals(from) && x.To.Equals(to));
                    if (shift != null)
                    {
                        shiftCount++;
                        lst.First().Remove(shift);
                    }
                    else
                    {
                        restart = true;
                        AddProbability(from, to, (double)shiftCount / shifts[shifts.Count - lst.Count].FindAll(x => x.From.Equals(from)).Count);
                    }
                }
            }
        }
        private void WriteLog()
        {
            using (var sw = new StreamWriter(@"log.txt"))
            {
                for (int i = 0; i < probabilities.Count; i++)
                {
                    if (probabilities[i].Count == 0) continue;
                    sw.WriteLine($"Function {i + 1}");
                    for (int j = 0; j < probabilities[i].Count; j++)
                    {
                        sw.WriteLine($"From {probabilities[i][j].From} To {probabilities[i][j].To}, Probability = {probabilities[i][j].probability}");
                    }
                }
                sw.WriteLine($"Levels: {levels}, Step(t) = {step}");
            }
            //g.DrawString("Writing finished", font, brush, 0, 100);
        }
        private int GetLevel(int position)
        {
            double fullHeight = image.Height;
            double level = (double)position / fullHeight * levels;
            if (Math.Round(level) < 1) return levels;
            return levels - (int)Math.Round(level) + 1;
        }
        private void GraphOne_Click(object sender, EventArgs e)
        {
            Tables.Clear();
            g.Clip = new Region(new Rectangle(0, 0, 1500, 1000));
            g.Clear(Color.White);
            finalPoints.Clear();
            List<double> Pt1 = new List<double> { 1 }, Pt2; // P(t)1..P(t)n, P(t+1)1..P(t+1)n
            for (int i = 1; i < levels; i++)
                Pt1.Add(0);
            Pt2 = Pt1.ToList();
            SetStartValue(Pt1, probabilities[0][0].From - 1);
            if (probabilities.Last().Count == 0) probabilities.Remove(probabilities.Last());
            g = this.CreateGraphics();
            g.Clip = new Region(new Rectangle(0, 0, 1000, 1000));
            g.Clear(Color.White);
            int wMulti = 1, hMulti = 15, startX = 70, startY = 750, stepCounter;
            for (int i = 1; i < levels + 1; i++)
            {
                g.DrawString($"{i}", Font, brush, startX - 20, startY - 2 - hMulti * i);
                g.DrawLine(new Pen(brush), startX, startY - hMulti * i - 4, startX + image.Width + 100, startY - hMulti * i - 4);
            }
            font = new Font("Arial", 10);
            for (int i = 0; i < image.Width / step; i += image.Width / step / 5)
            {
                g.DrawString($"{i * step}", font, brush, startX - 10 + i * step, startY + 7);
            }
            g.DrawRectangle(new Pen(brush.Color), startX, startY, image.Width + 100, 1);
            g.DrawRectangle(new Pen(brush.Color), startX, startY - expectedLevel(Pt1) * hMulti, wMulti, 3 * wMulti);
            finalPoints.Add(expectedLevel(Pt1));
            for (int s = 0; s < probabilities.Count; s++)
            {
                //Pt1 = SetStartValue(Pt1, probabilities[s][0].From - 1);
                stepCounter = 0;
                while (stepCounter < StepsCount(s))
                {
                    for (int i = 0; i < levels; i++)
                    {
                        double res = probabilities[s].FindAll(x => x.From.Equals(i + 1) && !x.To.Equals(i + 1)).Sum(x => x.probability);
                        Pt2[i] = Pt1[i] - Pt1[i] * probabilities[s].FindAll(x => x.From.Equals(i + 1) && !x.To.Equals(i + 1)).Sum(x => x.probability);
                        for (int j = 0; j < levels; j++)
                        {
                            if (i == j) continue;
                            Pt2[i] += Pt1[j] * probabilities[s].FindAll(x => x.From.Equals(j + 1) && x.To.Equals(i + 1)).Sum(x => x.probability);
                        }
                    }
                    Tables.Add(Pt1);
                    Pt1 = Pt2.ToList();
                    g.DrawRectangle(new Pen(brush.Color), startX += step, startY - expectedLevel(Pt1) * hMulti, wMulti, 3 * wMulti);
                    stepCounter++;
                    finalPoints.Add(expectedLevel(Pt1));
                }
            }
            Tables.Add(Pt1);
        }
        public List<double> SetStartValue(List<double> lst, int k)
        {
            for (int i = 0; i < lst.Count; i++)
                lst[i] = 0;
            if (k < lst.Count)
                lst[k] = 1;
            return lst;
        }
        private int expectedLevel(List<double> Pt)
        {
            double value = 0;
            for (int i = 0; i < levels; i++)
            {
                value += Pt[i] * (i + 1);
            }
            return (int)Math.Round(value);
        }

        private void GraphTwo_Click(object sender, EventArgs e)
        {
            g = CreateGraphics();
            g.Clip = new Region(new Rectangle(0, 0, 1500, 1000));
            g.Clear(Color.White);
            Color backColor = image.GetPixel(1, 1);
            int startX = 70, startY = 750;
            g.DrawLine(pen, startX, startY, startX + image.Width, startY);
            for (int i = 0; i < image.Width / step; i += image.Width / step / 5)                // OX numbers
            {
                g.DrawString($"{i * step}", font, brush, startX - 10 + i * step, startY + 7);
            }
            for (int i = 10; i < image.Height + 15; i += 15)                                         // OY numbers
            {
                g.DrawString($"{i}", Font, brush, startX - 30, startY - 5 - i);
                for (int j = 0; j < image.Width; j += 10)
                    g.DrawLine(pen, startX + j, startY - i, startX + j + 5, startY - i);
            }
            for (int i = 0; i < finalPoints.Count - 1; i++)
            {
                g.DrawLine(new Pen(Color.Red), startX + step * i, startY - finalPoints[i] * image.Height / levels,
                    startX + step * (i + 1), startY - finalPoints[i + 1] * image.Height / levels);
            }
            if (comparisonCheck.Checked == false) return;
            Point p1 = new Point { X = startX, Y = 0 }, p2 = new Point();
            for (int i = 0; i < image.Height; i++)
                if (image.GetPixel(0, i) != backColor)
                {
                    p1.Y = startY - (image.Height - i);
                    break;
                }

            for (int i = 1; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    if (image.GetPixel(i, j) != backColor)
                    {
                        p2.X = startX + i; p2.Y = startY - (image.Height - j);
                        break;
                    }
                }
                g.DrawLine(new Pen(Color.Green), p1, p2);
                p1 = p2;
            }

        }

        private void GraphThree_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            int t = image.Width, curLevel = 0, curT = 0, t1 = t1Param / step, Xmax = levels - 1, startX = 70, startY = 750,
                Hmulti = image.Height / levels;
            for (int i = 0; i < shifts.Count; i++)
            {
                for (int j = 0; j < shifts[i].Count; j++)
                {
                    if (curT > t1) break;
                    curLevel = shifts[i][j].From;
                    curT++;
                }
            }
            double a = 1, yx = curLevel - a;
            int interval = 40;
            g.DrawLine(pen, startX, startY, startX, startY - image.Height);    // OY
            g.DrawLine(pen, startX, startY, startX + t, startY);                        // OX
            g.DrawString("0", font, brush, startX - 10, startY + 4);
            g.DrawString("t1", font, brush, startX + t1 * step - 4, startY + 4);
            g.DrawString($"{t1 * 2}", font, brush, startX + t1 * step - 7, startY + 19);
            g.DrawString("t1+1", font, brush, startX + (t1 + interval) * step - 14, startY + 4);
            g.DrawString("t", font, brush, startX + t, startY + 4);
            g.DrawString($"Xmax\u203E", font, brush, startX - 45, (int)(startY - Xmax * Hmulti - 10));
            for (int i = 0; i <= t; i += 10)
                g.DrawLine(pen, startX + i, startY - Xmax * Hmulti, startX + i + 5, startY - Xmax * Hmulti);                // high zone of 'Xmax'
            g.DrawLine(pen, startX + t1 * step, startY, startX + t1 * step, startY - image.Height - Hmulti);   // t1
            g.DrawLine(pen, startX + (t1 + interval) * step, startY, startX + (t1 + interval) * step, startY - image.Height - Hmulti);   // t1 + 1
            g.DrawString("1", font, brush, startX - 15, (int)(startY - a * Hmulti - 1));                        // 1st level
            for (int i = Hmulti; i <= (levels - 1) * Hmulti; i += Hmulti)                                       // levels grid
            {
                for (int j = 0; j < image.Width; j += 10)
                    g.DrawLine(pen, startX + j, startY - i, startX + j + 5, startY - i);
            }
            DrawDistribution(t1, startX, startY, interval);
        }
        private void DrawDistribution(int t1, int startX, int startY, int interval)
        {
            Point leftPoint = new Point(), rightPoint = new Point();
            double Hmulti = image.Height / levels, valueMulti = interval * step, val, sigma;
            var probList = Tables[t1 - 1].ToList();
            startX += t1 * step;
            for (int p = 0; p < levels; p++)
            {
                val = probList[p];
                if (val == 0) continue;
                sigma = 1 / (val * Math.Sqrt(2 * Math.PI));
                leftPoint = new Point { X = startX + (int)Math.Round(val * valueMulti), Y = (int)(startY - (p + 1) * Hmulti + 0.5 * Hmulti) };
                rightPoint = new Point(leftPoint.X, leftPoint.Y);
                for (double i = 1; i <= Hmulti / 2; i++)
                {
                    if (val * valueMulti < 1) break;
                    val = (1 / (sigma * Math.Sqrt(2 * Math.PI))) * Math.Pow(Math.E, -i * i / (2 * sigma * sigma));
                    g.DrawLine(pen, leftPoint, new Point { X = startX + (int)Math.Round(val * valueMulti), Y = (int)(startY - (p + 1) * Hmulti + 0.5 * Hmulti - 1 * i) });
                    g.DrawLine(pen, rightPoint, new Point { X = startX + (int)Math.Round(val * valueMulti), Y = (int)(startY - (p + 1) * Hmulti + 0.5 * Hmulti + 1 * i) });
                    leftPoint.X = startX + (int)Math.Round(val * valueMulti);
                    leftPoint.Y = (int)(startY - (p + 1) * Hmulti + 0.5 * Hmulti - 1 * i);
                    rightPoint.X = startX + (int)Math.Round(val * valueMulti);
                    rightPoint.Y = (int)(startY - (p + 1) * Hmulti + 0.5 * Hmulti + 1 * i);
                }
            }
            for (int p = 0; p < levels - 1; p++)
                g.DrawLine(new Pen(Color.Green, 1), startX + (int)Math.Round(probList[p] * valueMulti), (int)(startY - (p + 1) * Hmulti + 0.5 * Hmulti),
                    startX + (int)Math.Round(probList[p + 1] * valueMulti), (int)(startY - (p + 2) * Hmulti + 0.5 * Hmulti));
        }
        private int StepsCount(int s)
        {
            return (bounds[s + 1] - bounds[s]) / step;
        }
        private void AddShiftList()
        {
            shifts.Add(new List<Shift> { });
        }
        private void AddShift(int from, int to)
        {
            shifts.Last().Add(new Shift { From = from, To = to });
        }
        private void AddProbabilityList()
        {
            probabilities.Add(new List<ShiftProbability> { });
        }
        private void AddProbability(int from, int to, double p)
        {
            probabilities.Last().Add(new ShiftProbability { From = from, To = to, probability = p });
        }
    }
    public class Shift
    {
        public int From { get; set; }
        public int To { get; set; }
    }
    public class ShiftProbability : Shift
    {
        public double probability { get; set; }
    }
}
