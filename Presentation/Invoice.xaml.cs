using System;
using System.Windows;
using Business;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for Invoice.xaml
    /// </summary>
    public partial class Invoice : Window
    {
        //set booking to the one passed in the constructor parameter
        Booking aBooking;

        //cosntructor, show values in labels
        public Invoice(Booking _booking,
                        int guests,
                        int num_nights,
                        int nights,
                        int meals,
                        int breakfast,
                        int carhire,
                        int carhire_days)
        {
            InitializeComponent();

            int total = guests + nights + meals + breakfast + carhire;

            aBooking = _booking;

            //show values
            lblGuests.Content = Convert.ToInt16(aBooking.GuestList.Count);
            lblNights.Content = Convert.ToString(num_nights);
            lblGuestsTotal.Content = Convert.ToString(guests);
            lblNightsTotal.Content = Convert.ToString(nights);

            //extras
            lblMealsTotal.Content = Convert.ToString(meals);
            lblBreakfastTotal.Content = Convert.ToString(breakfast);
            lblCarHireTotal.Content = Convert.ToString(carhire);
            lblTOTAL.Content = Convert.ToString(total);

            if (meals == 0)
                lblMeals.Content = Convert.ToString(0);
            else
                lblMeals.Content = "Yes";

            if (breakfast == 0)
                lblBreakfast.Content = Convert.ToString(0);
            else
                lblBreakfast.Content = "Yes";

            if (carhire == 0)
                lblCarHire.Content = Convert.ToString(0);
            else
                lblCarHire.Content = Convert.ToString(carhire_days) + " days";
        }

        
    }
}
