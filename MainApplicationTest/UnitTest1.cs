using System;
using MainApplication;
using NUnit.Framework;
using FluentAssertions;
using FakeItEasy;
using System.Threading;

namespace MainApplicationTest
{
    [TestFixture]
    public class UnitTest1
    {
        public UnitTest1()
        {
            numberAnalyzer = new NumberAnalyzer();
        }

        private readonly NumberAnalyzer numberAnalyzer;

        [Test]
        public void ReturnFalseGivenValueOf1()
        {
            var result = numberAnalyzer.IsPrime(1);

            Assert.IsFalse(result, 1 + " should not be prime");
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        public void ReturnFalseGivenValuesLessThan2(int value)
        {
            var result = numberAnalyzer.IsPrime(value);

            result.Should().BeFalse();
            Assert.IsFalse(result, @"{value} should not be prime");
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(10000)]
        public void ReturnCorrectValueForRandomValues(int count)
        {
            Func<int, bool> isPrime = candidate =>
            {
                if (candidate < 2)
                    return false;

                for (int i = 2; i < candidate; i++)
                {
                    if (candidate % i == 0)
                        return false;
                }

                return true;
            };

            Random random = new Random();

            for (int v = 0; v < count; v++)
            {
                int candidate = random.Next(1000000);
                var actualResult = numberAnalyzer.IsPrime(candidate);

                var expectedResult = isPrime(candidate);

                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        [Test]
        public void ShowNewPrimeShouldBeCalled()
        {
            INumberAnalyzerListener fakeAnalyzerListener = A.Fake<INumberAnalyzerListener>();
            numberAnalyzer.AddListener(fakeAnalyzerListener);
            numberAnalyzer.StartNewSearch();
            Thread.Sleep(500);
            A.CallTo(() => fakeAnalyzerListener.ShowNewPrime(A<int>.Ignored)).MustHaveHappened();
            numberAnalyzer.StopSearch();

            //This code shows how to check that event subscription has been made:
            /*INumberAnalyzer fakeAnalyzer = A.Fake<INumberAnalyzer>();
            INumberAnalyzerListener fakeAnalyzerListener = A.Fake<INumberAnalyzerListener>();
            fakeAnalyzer.sumEvent += fakeAnalyzerListener.SetNewSum;
            A.CallTo(fakeAnalyzer).Where(x => x.Method.Name.Equals("add_sumEvent")).MustHaveHappened();*/
        }

        /*[Test]
        public void CheckNumberAnalyzerListener()
        {
            INumberAnalyzerListener fakeAnalyzerListener = A.Fake<INumberAnalyzerListener>();
            numberAnalyzer.sumEvent += fakeAnalyzerListener.SetNewSum;
            int x = numberAnalyzer.Sum(10, 20);

            A.CallTo(() => fakeAnalyzerListener.SetNewSum(A<int>._)).MustHaveHappened();
        }*/
    }
}