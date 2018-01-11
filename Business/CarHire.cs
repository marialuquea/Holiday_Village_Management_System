using System;

namespace Business
{
    /*
     * Author: Maria Luque Anguita 40280156
     * Description of class: defines and create carHire objects
     * Date last modified: 3/12/2017
     */
    public class CarHire
    {
        //properties of the carhire objects
        private DateTime _startDate;
        private DateTime _endDate;
        private string _driversName;

        //getters and setters
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public string DriversName
        {
            get { return _driversName; }
            set { _driversName = value; }
        }
        
    }
}
