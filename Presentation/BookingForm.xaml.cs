using System;
using System.Windows;
using System.Windows.Input;
using Business;
using Data;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for BookingForm.xaml
    /// </summary>
    public partial class BookingForm : Window
    {
        CarHire newcarhire = new CarHire();
        
        public Booking aBooking;
        public Customer aCustomer;
        
        int price_of_nights;
        int price_of_guests;
        int number_of_guests;
        int number_of_nights;
        int carhire_days;
        int meals_price;
        int breakfast_price;
        int carhire_price;

        //create a list to store bookings and guests
        ObjectsList dataLayerSingleton = ObjectsList.Instance;
        
        //constructor and initialise values
        public BookingForm(Booking _aBooking, Customer _aCustomer)
        {
            InitializeComponent();

            //add options to chalet combobox
            chaletsOption.Items.Add("1");
            chaletsOption.Items.Add("2");
            chaletsOption.Items.Add("3");
            chaletsOption.Items.Add("4");
            chaletsOption.Items.Add("5");
            chaletsOption.Items.Add("6");
            chaletsOption.Items.Add("7");
            chaletsOption.Items.Add("8");
            chaletsOption.Items.Add("9");
            chaletsOption.Items.Add("10");

            //set customer and booking
            aCustomer = _aCustomer;
            aBooking = _aBooking;

            //show dates in GUI
            lblDateIn.Content = aBooking.ArrivalDate;
            lblDateOut.Content = aBooking.DepartureTime;
            lblCustomerID.Content = aCustomer.CustomerID;
            lblBookingID.Content = aBooking.BookingID;

            //set days in car hire
            dStart.DisplayDateStart = aBooking.ArrivalDate;
            dStart.DisplayDateEnd = aBooking.DepartureTime;
            dEnd.DisplayDateEnd = aBooking.DepartureTime;
            dEnd.DisplayDateStart = aBooking.ArrivalDate.AddDays(+1);

            //block buttons
            btnRemoveGuest.IsEnabled = false;
            btnModifyGuest.IsEnabled = false;

            //if the customer already has stuff, show
            if (aBooking.EveningMeal == true)
                checkMeals.IsChecked = true;
            if (aBooking.Breakfast == true)
                checkBreakfast.IsChecked = true;
            if (aBooking.CarHireBool == true)
            {
                checkCarHire.IsChecked = true;
                lblInfo.Visibility = Visibility.Visible;
                lblShowCarStart.Visibility = Visibility.Visible;
                lblShowCarEnd.Visibility = Visibility.Visible;
                lblShowDriver.Visibility = Visibility.Visible;
                lblShowCarStart.Content = aBooking.CarHire.StartDate.ToString("d");
                lblShowCarEnd.Content = aBooking.CarHire.EndDate.ToString("d");
                lblShowDriver.Content = aBooking.CarHire.DriversName;
                btnEditCarHire.Visibility = Visibility.Visible;
            }
            if (aBooking.ChaletID != 0)
            {
                chaletsOption.SelectedIndex = aBooking.ChaletID - 1;
            }
                
            //display guests
            foreach (Guest g in aBooking.GuestList)
            {
                if (!lstBoxGuests.Items.Contains(g))
                {
                    lstBoxGuests.Items.Add(g.PassportNo);
                }
            }

        }

        //go to previews page
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        //display the total cost
        private void btnInvoice_Click(object sender, RoutedEventArgs e)
        {
            //calculate price
            number_of_guests = lstBoxGuests.Items.Count;
            price_of_guests = number_of_guests * 25;
            

            number_of_nights = Convert.ToInt16((aBooking.DepartureTime - aBooking.ArrivalDate).TotalDays);
            price_of_nights = number_of_nights * 60;

            if (checkMeals.IsChecked == true)
            {
                meals_price = number_of_guests * 10 * number_of_nights;

                aBooking.EveningMeal = true;
            }
            else
            {
                meals_price = 0;

                aBooking.EveningMeal = false;
            }
            if (checkBreakfast.IsChecked == true)
            {
                breakfast_price = number_of_guests * 5 * (number_of_nights);

                aBooking.Breakfast = true;
            }
            else
            {
                breakfast_price = 0;

                aBooking.Breakfast = false;
            }
            if (checkCarHire.IsChecked == true)
            {
                carhire_days = Convert.ToInt16((newcarhire.EndDate - newcarhire.StartDate).TotalDays);
                carhire_price = carhire_days * 50;

                aBooking.CarHireBool = true;
            }
            else
            {
                carhire_price = 0;
                aBooking.CarHireBool = false;
            }
            

            //open new window
            Invoice newWin = new Invoice(aBooking, price_of_guests, number_of_nights, price_of_nights, meals_price, breakfast_price, carhire_price, carhire_days);
            newWin.Show();

        }

        //show textboxes to input new information for guests
        private void btnAddGuest_Click(object sender, RoutedEventArgs e)
        {
            //maximum = 6 guests
            if (lstBoxGuests.Items.Count < 6)
            {
                //show textboxes to input guest
                lblName.Visibility = Visibility.Visible;
                lblAge.Visibility = Visibility.Visible;
                lblPassport.Visibility = Visibility.Visible;
                txtBoxGuestName.Visibility = Visibility.Visible;
                txtBoxGuestAge.Visibility = Visibility.Visible;
                txtBoxGuestPassp.Visibility = Visibility.Visible;
                btnSaveGuest.Visibility = Visibility.Visible;

                //hide button save info so it doesn't interfere
                btnSaveAndExit.Visibility = Visibility.Hidden;
            }
            else
            {
                btnAddGuest.IsEnabled = false;
                MessageBox.Show("max is 6 guests");
            }
        }

        //save the new guest in the booking's guest list
        private void btnSaveGuest_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = new Guest();

            try
            {
                guest.Name = txtBoxGuestName.Text;
                guest.Age = Convert.ToInt16(txtBoxGuestAge.Text);
                guest.PassportNo = txtBoxGuestPassp.Text;
            }
            catch
            {
                MessageBox.Show("Fill missing details.");
                return;
            }
            

            lstBoxGuests.Items.Add(guest.PassportNo);

            //add guest to booking
            aBooking.GuestList.Add(guest);

            //clear textboxes
            txtBoxGuestName.Clear();
            txtBoxGuestAge.Clear();
            txtBoxGuestPassp.Clear();

            //hide again all textboxes and labels
            lblName.Visibility = Visibility.Hidden;
            lblAge.Visibility = Visibility.Hidden;
            lblPassport.Visibility = Visibility.Hidden;
            txtBoxGuestName.Visibility = Visibility.Hidden;
            txtBoxGuestAge.Visibility = Visibility.Hidden;
            txtBoxGuestPassp.Visibility = Visibility.Hidden;
            btnSaveGuest.Visibility = Visibility.Hidden;

            //show save info button again
            btnSaveAndExit.Visibility = Visibility.Visible;
        }

        //when a guest is selected from listbox
        private void lstBoxGuests_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Guest guest = new Guest();

            if (lstBoxGuests.SelectedItem != null)
            {
                //allow removing/modifying guest buttons
                btnRemoveGuest.IsEnabled = true;
                btnModifyGuest.IsEnabled = true;

                //find guest on list
                string guestPassport = lstBoxGuests.SelectedItem.ToString();

                foreach(Guest g in aBooking.GuestList)
                {
                    if (guestPassport == g.PassportNo)
                    {
                        guest = g;
                    }
                }

                //display values
                lblShowName.Content = guest.Name;
                lblShowAge.Content = guest.Age;
                
                //show details of guest
                lblName2.Visibility = Visibility.Visible;
                lblAge2.Visibility = Visibility.Visible;
                lblShowAge.Visibility = Visibility.Visible;
                lblShowName.Visibility = Visibility.Visible;
            }
            else //if the listbox is empty and the user double clicks it
            {
                MessageBox.Show("Add guest please");
            }            
        }

        //delete guest
        private void btnRemoveGuest_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = new Guest();

            string curGuest = lstBoxGuests.SelectedItem.ToString();
            
            //find guest in booking's guest list
            foreach (Guest g in aBooking.GuestList)
            {
                if (curGuest == g.PassportNo)
                {
                    guest = g;
                }
            }

            aBooking.GuestList.Remove(guest);

            //delete item from listbox
            lstBoxGuests.Items.Remove(curGuest);

            lblShowName.Content = "";
            lblShowAge.Content = "";
        }

        //show boxes to edit guest
        private void btnModifyGuest_Click(object sender, RoutedEventArgs e)
        {
            //show textboxes to input guest
            lblName.Visibility = Visibility.Visible;
            lblAge.Visibility = Visibility.Visible;
            txtBoxGuestName.Visibility = Visibility.Visible;
            txtBoxGuestAge.Visibility = Visibility.Visible;
            btnSaveModifiedGuest.Visibility = Visibility.Visible;

            //hide save info button so it doesn't interfere
            btnSaveAndExit.Visibility = Visibility.Hidden;
        }

        //save modified guest
        private void btnSaveModifiedGuest_Click(object sender, RoutedEventArgs e)
        {
            Guest modifyGuest = new Guest();

            string guestPassport = lstBoxGuests.SelectedItem.ToString();

            //if all textboxes have information
            if ((txtBoxGuestName.Text!="") && (txtBoxGuestAge.Text!=""))
            {
                foreach (Guest g in aBooking.GuestList)
                {
                    if (guestPassport == g.PassportNo)
                    {
                        modifyGuest = g;
                    }
                }
                
                modifyGuest.Name = txtBoxGuestName.Text;
                modifyGuest.Age = Convert.ToInt16(txtBoxGuestAge.Text);

                //hide boxes
                lblName.Visibility = Visibility.Hidden;
                lblAge.Visibility = Visibility.Hidden;
                txtBoxGuestName.Visibility = Visibility.Hidden;
                txtBoxGuestAge.Visibility = Visibility.Hidden;
                btnSaveModifiedGuest.Visibility = Visibility.Hidden;

                //show save info button again
                btnSaveAndExit.Visibility = Visibility.Visible;

                //display new info
                lblShowName.Content = modifyGuest.Name;
                lblShowAge.Content = modifyGuest.Age;
            }
            else
            {
                MessageBox.Show("pls enter all details");
            }
        }
        
        //when car hire is checked show info and options about the car hire
        private void checkCarHire_Checked(object sender, RoutedEventArgs e)
        {
            lblShowCarStart.Visibility = Visibility.Visible;
            lblShowCarEnd.Visibility = Visibility.Visible;
            lblShowDriver.Visibility = Visibility.Visible;
            lblInfo.Visibility = Visibility.Visible;
            btnEditCarHire.Visibility = Visibility.Visible;
            
        }

        //when car hire is unchecked, hide info about it
        private void checkCarHire_Unchecked(object sender, RoutedEventArgs e)
        {
            lblCarStart.Visibility = Visibility.Hidden;
            lblCarEnd.Visibility = Visibility.Hidden;
            lblDriverName.Visibility = Visibility.Hidden;
            dStart.Visibility = Visibility.Hidden;
            dEnd.Visibility = Visibility.Hidden;
            txtBoxDriverName.Visibility = Visibility.Hidden;
            btnSaveCarHire.Visibility = Visibility.Hidden;
            btnEditCarHire.Visibility = Visibility.Hidden;
            lblShowCarStart.Visibility = Visibility.Hidden;
            lblShowCarEnd.Visibility = Visibility.Hidden;
            lblShowDriver.Visibility = Visibility.Hidden;
            lblInfo.Visibility = Visibility.Hidden;
            
        }

        //save information about car hire
        private void btnSaveCarHire_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dStart.SelectedDate < aBooking.ArrivalDate || dStart.SelectedDate > aBooking.DepartureTime)
                {
                    throw new ArgumentException("car hire date in wrong");
                }
                if (dEnd.SelectedDate < dStart.SelectedDate || dEnd.SelectedDate > aBooking.DepartureTime)
                {
                    throw new ArgumentException("car hire date out wrong");
                }

                newcarhire.StartDate = Convert.ToDateTime(dStart.SelectedDate);
                newcarhire.EndDate = Convert.ToDateTime(dEnd.SelectedDate);
                newcarhire.DriversName = txtBoxDriverName.Text;
                
            }
            catch
            {
                MessageBox.Show("Fill in missing values.");
                return;
            }
            

            //save car hire in booking
            aBooking.CarHireBool = true;
            aBooking.CarHire = newcarhire;

            //hide boxes
            lblCarStart.Visibility = Visibility.Hidden;
            lblCarEnd.Visibility = Visibility.Hidden;
            lblDriverName.Visibility = Visibility.Hidden;
            dStart.Visibility = Visibility.Hidden;
            dEnd.Visibility = Visibility.Hidden;
            txtBoxDriverName.Visibility = Visibility.Hidden;
            btnSaveCarHire.Visibility = Visibility.Hidden;
        }

        //show boxes to modify car hire
        private void btnEditCarHire_Click(object sender, RoutedEventArgs e)
        {
            lblCarStart.Visibility = Visibility.Visible;
            lblCarEnd.Visibility = Visibility.Visible;
            lblDriverName.Visibility = Visibility.Visible;
            dStart.Visibility = Visibility.Visible;
            dEnd.Visibility = Visibility.Visible;
            txtBoxDriverName.Visibility = Visibility.Visible;
            btnSaveModifiedCarHire.Visibility = Visibility.Visible;

            lblShowCarStart.Visibility = Visibility.Hidden;
            lblShowCarEnd.Visibility = Visibility.Hidden;
            lblShowDriver.Visibility = Visibility.Hidden;
            lblInfo.Visibility = Visibility.Hidden;
            btnEditCarHire.Visibility = Visibility.Hidden;

        }

        //save infromation about car hire
        private void btnSaveModifiedCarHire_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dStart.SelectedDate < aBooking.ArrivalDate || dStart.SelectedDate > aBooking.DepartureTime)
                {
                    throw new ArgumentException("car hire date in wrong");
                }
                if (dEnd.SelectedDate < dStart.SelectedDate || dEnd.SelectedDate > aBooking.DepartureTime)
                {
                    throw new ArgumentException("car hire date out wrong");
                }
                if (txtBoxDriverName.Text == "")
                {
                    throw new ArgumentException("Driver's name is missing.");
                }

                newcarhire.StartDate = Convert.ToDateTime(dStart.SelectedDate);
                newcarhire.EndDate = Convert.ToDateTime(dEnd.SelectedDate);
                newcarhire.DriversName = txtBoxDriverName.Text;
                
            }
            catch
            {
                MessageBox.Show("Fill in missing values.");
                return;
            }

            //save it in the booking
            aBooking.CarHire = newcarhire;

            //hide boxes
            lblCarStart.Visibility = Visibility.Hidden;
            lblCarEnd.Visibility = Visibility.Hidden;
            lblDriverName.Visibility = Visibility.Hidden;
            dStart.Visibility = Visibility.Hidden;
            dEnd.Visibility = Visibility.Hidden;
            txtBoxDriverName.Visibility = Visibility.Hidden;
            btnSaveModifiedCarHire.Visibility = Visibility.Hidden;

            lblShowCarStart.Visibility = Visibility.Visible;
            lblShowCarStart.Content = newcarhire.StartDate.ToString("d");
            lblShowCarEnd.Visibility = Visibility.Visible;
            lblShowCarEnd.Content = newcarhire.EndDate.ToString("d");
            lblShowDriver.Visibility = Visibility.Visible;
            lblShowDriver.Content = newcarhire.DriversName;
            lblInfo.Visibility = Visibility.Visible;
            btnEditCarHire.Visibility = Visibility.Visible;
        }

        //serialise lists to the xml file
        private void btnSaveAndExit_Click(object sender, RoutedEventArgs e)
        {
            //make sure a chalet id is selected
            if (chaletsOption.SelectedIndex > -1) //something was selected
            {
                aBooking.ChaletID = Convert.ToInt16(chaletsOption.SelectedItem);

                //Use the serializer to save all lists
                Serializer saveLists = new Serializer();
                saveLists.customerSerializer();
                saveLists.bookingsSerializer();

                MessageBox.Show("Information saved successfully. You can now close the program.");
            }
            else
            {
                MessageBox.Show("Please select a chalet number.");
            }
            
        }

        //go to customers window
        private void btnViewCustomers_Click(object sender, RoutedEventArgs e)
        {
            CustomerForm cf = new CustomerForm();
            cf.Show();
            this.Close();
        }

        //show options to modify dates
        private void btnModifyDates_Click(object sender, RoutedEventArgs e)
        {
            lblModifyDates.Visibility = Visibility.Visible;
            modifyStart.Visibility = Visibility.Visible;
            modifyEnd.Visibility = Visibility.Visible;
            btnSaveModifyDates.Visibility = Visibility.Visible;

            //make it impossible for the user to select past dates
            modifyStart.DisplayDateStart = DateTime.Today;
            modifyStart.SelectedDate = DateTime.Today;

            modifyEnd.DisplayDateStart = DateTime.Today.AddDays(+1);
            modifyEnd.SelectedDate = DateTime.Today.AddDays(+1);
        }

        //update booking with new dates
        private void btnSaveModifyDates_Click(object sender, RoutedEventArgs e)
        {
            if (modifyStart.SelectedDate == null || modifyEnd.SelectedDate == null)
            {
                MessageBox.Show("Select dates please.");
            }
            else
            {
                try
                {
                    aBooking.ArrivalDate = Convert.ToDateTime(modifyStart.SelectedDate);
                    aBooking.DepartureTime = Convert.ToDateTime(modifyEnd.SelectedDate);

                    if (aBooking.ArrivalDate.Date < DateTime.Now.Date)
                    {
                        throw new ArgumentException("Check in is wrong");
                    }
                    if (aBooking.DepartureTime.Date <= aBooking.ArrivalDate)
                    {
                        throw new ArgumentException("Check out is wrong");
                    }
                }
                catch (Exception except)
                {
                    MessageBox.Show(except.Message);
                    return;
                }

                lblModifyDates.Visibility = Visibility.Hidden;
                modifyStart.Visibility = Visibility.Hidden;
                modifyEnd.Visibility = Visibility.Hidden;
                btnSaveModifyDates.Visibility = Visibility.Hidden;

                //show new dates in GUI
                lblDateIn.Content = aBooking.ArrivalDate.ToString("d");
                lblDateOut.Content = aBooking.DepartureTime.ToString("d");

                
                //change hire dates options
                dStart.DisplayDateStart = aBooking.ArrivalDate;
                dStart.DisplayDateEnd = aBooking.DepartureTime;
                dEnd.DisplayDateEnd = aBooking.DepartureTime;
                dEnd.DisplayDateStart = aBooking.ArrivalDate.AddDays(+1);

                //display message to change car hire dates
                 MessageBox.Show("Please change car hire dates if you chose it");
            }

        }

        //update booking if evening meals is checked
        private void checkMeals_Checked(object sender, RoutedEventArgs e)
        {
            aBooking.EveningMeal = true;
        }

        //update booking if evening meals is unchecked
        private void checkMeals_Unchecked(object sender, RoutedEventArgs e)
        {
            aBooking.EveningMeal = false;
        }

        //update booking if breakfast is checked
        private void checkBreakfast_Checked(object sender, RoutedEventArgs e)
        {
            aBooking.Breakfast = true;
        }

        //update booking if breakfast is unchecked
        private void checkBreakfast_Unchecked(object sender, RoutedEventArgs e)
        {
            aBooking.Breakfast = false;
        }
    }
}
