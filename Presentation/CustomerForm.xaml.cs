using System;
using System.Windows;
using System.Windows.Input;
using Business;
using Data;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for CustomerForm.xaml
    /// </summary>
    public partial class CustomerForm : Window
    {
        //get objects from the constructor
        public Booking aBooking;
        public Customer aCustomer;
        
        //create list to store customers/bookings or read
        ObjectsList dataLayerSingleton = ObjectsList.Instance;

        //constructor
        public CustomerForm() 
        {
            InitializeComponent();

            //hide buttons delete and modify customer
            btnDelete.IsEnabled = false;
            btnModify.IsEnabled = false;
            btnNext.IsEnabled = false;
            btnNewBooking.IsEnabled = false;
            btnDeleteBooking.IsEnabled = false;

            foreach (Customer customers in dataLayerSingleton.allCustomers)
            {
                if(!lstBoxCustomers.Items.Contains(customers.CustomerID))
                {
                    lstBoxCustomers.Items.Add(customers.CustomerID);
                }
            }
        }

        //view the booking selected
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            //send booking and customer to BookingForm
            BookingForm bf = new BookingForm(aBooking, aCustomer);
            bf.Show();
            this.Close();
        }

        //go to previews window
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow nb = new MainWindow();
            nb.Show();
            this.Close();
        }

        //find customer
        private void btnFind_Click(object sender, RoutedEventArgs e)
        {

            //if the Find ID textbox is not empty...
            if (txtBoxfindID.Text != "")
            {
                //check if customer exists
                if (dataLayerSingleton.findCustomer(Convert.ToInt16(txtBoxfindID.Text))!= null)
                {
                    //create a new customer object with the ID that was input
                    Customer findCustomer = dataLayerSingleton.findCustomer(Convert.ToInt16(txtBoxfindID.Text));

                    //display values
                    lblShowName.Content = findCustomer.Name;
                    lblShowAddress.Content = findCustomer.Address;
                }
                else
                {
                    MessageBox.Show("Customer does not exist");
                }
            }
            else
            {
                MessageBox.Show("Write ID to find");
            }
            
            txtBoxfindID.Clear();
        }

        //delete customer
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Customer bCustomer = new Customer();

            if (txtBoxfindID.Text != "")
            {
                int custID = Convert.ToInt16(txtBoxfindID.Text);

                bCustomer = dataLayerSingleton.findCustomer(custID);

                
                if (lstBoxBookings.Items.Count == 0)
                {
                     dataLayerSingleton.deleteCustomer(custID);
                }
                else
                {
                    MessageBox.Show("You can't delete a customer if it has bookings");
                    return;
                }

                //clear boxes
                txtBoxfindID.Clear();
                lblShowName.Content = "";
                lblShowAddress.Content = "";

                //remove customer from listbox
                lstBoxCustomers.Items.Remove(custID);

                btnDelete.IsEnabled = false;
                btnModify.IsEnabled = false;

                MessageBox.Show("Don't forget to UPDATE INFORMATION before going to next page.");
            }
            else
            {
                MessageBox.Show("Nah nah");
            }
        }

        //when customer double clicked
        private void lstBoxCustomers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //if the listbox is not empty
            if (lstBoxCustomers.SelectedItem != null)
            {
                btnDelete.IsEnabled = true;
                btnModify.IsEnabled = true;
                btnNewBooking.IsEnabled = true;

                //find customer in list
                int customerid = (int)lstBoxCustomers.SelectedItem;
                aCustomer = dataLayerSingleton.findCustomer(customerid);

                txtBoxfindID.Text = Convert.ToString(aCustomer.CustomerID);
                lblShowName.Visibility = Visibility.Visible;
                lblShowName.Content = aCustomer.Name;
                lblShowAddress.Visibility = Visibility.Visible;
                lblShowAddress.Content = aCustomer.Address;
                
                lstBoxBookings.Items.Clear();

                //show bookings of that customer
                foreach (Booking bookings in aCustomer.BookingsList)
                {
                    lstBoxBookings.Items.Add(bookings.BookingID);
                }
            }
            else //display error message if listbox is empty
            {
                MessageBox.Show("Don't be cheeky.");
            }
        }

        //modify a customer
        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            if (txtBoxfindID.Text != "")
            {
                //Show textboxes to modify customer
                modifyName.Visibility = Visibility.Visible;
                modifyAddress.Visibility = Visibility.Visible;
                btnSave.Visibility = Visibility.Visible;
                lblName2.Visibility = Visibility.Visible;
                lblAddress2.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Select customer to modify.");
            }
        }

        //save modified costumer
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //if both textboxes aren't empty
            if ((modifyName.Text != "") && (modifyAddress.Text != ""))
            {
                //get customer ID from textBox
                int custID = Convert.ToInt16(txtBoxfindID.Text);

                //if customer exists on the list
                if (dataLayerSingleton.findCustomer(custID) != null)
                {
                    Customer modifyCust = dataLayerSingleton.findCustomer(custID);
                    modifyCust.Name = modifyName.Text;
                    modifyCust.Address = modifyAddress.Text;
                    lblShowName.Content = aCustomer.Name;
                    lblShowAddress.Content = aCustomer.Address;

                    //hide textboxes again
                    modifyName.Visibility = Visibility.Hidden;
                    modifyAddress.Visibility = Visibility.Hidden;
                    btnSave.Visibility = Visibility.Hidden;
                    lblName2.Visibility = Visibility.Hidden;
                    lblAddress2.Visibility = Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show("Customer ID does not exist.");
                }
            }
            else
            {
                MessageBox.Show("Please enter both details.");
            }
        }

        //when booking double clicked
        private void lstBoxBookings_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lstBoxBookings.SelectedItem != null)
            {
                btnNext.IsEnabled = true;
                btnDeleteBooking.IsEnabled = true;

                //find booking in list
                int bookingid = (int)lstBoxBookings.SelectedItem;
              
                foreach(Booking b in dataLayerSingleton.allBookings)
                {
                    if(bookingid == b.BookingID)
                    {
                        aBooking = b;
                    }
                }
                //Show booking details
                lblBookingStartDate.Content = aBooking.ArrivalDate.ToShortDateString();
                lblBookingEndDate.Content = aBooking.DepartureTime.ToShortDateString();

                lblhello.Content = aBooking.BookingID;
            }
            else
            {
                MessageBox.Show("nah nah");
            }
        }

        //add a new booking to the selcted customer, go to next window
        private void btnNewBooking_Click(object sender, RoutedEventArgs e)
        {
            NewBooking nb = new NewBooking(aCustomer);
            nb.Show();
            this.Close();
        }

        //delete booking
        private void btnDeleteBooking_Click(object sender, RoutedEventArgs e)
        {
            Booking bBooking = new Booking();

            if (lblhello.Content != null)
            {
                int bookingid = Convert.ToInt16(lblhello.Content);
                
                //find which booking has to be deleted
                foreach (Booking b in aCustomer.BookingsList)
                {
                    if (bookingid == b.BookingID)
                    {
                        bBooking = b;
                    }
                }

                aCustomer.BookingsList.Remove(bBooking);
                dataLayerSingleton.deleteBooking(bookingid);

                MessageBox.Show("Don't forget to UPDATE INFORMATION before going to next page.");
                
                //clear boxes
                lblBookingStartDate.Content = "";
                lblBookingEndDate.Content = "";
                lblhello.Content = "";

                //remove booking from listbox
                lstBoxBookings.Items.Remove(bookingid);

                btnDeleteBooking.IsEnabled = false;

            }
            else
            {
                MessageBox.Show("Nah nah");
            }
            
        }

        //serialise data to xml file
        private void btnUpdateInfo_Click(object sender, RoutedEventArgs e)
        {
            //Use the serializer to save all lists
            Serializer saveLists = new Serializer();
            saveLists.customerSerializer();
            saveLists.bookingsSerializer();

            MessageBox.Show("Information updated correctly.");
        }
    }
}
