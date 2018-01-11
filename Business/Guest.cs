using System;

namespace Business
{
    /*
     * Author: Maria Luque Anguita 40280156
     * Description of class: defines and creates guest object 
     * Date last modified: 3/12/2017
     */
    public class Guest
    {
        //properties of the object
        private string _name;
        private string _passportNo;
        private int _age;

        //getters and setters for the properties
        public string Name
        {
            get { return _name; }
            set
            {
                if (value == "")
                {
                    throw new ArgumentException("Plase fill in name.");
                }
                _name = value;
            }
        }

        public string PassportNo
        {
            get { return _passportNo; }
            set
            {
                if (value.Length > 10 || value.Length < 1)
                {
                    throw new ArgumentException("Passport no. too long");
                }
                _passportNo = value;
            }
        }

        public int Age
        {
            get { return _age; }
            set
            {
                if (value < 0 || value > 101)
                {
                    throw new ArgumentException("Age wrong.");
                }
                _age = value;
            }
        }
        
    }
}
