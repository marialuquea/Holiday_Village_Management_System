using System;
using System.Collections.Generic;

namespace Business
{
    /*
     * Author: Maria Luque Anguita 40280156
     * Description of class: defines customer object (properties and methods)
     * Date last modified: 3/12/2017
     */
    public class Customer 
    {
        //properties of the customer object
        private string _name;
        private string _address;
        private int _customerID;
        private List<Booking> _bookingsList = new List<Booking>();

        //constructor
        public Customer(int customerID)
        {
            _customerID = customerID;
        }

        public Customer() { }

        //getters and setters
        public string Name
        {
            get { return _name; }
            set
            {
                if (value == "")
                {
                    throw new ArgumentException("Plase fill in name.");
                }
                _name = value;
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                if (value == "")
                {
                    throw new ArgumentException("Plase fill in address.");
                }
                _address = value;
            }
        }

        public int CustomerID
        {
            get { return _customerID; }
            set { _customerID = value; }
        }

        public List<Booking> BookingsList
        {
            get { return _bookingsList; }
            set { _bookingsList = value; }
        }

    }
}
