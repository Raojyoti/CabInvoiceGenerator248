using CabInvoiceGeneratorProblem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

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
            //Creating instance of InvoiceGenerator for rides
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
            InvoiceSummary exceptedSummary = new InvoiceSummary(2, 30.0);
            //Asserting values
            Assert.AreEqual(exceptedSummary, summary);
        }

        /// <summary>
        /// Srep3- Enhanced Invoice
        /// </summary>
        [TestMethod]
        [DataRow(RideType.NORMAL)]
        public void GivenProperDistanceAndTimeForMultipleRideShouldTotalNumberOfRidesAndTotalFareAndAverageFarePerRide(RideType rideType)
        {
            //Creating instance of InvoiceGenerator for normal ride
            InvoiceGenerator cabInvoice = new InvoiceGenerator(rideType);
            Ride[] rides = { new Ride(4.0, 5.0), new Ride(2.0, 5.0), new Ride(0.1, 0.5) };
            //Generating summary for rides
            InvoiceSummary exceptedSummary = cabInvoice.CalculateFare(rides);
            //Asserting values
            Assert.AreEqual(3, exceptedSummary.numberOfRides);//3
            Assert.AreEqual(75, exceptedSummary.totalFare);//45+25+5=75
            Assert.AreEqual(25, exceptedSummary.averageFare);//75/3=25
            Console.WriteLine("\nNumber of rides => {0} \nTotal fare for {0} rides => {1} \nAverage fare for {0} rides => {2}", rides.Length, exceptedSummary.totalFare, exceptedSummary.averageFare);
        }

        /// <summary>
        /// Srep4- Invoice Service
        /// </summary>
        [TestMethod]
        public void GivenUserIDInvoiceServiceFromRideRepositoryGetsListofRidesReturnInvoice()
        {
            InvoiceGenerator cabInvoice = new InvoiceGenerator(RideType.NORMAL);
            Ride[] ride = { new Ride(4.0, 5.0), new Ride(2.0, 5.0), new Ride(0.1, 0.5) };
            Ride[] rideNew = { new Ride(6.0, 5.0), new Ride(4.0, 5.0) };
            cabInvoice.AddRides("Anmol", ride);
            cabInvoice.AddRides("Riya", rideNew);
            InvoiceSummary exceptedSummary = cabInvoice.GetInvoiceSummary("Anmol");
            InvoiceSummary exceptedSummaryNew = cabInvoice.GetInvoiceSummary("Riya");
            //Asserting values
            Assert.AreEqual(3, exceptedSummary.numberOfRides);
            Assert.AreEqual(75, exceptedSummary.totalFare);
            Assert.AreEqual(25, exceptedSummary.averageFare);
            Console.WriteLine("\nForUserId: Anmol\nNumber of rides => {0} \nTotal fare for {0} rides => {1} \nAverage fare for {0} rides => {2}", exceptedSummary.numberOfRides, exceptedSummary.totalFare, exceptedSummary.averageFare);

            Assert.AreEqual(2, exceptedSummaryNew.numberOfRides);
            Assert.AreEqual(110, exceptedSummaryNew.totalFare);
            Assert.AreEqual(55, exceptedSummaryNew.averageFare);
            Console.WriteLine("\nForUserId: Riya\nNumber of rides => {0} \nTotal fare for {0} rides => {1} \nAverage fare for {0} rides => {2}", exceptedSummaryNew.numberOfRides, exceptedSummaryNew.totalFare, exceptedSummaryNew.averageFare);
        }
    }
}
