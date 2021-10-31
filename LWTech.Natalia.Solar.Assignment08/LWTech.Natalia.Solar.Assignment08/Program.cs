using System;
using System.Collections.Generic;
using System.Net;
using System.IO;

namespace LWTech.Natalia.Solar.Assignment08
{
    class Program
    {
        static void Main(string[] args)
        {

            var airplanes = new List<string>();

            WebClient client = new WebClient();

            try
            {
                Stream stream = client.OpenRead("https://stockcharts.com/dev/chipa/airplanes.json");
                using (StreamReader reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] token = line.Split('{');
                        foreach (string s in token)
                        {
                            if (s.Contains("\"Type\":"))
                            {
                                int start = s.IndexOf("\"Type\":", StringComparison.Ordinal) + 8;
                                string typeString = s.Substring(start, 4);
                                string neededType = typeString.Substring(0, 2);

                                if (neededType.Contains("B7"))
                                {
                                    typeString = typeString.Substring(0, 3) + "7";
                                    airplanes.Add(typeString);
                                }

                                if (neededType.Contains("A3"))
                                {
                                    typeString = typeString.Substring(0, 3) + "0";
                                    airplanes.Add(typeString);
                                }
                            }
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("A network error occurred.  " + ex.Message);
                Console.WriteLine("Unable to continue.");
                return;
            }

            Console.WriteLine("Currently Flying Boeing/Airbus Airplanes:\n========================================= ");
            Histogram airplaneTypeHistogram = new Histogram(airplanes, width: 100, maxLabelWidth: 5);
            airplaneTypeHistogram.Sort((x, y) => y.Value.CompareTo(x.Value));
            Console.WriteLine(airplaneTypeHistogram);
            Console.ReadLine();
        }
    }


    class Histogram
    {
        private int width;
        private int maxBarWidth;
        private int maxLabelWidth;
        private int minValue;
        private List<KeyValuePair<string, int>> bars;

        public Histogram(List<string> data, int width = 80, int maxLabelWidth = 10, int minValue = 0)
        {
            this.width = width;
            this.maxLabelWidth = maxLabelWidth;
            this.minValue = minValue;
            this.maxBarWidth = width - maxLabelWidth - 2;   // -2 for the space and pipe separator

            // Use a Dictionary to count up the frequency of each string in the list
            var barCounts = new Dictionary<string, int>();
            foreach (string item in data)
            {
                if (barCounts.ContainsKey(item))
                    barCounts[item]++;
                else
                    barCounts.Add(item, 1);
            }

            // Store strings and frequencies in a (sortable) list of key/value pairs
            this.bars = new List<KeyValuePair<string, int>>(barCounts);
        }

        public void Sort(Comparison<KeyValuePair<string, int>> f)
        {
            bars.Sort(f);
        }

        public override string ToString()
        {
            string s = "";
            string blankLabel = "".PadRight(maxLabelWidth);

            int maxValue = 0;
            foreach (KeyValuePair<string, int> bar in bars)
            {
                if (bar.Value > maxValue)
                    maxValue = bar.Value;
            }

            foreach (KeyValuePair<string, int> bar in bars)
            {
                string key = bar.Key;
                int value = bar.Value;

                if (value >= minValue)
                {
                    string label;
                    if (key.Length < maxLabelWidth)
                        label = key.PadLeft(maxLabelWidth);
                    else
                        label = key.Substring(0, maxLabelWidth);

                    int barSize = (int)(((double)value / maxValue) * maxBarWidth);
                    string barStars = "".PadRight(barSize, '*');

                    s += label + " |" + barStars + " " + value + "\n";
                }
            }

            string axis = blankLabel + " +".PadRight(maxBarWidth + 2, '-') + "\n";    //TODO: Why is +2 is needed?
            s += axis;

            return s;
        }
    }

}
