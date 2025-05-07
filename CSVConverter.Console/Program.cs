using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using CSVConverter.BusinessLogic;

namespace CSVConverter.TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //TestTheConversion();
            //TestLoadCSV();
            TestBulk();
        }

        static void TestTheConversion()
        {
            Console.WriteLine("Testing the Conversion");
            Console.Write("first record: ");
            string recordOne = Console.ReadLine();
            Console.Write("second record: ");
            string recordTwo = Console.ReadLine();

            MainConverter myConverter = new MainConverter(recordOne, recordTwo);

            Console.WriteLine($"record 1 : {myConverter.newString.recordOne} and record 2 : {myConverter.newString.recordTwo}");
        }

        static void TestLoadCSV()
        {
            Console.WriteLine("Testing CSV Loading");
            Console.Write("source CSV: ");
            string sourceFile = Console.ReadLine();
            Console.Write("target CSV: ");
            string targetFile = Console.ReadLine();

            CSVLoader csvLoader = new CSVLoader(sourceFile);
            csvLoader.Transform(targetFile);

            Console.WriteLine("done!");
        }

        static void TestBulk()
        {
            Console.WriteLine("Testing multiple CSV Loading");
            Console.Write("source CSV folder : ");
            string sourceFolder = Console.ReadLine();
            Console.Write("target CSV folder: ");
            string destFolder = Console.ReadLine();

            foreach (string sourceFile in Directory.GetFiles(sourceFolder,"*.csv", SearchOption.TopDirectoryOnly))
            {
                CSVLoader csvLoader = new CSVLoader(sourceFile);
                csvLoader.Transform(destFolder + @"\" + Path.GetFileName(sourceFile));
            }

            Console.WriteLine("done!");
        }
    }
}
