using System;
using System.Collections.Generic;

namespace Lesson4
{
    class Task2
    {
        static void Main(string[] args)
        {
            int counter=1;

            Random rnd = new Random();
            List<int> numbers = new List<int>();
            for (int i = 0; i <20; i++) numbers.Add(rnd.Next(1, 7));
            foreach (int i in numbers) Console.Write(" " + i);
            for (int i = 0; i < 20; i++)
            {
                for (int j = i+1; j < 20; j++)
                {
                        if (numbers[i] == numbers[j])
                    {
                        counter++;
                        numbers[j] = 0;
                    }
                }
               if (numbers[i] != 0)
                Console.Write("\n"+numbers[i]+" встречается "+counter+" раз");
                counter = 1;
            }
            Console.ReadKey();
        }
    }
    
}
