using CabInvoiceGeneratorProblem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TestCabInvoiceGenerator
{
    [TestClass]
    public class TestCabInvoice
    {
        /// <summary>
        /// Step1- Given distance and time should return total fare
        /// </summary>
        [DataRow(1.5, 2.5, 27.5, RideType.PREMIUM)]
        [DataRow(3.0, 5.0, 35.0, RideType.NORMAL)]
        [TestMethod]
        public void GiveDistanceAndTimeShouldReturnTotalFare(double distance, double time, double expected, RideType rideType)
        {
            //Creating instance of InvoiceGenerator for normal ride
            InvoiceGenerator cabInvoice =new InvoiceGenerator(rideType);
            //calculate fare
            double totalFare = cabInvoice.CalculateFare(distance, time);//((1.5*15) + (2.5*2))=27.5 And ((3*10) + (5*1))=35
            //Asserting values
            Assert.AreEqual(expected, totalFare);
        }

        /// <summary>
        /// Step1.1- Given invalid distance should throw custom exception.
        /// Given invalid time should throw custom exception.
        /// </summary>
        [TestMethod]
        [DataRow(-1.5, 2.5, "Invalid Distance")]
        [DataRow(1.5, -2.5, "Invalid Time")]
        public void GiveInvalidDistanceShouldThrowCustomExceptions(double distance, double time, string expected)
        {
            //Creating instance of InvoiceGenerator for normal ride
            InvoiceGenerator cabInvoice = new InvoiceGenerator(RideType.NORMAL);
            //calculate fare
            var actual = Assert.ThrowsException<CabInvoiceException>(() => cabInvoice.CalculateFare(distance, time));
            //Asserting values
            Assert.AreEqual(actual.Message, expected);
            Console.WriteLine("Given diatance => {0} and time => {1} should \nthrow CabInvoiceException => {2} ", distance, time, actual.Message);
        }

        /// <summary>
        /// Step2- Calculate fare function for multiple invoice rides summary
        /// </summary>
        [TestMethod]
        public void GiveMultipleRidesShouldReturnInvoiceSummary()
        {
            //Creating instance of InvoiceGenerator for normal ride
            InvoiceGenerator cabInvoice = new InvoiceGenerator(RideType.NORMAL);
            Ride[] rides =
            {
                new Ride(2.0, 5),
                new Ride(0.1, 1)
            };
            //Generating summary for rides
            InvoiceSummary summary = cabInvoice.CalculateFare(rides);
            //calculate fare
            InvoiceSummary exceptedSummary = new InvoiceSummary(2, 30.0);
            //Asserting values
            Assert.AreEqual(exceptedSummary, summary);
        }
    }
}
