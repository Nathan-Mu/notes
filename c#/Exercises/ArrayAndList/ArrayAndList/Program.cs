using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace ArrayAndList
{
    class Program
    {
        /*
         * Note: For any of these exercises, ignore input validation unless otherwise directed.
         * Assume the user enters values in the format that the program expects.
         */
        static void Main(string[] args)
        {
            Console.WriteLine("Array & List Exercise");
            Console.WriteLine("Pick from 1 to 5");
            int option = Convert.ToInt32(Console.ReadLine().Trim());
            switch (option)
            {
                case 1: 
                    Exercise1();
                    break;
                case 2: 
                    Exercise2();
                    break;
                case 3:
                    Exercise3();
                    break;
                case 4: 
                    Exercise4();
                    break;
                case 5: 
                    Exercise5();
                    break;
                default:
                    break;
            }
        }

        /*
         * 1 - When you post a message on Facebook, depending on the number of people who like your post, Facebook displays different information.
         *
         * > If no one likes your post, it doesn't display anything.
         * > If only one person likes your post, it displays: [Friend's Name] likes your post.
         * > If two people like your post, it displays: [Friend 1] and [Friend 2] like your post.
         * > If more than two people like your post, it displays: [Friend 1], [Friend 2] and [Number of Other People] others like your post.
         *
         * Write a program and continuously ask the user to enter different names, until the user presses Enter (without supplying a name).
         * Depending on the number of names provided, display a message based on the above pattern.
         */

        static void Exercise1()
        {
            var isStopped = false;
            var names = new List<string>();
            while (!isStopped)
            {
                Console.WriteLine("Please enter a name (or quit by just pressing enter ONLY):");
                var name = Console.ReadLine().Trim();
                if (String.IsNullOrEmpty(name))
                {
                    break;
                }
                else
                {
                    names.Add(name);
                }
            }

            var output = "";
            switch (names.Count)
            {
                case 0:
                    break;
                case 1:
                    output = $"{names[0]} likes your post.";
                    break;
                case 2:
                    output = $"{names[0]} and {names[1]} like your post.";
                    break;
                default:
                    output = $"{names[0]}, {names[1]} and {names.Count - 2} like your post.";
                    break;
            }
            Console.WriteLine(output);
        }

        /*
         * 2 - Write a program and ask the user to enter their name. Use an array to reverse the name and then store the result in a new string.
         *     Display the reversed name on the console.
         */
        static void Exercise2()
        {
            Console.WriteLine("Please enter your name:");
            var name = Console.ReadLine();
            var chars = name.ToCharArray();
            Console.WriteLine(new string(chars.Reverse().ToArray()));
        }

        /*
         * 3 - Write a program and ask the user to enter 5 numbers. If a number has been previously entered,
         *     display an error message and ask the user to re-try. Once the user successfully enters 5 unique numbers, sort them and display the result on the console
         *
         */
        static void Exercise3()
        {
            var numbers = new List<int>();
            while (numbers.Count != 5)
            {
                Console.WriteLine("Please enter a number:");
                var number = Convert.ToInt32(Console.ReadLine());
                if (numbers.Contains(number))
                {
                    Console.WriteLine($"{number} is existed.");
                }
                else
                {
                    numbers.Add(number);
                }
            }
            numbers.Sort();
            Console.WriteLine(string.Join(",", numbers));
        }

        /*
         * 4- Write a program and ask the user to continuously enter a number or type "Quit" to exit. The list of numbers may include duplicates.
         *    Display the unique numbers that the user has entered.
         */
        static void Exercise4()
        {
            var numbers = new List<int>();
            while (true)
            {
                Console.WriteLine("Please enter a number (or just press enter to exit):");
                var input = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(input))
                {
                    break;
                }
                else
                {
                    numbers.Add(Convert.ToInt32(input));
                }
            }
            var uniqueNumbers = new List<int>();
            foreach (var num in numbers)
            {
                if (!uniqueNumbers.Contains(num))
                {
                    uniqueNumbers.Add(num);
                }
            }
            Console.WriteLine(string.Join(",", uniqueNumbers));
        }

        /*
         * 5 - Write a program and ask the user to supply a list of comma separated numbers (e.g 5, 1, 9, 2, 10).
         *     If the list is empty or includes less than 5 numbers, display "Invalid List" and ask the user to re-try;
         *     otherwise, display the 3 smallest numbers in the list.
         */
        static void Exercise5()
        {
            var resultNumbers = new int[3];
            while (true)
            {
                Console.WriteLine("Please enter at least 5 numbers:");
                var input = Console.ReadLine();
                var strNumbers = input.Split(",");
                if (strNumbers.Length >= 5)
                {
                    var numbers = new int[strNumbers.Length];
                    for (var i = 0; i < strNumbers.Length; i++)
                    {
                        numbers[i] = Convert.ToInt32(strNumbers[i]);
                    }
                    Array.Sort(numbers);
                    Array.Copy(numbers, resultNumbers, 3);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid List. At least 5 numbers.");
                }
            }
            Console.WriteLine(string.Join(",", resultNumbers));
        }
    }
}
