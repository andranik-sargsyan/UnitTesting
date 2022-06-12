using Moq;
using NUnit.Framework;
using System;

namespace UnitTesting
{
    internal class Program
    {
        private Mock<API> apiMock;

        static void Main()
        {

        }

        [SetUp]
        public void Start()
        {
            apiMock = new Mock<API>();

            apiMock
                .Setup(x => x.Call())
                .Verifiable();
            apiMock
                .Setup(x => x.GetPaymentMethod(It.Is<int>(m => m < 1000)))
                .Returns("PayPal");
            apiMock
                .Setup(x => x.GetPaymentMethod(It.Is<int>(m => m >= 1000)))
                .Returns("Debit card");

            Bank.Api = apiMock.Object;

            Console.WriteLine("SetUp");
        }

        [TearDown]
        public void End()
        {
            Console.WriteLine("TearDown");
        }

        [Test]
        public void RequestLoan_HappyCaseNormalMoney()
        {
            int expectedResult = 200;

            int actualResult = Bank.RequestLoan(800, 4);

            apiMock.Verify(x => x.Call(), Times.Once);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void RequestLoan_HappyCaseBigAmount()
        {
            int expectedResult = 440;

            int actualResult = Bank.RequestLoan(1800, 4);

            apiMock.Verify(x => x.Call(), Times.Once);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void RequestLoan_MaxLimitExceeded()
        {
            var exception = Assert.Throws<Exception>(() => Bank.RequestLoan(800, 7));

            apiMock.Verify(x => x.Call(), Times.Never);
            Assert.AreEqual("Max count months should be 6.", exception.Message);
        }

        [Test]
        public void RequestLoan_MinLimitExceeded()
        {
            var exception = Assert.Throws<Exception>(() => Bank.RequestLoan(800, 1));

            apiMock.Verify(x => x.Call(), Times.Never);
            Assert.AreEqual("Min count months should be 3.", exception.Message);
        }

        [Test]
        public void RequestLoan_InvalidMoney()
        {
            var exception = Assert.Throws<Exception>(() => Bank.RequestLoan(-100, 4));

            apiMock.Verify(x => x.Call(), Times.Never);
            Assert.AreEqual("Money can't be negative or zero.", exception.Message);
        }
    }
}
