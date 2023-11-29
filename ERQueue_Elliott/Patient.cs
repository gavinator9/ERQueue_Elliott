using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERQueue_Elliott
{
    internal class Patient
    {
        private string lastname;
        public string publastname { get { return lastname; } set { lastname = value; } }
        private string firstname;
        public string pubfirstname {get { return firstname; } set { firstname = value; } }
        private string birthday;
        public string pubbirthday { get { return birthday; } set { birthday = value; } }
        private int priority;
        private DateTime birthdayraw;

        public int pubpriority
        {
            get
            {
                return priority;
            }
            set
            {
                priority = value;
            }
        }

        public Patient(string Firstname, string Lastname, string Birthday, int Priority)
        {
            priority= Priority;
            lastname= Lastname;
            firstname= Firstname;
            birthdayraw = DateTime.Parse(Birthday);
            birthday= Birthday;
            DateTime currentdate= DateTime.Now;
            int age=currentdate.Year-birthdayraw.Year;
            if (age > 65 || age < 21)
            {
                priority = 1;
            }
            
        }

        public override string ToString()
        {
            string output = "Firstname: " + firstname + " Lastname: " + lastname + " Priority: " + priority.ToString() + " DOB:"+birthday;
            return output;
        }
    }
}
