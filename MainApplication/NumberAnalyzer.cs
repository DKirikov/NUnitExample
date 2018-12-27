using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MainApplication
{
    public delegate void PrimeIsFoundEventHandler(int primeNumber);

    public interface INumberAnalyzer
    {
        bool IsPrime(int candidate);

        void AddListener(INumberAnalyzerListener numberAnalyzerListener);

        bool StartNewSearch();
        void StopSearch();
    }

    public class NumberAnalyzer : INumberAnalyzer
    {
        public bool IsPrime(int candidate)
        {
            if (candidate < 2)
                return false;

            for (int i = 2; i < candidate; i++)
            {
                if (candidate % i == 0)
                    return false;
            }

            return true;

            //throw new NotImplementedException();
        }

        public bool StartNewSearch()
        {
            if (!stop)
                return false;

            stop = false;
            Thread.Sleep(20);//it's necessary to complete the calculation of IsPrime

            Task.Run(() => StartNewSearchInt());
            return true;
        }

        public void StopSearch()
        {
            stop = true;
        }

        public void AddListener(INumberAnalyzerListener numberAnalyzerListener)
        {
            primeIsFoundEvent += numberAnalyzerListener.ShowNewPrime;
        }

        private void StartNewSearchInt()
        {
            int candidate = 0;
            while (!stop)
            {
                Thread.Sleep(100);
                if (IsPrime(candidate))
                {
                    if (primeIsFoundEvent != null)
                        primeIsFoundEvent(candidate);
                }

                candidate++;
            }
        }

        private event PrimeIsFoundEventHandler primeIsFoundEvent;
        private bool stop = true;
    }
}