using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Business;

namespace UnitTestProject1
{
    /*
     * Author: Maria Luque Anguita 40280156
     * Description of class: tests validation of Booking class
     * Date last modified: 3/12/2017
     */

    [TestClass]
    public class UnitTest1
    {

        //the booking object to test
        Booking testBooking = new Booking();

        //test for an argument exception when arrival date is wrong
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_arrival_date()
        {
            testBooking.ArrivalDate = DateTime.Now.AddDays(-1);
        }

        //test for an argument exception when departure date is wrong
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_departure_date()
        {
            testBooking.ArrivalDate = DateTime.Now.AddDays(+1);
            testBooking.DepartureTime = DateTime.Now.AddDays(-3);
        }

        //test for an argument exception when booking ID is wrong
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_bookingID()
        {
            testBooking.BookingID = -2;
        }

        //test for an argument exception when a chaletID is wrong
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_chaletID()
        {
            testBooking.ChaletID = 14;
        }
        
    }
}
