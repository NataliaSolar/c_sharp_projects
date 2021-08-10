using System;
using System.Collections.Generic;

namespace LWTech.Natalia.Solar.Assignment07
{
    abstract class Statistics
    {
        public Dictionary<string, int> Frequencies { get; }

        public Statistics()
        {
            Frequencies = new Dictionary<string, int>();
        }

        public void PopulateDoctionary(string key)
        {
            if (key == null || Frequencies == null) throw new ArgumentNullException("Null is passed to the method.");
            if (Frequencies.ContainsKey(key))
                Frequencies[key]++;
            else
                Frequencies.Add(key, 1);
        }
        public abstract void DisplayDictionary(Dictionary<string, int> dict);
        public abstract Dictionary<string, int> SortDictionary();
    }

    class StatusStatistics : Statistics
    {
        public StatusStatistics() : base() { }

        public override void DisplayDictionary(Dictionary<string, int> dict)
        {
            if (dict == null) throw new ArgumentNullException("Null is passed to the method.");

            foreach (string key in dict.Keys)
                Console.WriteLine(dict[key] + ": " + key);
        }

        public override Dictionary<string, int> SortDictionary()
        {

            List<string> keys = new List<string>();
            foreach (KeyValuePair<string, int> pair in base.Frequencies)
            {
                keys.Add(pair.Key);
            }

            keys.Sort();

            List<int> sortedValues = new List<int>();
            for (int i = 0; i < keys.Count; i++)
            {
                sortedValues.Add(base.Frequencies[keys[i]]);
            }

            Dictionary<string, int> sortedDict = new Dictionary<string, int>();
            for (int index = 0; index < keys.Count; index++)
            {
                string aKey = keys[index];
                int aValue = sortedValues[index];

                sortedDict[aKey] = aValue;
            }
            return sortedDict;
        }

    }

    class IPStatistics : Statistics
    {
        public IPStatistics() : base() { }

        public override void DisplayDictionary(Dictionary<string, int> dict)
        {
            if (dict == null) throw new ArgumentNullException("Null is passed to the method.");

            foreach (string key in dict.Keys)
            {
                if (dict[key] >= 10)
                    Console.WriteLine(dict[key] + ": " + key);
            }
        }

        public override Dictionary<string, int> SortDictionary()
        {
            List<int> values = new List<int>();
            foreach (KeyValuePair<string, int> pair in base.Frequencies)
            {
                values.Add(pair.Value);
            }

            values.Sort();
            values.Reverse();

            List<int> valuesWithoutDuplicates = new List<int>();
            for (int i = 0; i < values.Count; i++)
            {
                if (!valuesWithoutDuplicates.Contains(values[i]))
                {
                    valuesWithoutDuplicates.Add(values[i]);
                }
            }

            Dictionary<string, int> sortedDict = new Dictionary<string, int>();
            for (int i = 0; i < valuesWithoutDuplicates.Count; i++)
            {
                foreach (string key in base.Frequencies.Keys)
                {
                    if (base.Frequencies[key] == valuesWithoutDuplicates[i])
                    {
                        sortedDict.Add(key, valuesWithoutDuplicates[i]);
                    }
                }
            }
            return sortedDict;
        }

    }


    class URLStatistics : Statistics
    {
        public URLStatistics() : base() { }

        public override void DisplayDictionary(Dictionary<string, int> dict)
        {
            if (dict == null) throw new ArgumentNullException("Null is passed to the method.");

            foreach (string key in dict.Keys)
            {
                if (dict[key] >= 10)
                    Console.WriteLine(dict[key] + ": " + key);
            }
        }

        public override Dictionary<string, int> SortDictionary()
        {
            List<int> values = new List<int>();
            foreach (KeyValuePair<string, int> pair in base.Frequencies)
            {
                values.Add(pair.Value);
            }

            values.Sort();
            values.Reverse();

            List<int> valuesWithoutDuplicates = new List<int>();
            for (int i = 0; i < values.Count; i++)
            {
                if (!valuesWithoutDuplicates.Contains(values[i]))
                {
                    valuesWithoutDuplicates.Add(values[i]);
                }
            }

            Dictionary<string, int> sortedDict = new Dictionary<string, int>();
            for (int i = 0; i < valuesWithoutDuplicates.Count; i++)
            {
                foreach (string key in base.Frequencies.Keys)
                {
                    if (base.Frequencies[key] == valuesWithoutDuplicates[i])
                    {
                        sortedDict.Add(key, valuesWithoutDuplicates[i]);
                    }
                }
            }
            return sortedDict;
        }

    }
}
