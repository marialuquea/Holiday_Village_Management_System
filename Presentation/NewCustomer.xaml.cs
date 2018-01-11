using System.Windows;
using Business;
using Data;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for NewCustomer.xaml
    /// </summary>
    public partial class NewCustomer : Window
    {
        //instance of the lists
        ObjectsList objectlist = ObjectsList.Instance;
        
        //constructor, create new customer
        public NewCustomer()
        {
            InitializeComponent();
            CustomerFactory.numberofcustomers(objectlist.allCustomers.Count);
        }

        //save information into the customer object and save customer in list
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Customer aCustomer = CustomerFactory.createCustomerFactory();

            try
            {
                aCustomer.Name = txtBoxName.Text;
                aCustomer.Address = txtBoxAddress.Text;
            }
            catch 
            {
                MessageBox.Show("make sure values are correct");
                return;
            }

            //add customer to the objects customer list
            objectlist.addCustomer(aCustomer);
            
            //Clear textboxes 
            txtBoxName.Clear();
            txtBoxAddress.Clear();

            //go to next window and close this one
            NewBooking bf = new NewBooking(aCustomer);
            bf.Show();
            this.Close();

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            //go to the window that was before and close this one
            MainWindow nb = new MainWindow();
            nb.Show();
            this.Close();
        }
        
       
    }
}
