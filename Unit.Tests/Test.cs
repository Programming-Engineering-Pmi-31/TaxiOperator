using System;
using OperatorTaxi;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class ModelTesting
    {
        [TestMethod]
        public void OrderConstructorTest()
        {
            string StartPoint = "Start";
            string EndPoint = "End";
            int Count = 4;
            status Status = status.NotAppointed;
            string Number = "------";

            Order testOrder = new Order(StartPoint, EndPoint, Count, Status, Number);
            Assert.AreEqual(StartPoint, testOrder.StartPoint);
            Assert.AreEqual(EndPoint, testOrder.EndPoint);
            Assert.AreEqual(Count, testOrder.Count);
            Assert.AreEqual(Status, testOrder.Status);
            Assert.AreEqual(Number, testOrder.CarNumber);
        }

        [TestMethod]
        public void TaxistConstructorTest()
        {
            string Model = "QWER";
            string Number = "CD1432";
            bool Busy = false;
            Taxist testTaxi = new Taxist(Model, Number, Busy);
            Assert.AreEqual(Model, testTaxi.CarModel);
            Assert.AreEqual(Number, testTaxi.Number);
            Assert.AreEqual(Busy, testTaxi.Busy);
        }

    }

}
