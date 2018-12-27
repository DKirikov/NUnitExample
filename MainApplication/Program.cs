using System;

namespace MainApplication
{
    class Program
    {
        private static NumberAnalyzer numberAnalyzer = new NumberAnalyzer();

        static void Main(string[] args)
        {
            PrimeNumberHandler primeNumberHandler = new PrimeNumberHandler();
            numberAnalyzer.AddListener(primeNumberHandler);

            bool working = true;
			while (working)
            {
                try
                {
                    String input = Console.ReadLine().ToLower();

                    var tokens = input.Split(' ');

                    if (tokens.Length == 0)
                        continue;

                    switch (tokens[0])
                    {
                        case "s":
                        case "start":
                            bool isStart = numberAnalyzer.StartNewSearch();
                            if (isStart)
                                Console.WriteLine("New search successfully started");
                            else
                                Console.WriteLine("Search has been already running");

                            break;
                        case "stop":
                            numberAnalyzer.StopSearch();
                            break;

                        case "check":
                            int num = int.Parse(tokens[1]);
                            if (numberAnalyzer.IsPrime(num))
                                Console.WriteLine(num + " is a prime number");
                            else
                                Console.WriteLine(num + " is not a prime number");
                            break;

                        case "quit":
                        case "exit":
                            working = false;
                            break;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
            }
        }
    }
}