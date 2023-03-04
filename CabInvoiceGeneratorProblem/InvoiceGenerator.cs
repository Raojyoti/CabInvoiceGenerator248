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
        //Constants
        private readonly double MINIMUM_COST_PER_KM;
        private readonly int COST_PER_TIME;
        private readonly double MINIMUM_FARE;
        
        RideType rideType;

        /// <summary>
        /// Constructor to create RideRepository instance.
        /// </summary>
        /// <param name="rideType"></param>
        /// <exception cref="CabInvoiceException"></exception>
        public InvoiceGenerator(RideType rideType)
        {
            this.rideType = rideType;
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
    }
}
