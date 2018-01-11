using System.Windows;
using Data;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //constructor
        public MainWindow()
        {
            InitializeComponent();
            
            //Load saved Xml ObjectsLists into the program
            Serializer loadLists = new Serializer();
            loadLists.customerDeserializer();
            loadLists.bookingDeserializer();
        }

        //go to newCustomer window
        private void btnNewc_Click(object sender, RoutedEventArgs e)
        {
            NewCustomer nc = new NewCustomer();
            nc.Show();
            this.Close();
        }

        //go to existingCustomer window
        private void btnExistingc_Click(object sender, RoutedEventArgs e)
        {
            CustomerForm cf = new CustomerForm();
            cf.Show();
            this.Close();
        }
    }
}
