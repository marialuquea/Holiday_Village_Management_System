using System;
using System.Windows;
using Business;
using Data;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for NewBooking.xaml
    /// </summary>
    public partial class NewBooking : Window
    {
        public Customer aCustomer;
        
        //instance of the lists
        ObjectsList dataLayerSingleton = ObjectsList.Instance;

        //constructor
        public NewBooking(Customer _aCustomer)
        {
            InitializeComponent();

            aCustomer = _aCustomer;

            //make it impossible for the user to select past dates
            dateInPicker.DisplayDateStart = DateTime.Today;
            dateInPicker.SelectedDate = DateTime.Today;

            dateOutPicker.DisplayDateStart = DateTime.Today.AddDays(+1);
            dateInPicker.SelectedDate = DateTime.Today.AddDays(+1);

            BookingsFactory.numberofbookings(dataLayerSingleton.allBookings.Count);

        }
        
        //add info to the booking, save it and go to next page
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {

            DateTime _dateIn;
            DateTime _dateOut;

            try
            {
                _dateIn = Convert.ToDateTime(dateInPicker.SelectedDate);
                _dateOut = Convert.ToDateTime(dateOutPicker.SelectedDate);


                if (_dateIn.Date < DateTime.Now.Date)
                {
                    throw new ArgumentException("Date in is wrong");
                }
                if (_dateOut.Date <= _dateIn)
                {
                    throw new ArgumentException("Date out is wrong");
                }

            }
            catch (Exception except) //if any of these values are wrong, the program will not crash
            {
                MessageBox.Show(except.Message);
                return;
            }

            //do not leave window until both dates are chosen
            if (dateInPicker.SelectedDate == null || dateOutPicker.SelectedDate == null)
            {
                MessageBox.Show("select datessss pls");
            }
            else
            {

                //create a booking with the dates
                Booking aBooking = BookingsFactory.createBookingFactory();
                aBooking.ArrivalDate = _dateIn;
                aBooking.DepartureTime = _dateOut;

                //save booking in customer
                aCustomer.BookingsList.Add(aBooking);

                //save booking to all bookings list
                dataLayerSingleton.allBookings.Add(aBooking);

                //go to next window passing the customer and booking
                BookingForm nc = new BookingForm(aBooking, aCustomer);
                nc.Show();
                this.Close();
            }
        }

        //go back to previous window
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }


        
    }
}
