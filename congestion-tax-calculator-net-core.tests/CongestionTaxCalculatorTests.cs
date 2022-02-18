using congestion.calculator;
using congestion.calculator.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace congestion_tax_calculator_net_core.tests
{
    [TestClass]
    public class CongestionTaxCalculatorTests
    {
        [TestMethod]
        public void TestStraightForwardScenario()
        {
            var manager = new CongestionTaxCalculator();
            var tax = manager.GetTax(VehicleType.Car, new[] {
               DateTime.Parse("2013-01-14 15:30:00"), //18
               DateTime.Parse("2013-01-15 21:00:00"), //0
               DateTime.Parse("2013-02-07 06:23:27"), //8
               DateTime.Parse("2013-02-07 15:27:00"), //13
            });
            Assert.AreEqual(tax, 39);
        }

        [TestMethod]
        public void TestTollsInWeekEndsScenario()
        {
            var manager = new CongestionTaxCalculator();
            var tax = manager.GetTax(VehicleType.Car, new[] {
               DateTime.Parse("2013-02-09 15:00:00"), //Weekend-Saturday
               DateTime.Parse("2013-02-10 07:00:00"), //Weekend-Sunday
               DateTime.Parse("2013-02-11 09:23:27"), //8
            });
            Assert.AreEqual(tax, 8);
        }
       
        [TestMethod]
        public void TestGivenOnDeskScanrio()
        {
            var manager = new CongestionTaxCalculator();
            var tax = manager.GetTax(VehicleType.Car, new[] {
               DateTime.Parse("2013-01-14T21:00:00"), //0
               DateTime.Parse("2013-01-15T21:00:00"), //0
               DateTime.Parse("2013-02-07T06:23:27"), //8
               DateTime.Parse("2013-02-07T15:27:00"), //13
               DateTime.Parse("2013-02-08T06:27:00"), //G1-8
               DateTime.Parse("2013-02-08T06:20:27"), //G1-8
               DateTime.Parse("2013-02-08T14:35:00"), //G2-8
               DateTime.Parse("2013-02-08T15:29:00"), //G2-13
               DateTime.Parse("2013-02-08T15:47:00"), //G3-18
               DateTime.Parse("2013-02-08T16:01:00"), //G3-18
               DateTime.Parse("2013-02-08T16:48:00"), //18
               DateTime.Parse("2013-02-08T17:49:00"), //G4-13 CutSameDay-11
               DateTime.Parse("2013-02-08T18:29:00"), //G4-8 CutSameDay-0
               DateTime.Parse("2013-02-08T18:35:00"), //G4-0
               DateTime.Parse("2013-03-25T14:25:00"), //DayBeforeHoliday
               DateTime.Parse("2013-03-26T14:25:00"), //Holiday
               DateTime.Parse("2013-03-28T14:07:27"), //8
               DateTime.Parse("2013-03-30T14:07:27") //Weekend-Saturday
            });
            Assert.AreEqual(tax, 89);
        }

        [TestMethod]
        public void TestTollFreeInJulyScanrio()
        {
            var manager = new CongestionTaxCalculator();
            var tax = manager.GetTax(VehicleType.Car, new[] {
               DateTime.Parse("2013-07-14 21:00:00"),
               DateTime.Parse("2013-07-15 21:00:00"),
               DateTime.Parse("2013-07-07 06:23:27"),
               DateTime.Parse("2013-07-07 15:27:00"),
               DateTime.Parse("2013-07-08 06:27:00"),
               DateTime.Parse("2013-07-22 06:20:27"),
               DateTime.Parse("2013-07-15 14:35:00"),
            });
            Assert.AreEqual(tax, 0);
        }

        [TestMethod]
        public void TestTollFreeVehcles()
        {
            var manager = new CongestionTaxCalculator();
            var tax = manager.GetTax(VehicleType.Motorcycle, new[] {
               DateTime.Parse("2013-01-14 21:00:00"),
               DateTime.Parse("2013-02-15 21:00:00"),
               DateTime.Parse("2013-03-07 06:23:27"),
               DateTime.Parse("2013-04-07 15:27:00"),
               DateTime.Parse("2013-05-08 06:27:00"),
               DateTime.Parse("2013-06-22 06:20:27"),
               DateTime.Parse("2013-07-15 14:35:00"),
            });
            Assert.AreEqual(tax, 0);
        }
    }
}
