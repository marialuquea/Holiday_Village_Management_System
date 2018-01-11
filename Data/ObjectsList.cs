using System.Collections.Generic;
using Business;

namespace Data
{
    /*
     * Author: Maria Luque Anguita 40280156
     * Description of class: methods to read and save objects to lists
     * Date last modified: 3/12/2017
     * Dessign pattern: Singleton
     */
    public class ObjectsList
    {
        //lists that will store objects
        private List<Customer> custList = new List<Customer>();
        private List<Guest> guestList = new List<Guest>();
        private List<Booking> bookingList = new List<Booking>();

        //singleton 
        private static ObjectsList instance;

        //singleton constructor
        private ObjectsList() { }

        public static ObjectsList Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ObjectsList();
                }
                return instance;
            }
        }

        //Add
        public void addCustomer(Customer cust)
        {
            custList.Add(cust);
        }
        
        //Find
        public Guest findGuest(string passport)
        {
            foreach (Guest g in guestList)
            {
                if (passport == g.PassportNo)
                {
                    return g;
                }
            }
            return null;
        }

        public Booking findBooking(int id)
        {
            foreach (Booking b in bookingList)
            {
                if (id == b.BookingID)
                {
                    return b;
                }
            }
            return null;
        }

        public Customer findCustomer(int id)
        {
            foreach (Customer c in custList)
            {
                if (id == c.CustomerID)
                {
                    return c;
                }
            }
            return null;
        }

        //Delete
        public void deleteCustomer(int reference)
        {
            Customer c = this.findCustomer(reference);
            if (c != null)
            {
                custList.Remove(c);
            }
        }

        public void deleteBooking(int id)
        {
            Booking b = this.findBooking(id);
            if (b != null)
            {
                bookingList.Remove(b);
            }
        }

        //Return complete lists
        public List<Booking> allBookings
        {
            get { return bookingList; }
            set { bookingList = value; }
        }

        public List<Customer> allCustomers
        {
            get { return custList; }
            set { custList = value; }
        }

    }
}
