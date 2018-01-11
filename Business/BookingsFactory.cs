namespace Business
{
    /*
     * Author: Maria Luque Anguita 40280156
     * Description of class: find next available ID and create booking objects with set ID
     * Date last modified: 3/12/2017
     * Design Pattern: Factory
     */
    public class BookingsFactory
    {
        static private int bookingCounter = 0;

        //find total number of bookinsgs in the program (list)
        static public void numberofbookings(int bookingid)
        {
            bookingCounter = bookingid;
        }
        
        //increment number of bookings to set next bookingID
        public static Booking createBookingFactory()
        {
            bookingCounter++;

            Booking newBooking = new Booking(bookingCounter);

            return newBooking;
        }
    }
}
