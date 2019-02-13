using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson4
{
    class Task2_c
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            List<int> numbers = new List<int>();
            for (int i = 0; i < 20; i++) numbers.Add(rnd.Next(1, 7));
            foreach (int i in numbers) Console.Write(" " + i);
            Console.WriteLine();
            foreach (int val in numbers.Distinct())
            {
              Console.WriteLine(val + " - " + numbers.Where(x => x == val).Count() + " раз");
            }
            Console.ReadKey();
        }
       
    }
}