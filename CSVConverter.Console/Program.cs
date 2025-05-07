using System;
using CSVConverter.BusinessLogic;

namespace CSVConverter.TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string recordOne = Console.ReadLine();
            string recordTwo = Console.ReadLine();

            MainConverter myConverter = new MainConverter(recordOne, recordTwo);
            Console.WriteLine($"record 1 : {myConverter.newString.recordOne} and record 2 : {myConverter.newString.recordTwo}");
        }
    }
}
