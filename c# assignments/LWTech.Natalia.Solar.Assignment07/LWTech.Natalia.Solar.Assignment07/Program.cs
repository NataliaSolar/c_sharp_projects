using System;
using System.Collections.Generic;
using System.IO;

namespace LWTech.Natalia.Solar.Assignment07
{

    





    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string line;
                string[] token;
                string IPaddress;
                string status;
                string URLpaths;

                Statistics statusFrequencies = new StatusStatistics();
                Statistics IPFrequencies = new IPStatistics();
                Statistics URLFrequencies = new URLStatistics();

                using (StreamReader sr = new StreamReader("..//..//..//access-log.txt"))
                while (!sr.EndOfStream)
                {

                    line = sr.ReadLine();

                    token = line.Split('"');
                    token = token[2].Split(' ');
                    status = token[1];
                    statusFrequencies.PopulateDoctionary(status);                    

                    token = line.Split(' ');
                    IPaddress = token[0];
                    IPFrequencies.PopulateDoctionary(IPaddress);

                    token = line.Split('"');
                    token = token[1].Split(' ');
                    token = token[1].Split('?');
                    URLpaths = token[0];
                    URLFrequencies.PopulateDoctionary(URLpaths);
                }


                Console.WriteLine("\nStatus Frequencies:\n===================\n");
                statusFrequencies.DisplayDictionary(statusFrequencies.SortDictionary());

                Console.WriteLine("\nIP Frequencies:\n===================\n");
                IPFrequencies.DisplayDictionary(IPFrequencies.SortDictionary());

                Console.WriteLine("\nURL Frequencies:\n===================\n");
                URLFrequencies.DisplayDictionary(URLFrequencies.SortDictionary());
                Console.ReadLine();

            }
            catch (IOException ex)
            {
                Console.WriteLine("A filesystem error occurred.  " + ex.Message);
                Console.WriteLine("Unable to continue.");
                Console.ReadLine();
            }
        }
    }
}
