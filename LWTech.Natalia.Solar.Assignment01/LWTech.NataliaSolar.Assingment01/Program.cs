using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//CSD 228
//Natalia Solar
//Assignment 1

namespace LWTech.NataliaSolar.Assingment01
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputArray = new string[3];
            string input = "";
            bool itWorked = false;
            bool go = true;
            long[] parsedInputArray = new long[3];
            long parsedInput =0;
            long minimum;
            long maximum;
            int sequenceSize = 0;
            long sequence = 0;
            int lowerBound;
            int upperBound;

            // #1 Prompt the user to enter three integers, then use a method to calculate their sum.  Display that result for the user.

            Console.WriteLine("Please enter three integers:");
            for (int i = 0; i < 3; i++)
            {
                inputArray[i] = Console.ReadLine();
                while (!itWorked)
                {
                    itWorked = long.TryParse(inputArray[i], out parsedInputArray[i]);
                    if (itWorked == false)
                    {
                        Console.WriteLine("I'm sorry. I couldn't understand what you entered.  Please enter integer again using just the number keys on your keyboard.");
                        inputArray[i] = Console.ReadLine();
                    }
                }
                itWorked = false;
            }
            Console.WriteLine($"The sum of integers you entered is: {Sum(parsedInputArray)}");
            Console.ReadLine();


            //#2 Prompt the user to enter an integer, then use a method to calculate the value of the 
            //polynomial 4x3 + 6x – 2 (where x is the number entered by the user).  Display the result for the user.

            Console.WriteLine("Please enter an integer:");
            input = Console.ReadLine();
            while (!itWorked)
            {
                itWorked = long.TryParse(input, out parsedInput);
                if (itWorked == false)
                {
                    Console.WriteLine("I'm sorry. I couldn't understand what you entered.  Please enter integer again using just the number keys on your keyboard.");
                    input = Console.ReadLine();
                }
            }
            Console.WriteLine($"The result of polynomial 4x^3 + 6x – 2 (where x is the number entered by you): {Polynomial(parsedInput)}");
            Console.ReadLine();


            // #3 Prompt the user to enter an integer representing some number of seconds and then pass that number to a method that converts it into the equivalent 
            //number of hours, minutes and seconds.  Display the results for the user. For example, if the user entered 52400, the program would display "14 hours, 33 minutes, 20 seconds".

            itWorked = false;
            Console.WriteLine("Please enter a number of seconds:");
            input = Console.ReadLine();
            while (!itWorked)
            {
                itWorked = long.TryParse(input, out parsedInput);
                if (itWorked == false)
                {
                    Console.WriteLine("I'm sorry. I couldn't understand what you entered.  Please enter integer again using just the number keys on your keyboard.");
                    input = Console.ReadLine();
                }
                if (parsedInput < 0)
                {
                    Console.WriteLine("The number of seconds should be a positive number.  Please enter number of seconds again.");
                    input = Console.ReadLine();
                    itWorked = false;
                }
            }
            Console.WriteLine($"{parsedInput} seconds is {ConvertedSeconds(parsedInput)}");
            Console.ReadLine();



            // #4 Prompt the user to enter a sequence of integers and then print out the maximum and minimum 
            //values from that sequence.  Before reading the sequence, ask the user how many numbers are in the sequence.

            itWorked = false;
            Console.WriteLine("Please enter a sequence of integers. How many numbers will be in the sequence?");
            input = Console.ReadLine();
            while (!itWorked)
            {
                itWorked = int.TryParse(input, out sequenceSize);
                if (itWorked == false)
                {
                    Console.WriteLine("I'm sorry. I couldn't understand what you entered.  Please enter sequence size again using just the number keys on your keyboard.");
                    input = Console.ReadLine();
                }
                if (sequenceSize < 0)
                {
                    Console.WriteLine("The size of a sequence should be a positive number.  Please enter sequence size again.");
                    input = Console.ReadLine();
                    itWorked = false;
                }
            }
            Console.WriteLine($"sequence size {sequenceSize}");
            Console.WriteLine("Please enter sequence integers:");
            input = Console.ReadLine();
            itWorked = false;
            while (!itWorked)
            {
                itWorked = long.TryParse(input, out sequence);
                if (itWorked == false)
                {
                    Console.WriteLine("I'm sorry. I couldn't understand what you entered.  Please enter sequence size again using just the number keys on your keyboard.");
                    input = Console.ReadLine();
                }                
            }
            minimum = maximum = sequence;
            itWorked = false;
            for (int i = 0; i < sequenceSize - 1; i++)
            {
                input = Console.ReadLine();
                while (!itWorked)
                {
                    itWorked = long.TryParse(input, out sequence);
                    if (itWorked == false)
                    {
                        Console.WriteLine("I'm sorry. I couldn't understand what you entered.  Please enter integer again using just the number keys on your keyboard.");
                        input = Console.ReadLine();
                    }
                }               
                if (sequence < minimum)
                {
                    minimum = sequence;
                }
                if (sequence > maximum)
                {
                    maximum = sequence;
                }
                itWorked = false;
            }
            Console.WriteLine($"The maximum value in the sequence: {maximum}\nThe minimum value in the sequence: {minimum} ");
            Console.ReadLine();


            //#5 Use a while-loop to display all even integers between 150 and 200 (inclusive) in ascending order.           
            lowerBound = 150;
            upperBound = 200;
            while (lowerBound <= upperBound)
            {
                if (lowerBound % 2 == 0) Console.WriteLine(lowerBound);
                lowerBound++;
            }
            Console.ReadLine();

            // #6 Use a do-loop to display all even integers between 100 and 0 (inclusive) in descending order.

            upperBound = 100;
            lowerBound = 0;
            do
            {
                if (upperBound % 2 == 0) Console.WriteLine(upperBound);
                upperBound--;
            } while (upperBound >= lowerBound);
            Console.ReadLine();


            // #7 Create a method that uses a switch statement to convert a test score into a letter grade based on the following table:
            //91 to 100   A
            //81 to 90    B
            //71 to 80    C
            //61 to 70    D
            //60 and below    F
            //Prompt the user for a test score, convert that score into a letter grade(using the method you created above) 
            //and then display the grade.Continue to ask the user for test scores to convert until they enter "quit"(case-insensitive).

            itWorked = false;
            Console.WriteLine("Please enter a test score:");
            input = Console.ReadLine();
            while (go)
            {              
                while (!itWorked)
                {
                        itWorked = long.TryParse(input, out parsedInput);
                    if (itWorked == false)
                    {
                        Console.WriteLine("I'm sorry. I couldn't understand what you entered.  Please enter integer again using just the number keys on your keyboard.");
                        input = Console.ReadLine();
                    }
                    else if (parsedInput < 0 || parsedInput > 100)
                    {
                        Console.WriteLine("The score should be a positive number from 0 to 100.  Please enter a test score again.");
                        input = Console.ReadLine();
                        itWorked = false;
                    }
                }
                itWorked = false;
                Console.WriteLine($"Your grade is {LetterGrade(parsedInput)}\nPlease enter a new score (To quit enter: 'quit'): ");
                input = Console.ReadLine();
                if (input.ToLower() == "quit") go = false;
            }
        }
        


            public static long Sum(long[] array)
            {
                long total = 0;
                foreach (long integer in array)
                {
                    total += integer;
                }
                return total;
            }


            public static long Polynomial(long x)
            {
                return 4 * ((long)Math.Pow(x, 3)) + 6 * x - 2;
            }


            public static string ConvertedSeconds(long sec)
            {
                long hours = sec / 3600;
                long minutes = (sec % 3600) / 60;
                long seconds = (sec % 3600) % 60;
                return $"{hours} hour(s), {minutes} minute(s), {seconds} second(s)";

            }


            public static string LetterGrade(long score)
            {
                string letterGrade = "";
                int caseNumber;
            caseNumber = (int)((score - 1) / 10);
                switch (caseNumber)
                {
                    case 9:
                        letterGrade = "A";
                        break;
                    case 8:
                        letterGrade = "B";
                        break;
                    case 7:
                        letterGrade = "C";
                        break;
                    case 6:
                        letterGrade = "D";
                        break;
                    default:
                        letterGrade = "F";
                        break;
                }
                return letterGrade;
            }
        
    }
}
