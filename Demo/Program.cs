using Demo.Business;
using Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] names = new string[] { "Andrew", "Mary", "Justin", "Susan", "Jennifer", "Anna", "Angie", "Adrienne", "Jude", "John" };
            var processor = new ProcessorNameCounter(names);
            processor.OnProgressEvent += Processor_OnProgressEvent;
            processor.OnCompleteEvent += (sender, results) => Console.WriteLine($"We processed {results.Count()} elements");
            processor.OnCompleteEvent += Processor_OnCompleteEvent;

            processor.Execute();
        }

        private static void Processor_OnCompleteEvent(object arg1, IEnumerable<Result> args)
        {
            Console.Clear();
            Console.WriteLine("Processed: {0} items", args.Count());

            var allElements = args.ToList();

            var greaterThanSix = allElements.Where(r => r.Length > 6).Select(r => r).ToList();

            var greaterThanSixQuery = (from result in allElements
                                       where result.Length > 6
                                       select result).ToList();


            Console.WriteLine($"Printing greater than 6{Environment.NewLine}");
            greaterThanSix.ForEach(p => Console.WriteLine(p));

            Console.WriteLine($"Printing greater than 6 Query{Environment.NewLine}");
            greaterThanSixQuery.ForEach(Console.WriteLine);

            Console.WriteLine($"Printing sorted{Environment.NewLine}");
            var sorted = args.OrderBy(p => p.Length).ToList();
            sorted.ForEach(Console.WriteLine);

            var grouped = allElements.GroupBy(p => p.Length);
            Console.WriteLine($"Printing grouped{Environment.NewLine}");

            foreach (var item in grouped)
            {
                Console.WriteLine("Grouped by: {0}", item.Key);
                foreach (var group in item)
                {
                    Console.WriteLine(group);
                }
            }
        }

        private static void Processor_OnProgressEvent(object sender, float args, Result item)
        {
            Console.Clear();
            Console.WriteLine("Processing: {0}", item);
            for (var i = 0; i < args; i++)
            {
                Console.Write("* ");
            }
        }
    }
}
