using System;

namespace MainApplication
{
    public interface INumberAnalyzerListener
    {
        void ShowNewPrime(int sum);
    }

    public class PrimeNumberHandler : INumberAnalyzerListener
    {
        public void ShowNewPrime(int sum)
        {
            Console.WriteLine(sum);
        }
    }
}
