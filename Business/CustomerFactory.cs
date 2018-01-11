namespace Business
{
    /*
     * Author: Maria Luque Anguita 40280156
     * Description of class: find nect available ID and create customer object with that ID
     * Date last modified: 3/12/2017
     * Design pattern: Factory
     */
    public class CustomerFactory
    {

        static private int customerCounter = 0;
        
        //counts number of customers in customer list
        static public void numberofcustomers(int custid)
        {
            customerCounter = custid;
        }
        
        //increments number of customer in list to set the next customerID
        public static Customer createCustomerFactory()
        {
            customerCounter++;

            Customer newCustomer = new Customer(customerCounter);
            
            return newCustomer;
        }
        
    }
}
