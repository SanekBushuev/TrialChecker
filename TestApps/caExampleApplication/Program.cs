using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrialChecker;

namespace caExampleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Example of using TrialChecker");
            try
            {
                if (!TrialChecker.TrialChecker.CheckValidity())
                    Console.WriteLine($"Лицензия не прошла проверку");
                else
                    Console.WriteLine($"Лицензия прошла проверку");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
            }
            Console.ReadKey();
        }
    }
}
