using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabInvoiceGeneratorProblem
{
    /// <summary>
    /// RideRepository class for Rides List.
    /// </summary>
    public class RideRepository
    {
        //Dictionary to store UserId Rides int List.
        Dictionary<string, List<Ride>> userRides = new Dictionary<string, List<Ride>>();
        /// <summary>
        /// Constructor to create Dictionary
        /// </summary>
        public RideRepository()
        {
            this.userRides = new Dictionary<string, List<Ride>>();
        }
        /// <summary>
        /// Function to add Ride list to specified UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="rides"></param>
        /// <exception cref="CabInvoiceException"></exception>
        public void AddRide(string userId, Ride[] rides)
        {
            bool rideList = this.userRides.ContainsKey(userId);
            if (!rideList)
            {
                List<Ride> list = new List<Ride>();
                list.AddRange(rides);
                this.userRides.Add(userId, list);
            }
        }
        /// <summary>
        /// Function to get rides list As an array for specified user Id 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="CabInvoiceException"></exception>
        public Ride[] GetRides(string userId)
        {
            return this.userRides[userId].ToArray();
        }
    }
}
