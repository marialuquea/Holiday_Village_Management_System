using System;
using System.Collections.Generic;

namespace Business
{
    /*
     * Author: Maria Luque Anguita 40280156
     * Description of class: defines booking object (with properties and methods)
     * Date last modified: 3/12/2017
     */
    public class Booking
    {
        //properties of booking objects
        private DateTime _arrivalDate;
        private DateTime _departureDate;
        private int _bookingID;
        private int _chaletID;
        private Boolean _eveningMeals;
        private Boolean _breakfast;
        private Boolean _carHire;
        private List<Guest> _guestList = new List<Guest>();
        private CarHire _carhire;

        //constructor
        public Booking(int bookingID)
        {
            _bookingID = bookingID;
        }

        public Booking() { }

        //getters and setters with validations
        public DateTime ArrivalDate
        {
            get { return _arrivalDate; }
            set
            { /*
                if (value < DateTime.Now.Date)
                {
                    throw new ArgumentException("date in wrong");
                }*/
                _arrivalDate = value;
            }
        }

        public DateTime DepartureTime
        {
            get { return _departureDate; }
            set
            {
                if (value <= _arrivalDate)
                {
                    throw new ArgumentException("date out wrong");
                }
                _departureDate = value;
            }
        }

        public int BookingID
        {
            get { return _bookingID; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("wrong booking id");
                }
                _bookingID = value;
            }
        }

        public int ChaletID
        {
            get { return _chaletID; }
            set
            {
                if (value < 1 || value > 10)
                {
                    throw new ArgumentException("Chalet ID can only be 1-10");
                }
                _chaletID = value;
            }
        }

        public Boolean EveningMeal
        {
            get { return _eveningMeals; }
            set { _eveningMeals = value; }
        }

        public Boolean Breakfast
        {
            get { return _breakfast; }
            set { _breakfast = value; }
        }

        public Boolean CarHireBool
        {
            get { return _carHire; }
            set { _carHire = value; }
        }

        public List<Guest> GuestList
        {
            get { return _guestList; }
            set { _guestList = value; }
        }

        public CarHire CarHire
        {
            get { return _carhire; }
            set { _carhire = value; }
        }
    }
}
