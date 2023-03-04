using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabInvoiceGeneratorProblem
{
    public class InvoiceGenerator
    {
        private readonly double MINIMUM_COST_PER_KM;
        private readonly int COST_PER_TIME;
        private readonly double MINIMUM_FARE;
        RideType rideType;
        private RideRepository rideRepository;
        /// <summary>
        /// Constructor to create RideRepository instance.
        /// </summary>
        /// <param name="rideType"></param>
        /// <exception cref="CabInvoiceException"></exception>
        public InvoiceGenerator(RideType rideType)
        {
            this.rideType = rideType;
            this.rideRepository = new RideRepository();
            //if ride type is Premium then rates for Premium else for Normal.
            if (rideType.Equals(RideType.PREMIUM))
            {
                this.MINIMUM_COST_PER_KM = 15;
                this.COST_PER_TIME = 2;
                this.MINIMUM_FARE = 20;
            }
            else if (rideType.Equals(RideType.NORMAL))
            {
                this.MINIMUM_COST_PER_KM = 10;
                this.COST_PER_TIME = 1;
                this.MINIMUM_FARE = 5;
            }
        }
        /// <summary>
        /// Function to calculate fare
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        /// <exception cref="CabInvoiceException"></exception>
        public double CalculateFare(double distance, double time)
        {
            if (distance <= 0)
            {
                throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_DISTANCE, "Invalid Distance");
            }
            else if (time < 0)
            {
                throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_TIME, "Invalid Time");
            }
            else
            {
                //Calculating total fare
                double totalFare = 0;
                totalFare = distance * MINIMUM_COST_PER_KM + time * COST_PER_TIME;
                Console.WriteLine("Given diatance => {0} and time => {1} should return\ntotal fare => {2}",distance,time,totalFare);
                return Math.Max(totalFare, MINIMUM_FARE);
            }
        }
        /// <summary>
        /// Function to calculate Total Fare and generating  summary for multiple Rides. 
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        /// <exception cref="CabInvoiceException"></exception>
        public InvoiceSummary CalculateFare(Ride[] rides)
        {
            double totalFare = 0;
            foreach (Ride ride in rides)
            {
                totalFare += this.CalculateFare(ride.distance, ride.time);
            }
            //Console.WriteLine("\nTotal fare for {0} rides => {1}", rides.Length,totalFare);
            return new InvoiceSummary(rides.Length, totalFare);
        }
        /// <summary>
        /// Function to get summary by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="rides"></param>
        /// <exception cref="CabInvoiceException"></exception>
        public void AddRides(string userId, Ride[] rides)
        {
            rideRepository.AddRide(userId, rides);
            Console.WriteLine("Adding userId =>{0}",userId);
        }

        /// <summary>
        /// Function to get summary by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="CabInvoiceException"></exception>
        public InvoiceSummary GetInvoiceSummary(string userId)
        {
            Console.WriteLine("\nGetting userId =>{0}", userId);
            return this.CalculateFare(rideRepository.GetRides(userId));
        }
    }
}
